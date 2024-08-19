using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SimpleRAG.Common.Options;

namespace SimpleRAG.Models
{
    public class OpenAIHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            UriBuilder uriBuilder;
            string chatUrl = ChatAIOption.Endpoint;
            string embeddingUrl = EmbeddingOption.Endpoint;
            Uri uri1 = new Uri(chatUrl);
            string scheme1 = uri1.Scheme;
            string host1 = uri1.Host;
            Uri uri2 = new Uri(embeddingUrl);
            string scheme2 = uri2.Scheme;
            string host2 = uri2.Host;
            switch (request.RequestUri?.LocalPath)
            {
                case "/v1/chat/completions":
                    uriBuilder = new UriBuilder(request.RequestUri)
                    {
                        // 这里是你要修改的 URL
                        Scheme = scheme1,
                        Host = host1,
                        Path = "v1/chat/completions",
                    };
                    request.RequestUri = uriBuilder.Uri;
                    break;

                case "/v1/embeddings":
                    uriBuilder = new UriBuilder(request.RequestUri)
                    {
                        // 这里是你要修改的 URL
                        Scheme = scheme2,
                        Host = host2,
                        Path = "v1/embeddings",
                    };
                    request.RequestUri = uriBuilder.Uri;
                    break;
            }

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}
