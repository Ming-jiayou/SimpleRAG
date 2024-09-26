using SimpleRAG.Interface;
using SimpleRAG.Models;
using Microsoft.SemanticKernel;
using SimpleRAG.Common.Options;
using System.Net.Http;
using Microsoft.SemanticKernel.Text;
using System.Reflection.Metadata;
using System;
using Microsoft.SemanticKernel.Connectors.Sqlite;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Reflection;
using Microsoft.SemanticKernel.ChatCompletion;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
namespace SimpleRAG.Services
{
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0010
    public class SemanticKernelService : ISemanticKernelService
    {
        private readonly Kernel _kernel;
        public SemanticKernelService()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
              modelId: ChatAIOption.ChatModel,
              apiKey: ChatAIOption.Key,
              httpClient: new HttpClient(handler));
            var kernel = builder.Build();
            
            _kernel = kernel;
        }

        public async Task<Query> GetAIResponse(string question)
        {
            var query = new Query { Question = question };
            var result = await _kernel.InvokePromptAsync(question);
            var str = result.ToString();
            query.Answer = str;
            return query;
        }

        public async IAsyncEnumerable<string> GetAIResponse2(string question)
        {
            var query = new Query { Question = question };
            await foreach (var str in _kernel.InvokePromptStreamingAsync(question))
            {
                yield return str.ToString();
            }          
        }

        public async IAsyncEnumerable<string> GetAIResponse3(string question,string filePath)
        {
            string fileContent = File.ReadAllText(filePath);
            string skPrompt = """
                               获取到的文件内容：{{$FileContent}}。
                               根据获取到的信息回答问题：{{$Question}}。
                               如果文件内容中没有提到，直接回答不知道。
                            """;
            await foreach (var str in _kernel.InvokePromptStreamingAsync(skPrompt, new() { ["FileContent"] = fileContent, ["Question"] = question }))
            {
                yield return str.ToString();
            }
        }

        public async Task Embedding(QueryModel queryModel)
        {           
            var textMemory = await GetTextMemory();
            var index = queryModel.Index;
            var text = queryModel.Text;
            var lines = TextChunker.SplitPlainTextLines(queryModel.Text, TextChunkerOption.LinesToken);
            var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, TextChunkerOption.ParagraphsToken);

            foreach (var para in paragraphs)
            {
                await textMemory.SaveInformationAsync(index, id: Guid.NewGuid().ToString(), text: para, cancellationToken: default);
            }
          
        }

        public async Task<Query> GetRAGResponse(QueryModel queryModel)
        {
            var textMemory = await GetTextMemory();
            var index = queryModel.Index;
            var text = queryModel.Text;
            var memoryResults = textMemory.SearchAsync(index, text, limit: 3, minRelevanceScore: 0.3);
            string result = "";
            string information = "";
            await foreach (MemoryQueryResult memoryResult in memoryResults)
            { 
                information += memoryResult.Metadata.Text;
            }
            string skPrompt = """
                               获取到的相关信息：{{$Information}}。
                               根据获取到的信息回答问题：{{$Question}}。
                               如果没有获取到相关信息，直接回答不知道。
                            """;
            var response = await _kernel.InvokePromptAsync(skPrompt, new() { ["Information"] = information, ["Question"] = queryModel.Text });           
            result = response.ToString();
            var query = new Query { Question = text, Answer = result };
            return query;
        }

        public async Task<ISemanticTextMemory> GetTextMemory()
        {
            var memoryBuilder = new MemoryBuilder();
            var handler = new OpenAIHttpClientHandler();
            memoryBuilder.WithOpenAITextEmbeddingGeneration(EmbeddingOption.EmbeddingModel, EmbeddingOption.Key, httpClient: new HttpClient(handler));
            IMemoryStore memoryStore = await SqliteMemoryStore.ConnectAsync("memstore.db");
            memoryBuilder.WithMemoryStore(memoryStore);
            var textMemory = memoryBuilder.Build();
            return textMemory;
        }

        public async Task<string> RunUniversalLLMFunctionCallerSampleAsync(string askText)
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            var kernel = builder.Build();

            // Add a plugin with some helper functions we want to allow the model to utilize.
            kernel.ImportPluginFromFunctions("HelperFunctions",
             [
                 kernel.CreateFunctionFromMethod(() => DateTime.Now.ToString("R"), "GetCurrentTime", "获取当前的时间"),
                 kernel.CreateFunctionFromMethod((string cityName) =>
                 cityName switch
                 {
                    "武汉" => "38℃，晴天",
                    "北京" => "25℃，下雨",
                    "杭州" => "30℃，阴天",
                    "福州" => "35℃，晴天",
                    "厦门" => "27℃，阴天",
                     _ => "31℃，晴天",
                 }, "GetWeatherForCity", "获取指定城市的当前天气情况。"),
                 kernel.CreateFunctionFromMethod((int id) =>
                 {
                   List<Product> Products = new List<Product>()
                   {
                       new Product{Id = 1,Name = "iPhone 15",Address = "湖北武汉"},
                       new Product{Id = 2,Name = "小米",Address = "广东深圳"},
                       new Product{Id = 3,Name = "华为",Address = "广东广州"},
                       new Product{Id = 4,Name = "vivo",Address = "福建福州"},
                       new Product{Id = 5,Name = "oppo",Address = "福建厦门"}
                   };
                   var product = Products.Find(p => p.Id == id);
                   return product;
                    
                 }, "GetProductById", "根据id获取订单。"),
            ]);
         
            ChatHistory history = new ChatHistory();
            history.AddUserMessage(askText);
            UniversalLLMFunctionCaller planner = new(kernel);
            string result = await planner.RunAsync(askText);

            history.AddAssistantMessage(result);
            return result;
        }

        public async Task<string> RunTranslationAIAgentSampleAsync(string askText)
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            var kernel = builder.Build();

            // Add a plugin with some helper functions we want to allow the model to utilize.
            kernel.ImportPluginFromFunctions("HelperFunctions",
             [
                kernel.CreateFunctionFromMethod((string text,string filePath) =>
                {
                    // 指定文件的路径
                    //string filePath = @"D:\桌面\test.txt";

                    // 使用 StreamWriter 将文本写入文件
                    using (StreamWriter writer = new StreamWriter(filePath,true))
                    {
                           writer.WriteLine(text);
                    }
                    return "已成功写入文件";

                }, "SaveTextToTxt", "将文本写入文件")
            ]);

            kernel.Plugins.AddFromType<Translation>();

            ChatHistory history = new ChatHistory();
            history.AddUserMessage(askText);
            UniversalLLMFunctionCaller planner = new(kernel);
            string result = await planner.RunAsync(askText);

            history.AddAssistantMessage(result);
            return result;
        }
    }
}
