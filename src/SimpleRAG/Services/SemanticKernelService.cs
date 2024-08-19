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

        public async Task Embedding(QueryModel queryModel)
        {           
            var textMemory = await GetTextMemory();
            var index = queryModel.Index;
            var text = queryModel.Text;
            var lines = TextChunker.SplitPlainTextLines(queryModel.Text, 20);
            var paragraphs = TextChunker.SplitPlainTextParagraphs(lines, 100);

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
            var memoryResults = textMemory.SearchAsync(index, text, limit: 3, minRelevanceScore: 0.5);
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
    }
}
