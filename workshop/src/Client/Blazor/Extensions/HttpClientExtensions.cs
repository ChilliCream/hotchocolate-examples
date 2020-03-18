using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Client.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddBearerToken(this HttpClient client, string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                client
                    .DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue(
                        "Bearer",
                        token);
            }
        }
    }
}