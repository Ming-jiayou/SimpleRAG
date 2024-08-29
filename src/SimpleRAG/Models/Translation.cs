using Microsoft.SemanticKernel;
using SimpleRAG.Common.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.Models
{
    internal class Translation
    {
        private readonly Kernel _kernel;
        public Translation()
        {
            var handler = new OpenAIHttpClientHandler();
            var builder = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
               modelId: ChatAIOption.ChatModel,
               apiKey: ChatAIOption.Key,
               httpClient: new HttpClient(handler));
            _kernel = builder.Build();
        }
        [KernelFunction, Description("选择用户想要的语言翻译文本")]
        public async Task<string> TranslateText(
            [Description("要翻译的文本")] string text,
            [Description("要翻译成的语言，从'中文'、'英文'中选一个")] string language
 )
        {
            string skPrompt = """
                            {{$input}}

                            将上面的文本翻译成{{$language}}，无需任何其他内容
                            """;
            var result = await _kernel.InvokePromptAsync(skPrompt, new() { ["input"] = text, ["language"] = language });
            var str = result.ToString();
            return str;
        }
    }
}
