using SimpleRAG.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.Interface
{
    public interface ISemanticKernelService
    {
        public Task<Query> GetAIResponse(string question);
        public Task Embedding(QueryModel queryModel);
        public Task<Query> GetRAGResponse(QueryModel queryModel);
        public IAsyncEnumerable<string> GetAIResponse2(string question);
        public IAsyncEnumerable<string> GetAIResponse3(string question, string filePath);
        public IAsyncEnumerable<string> GetAIResponse4(string question, string imagePath);
        public Task<string> RunUniversalLLMFunctionCallerSampleAsync(string askText);
        public Task<string> RunTranslationAIAgentSampleAsync(string askText);
    }
}
