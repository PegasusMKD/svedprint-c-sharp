using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Utility
{
    public static class HttpClientFactory
    {
        private static string token = "";

        public static HttpClient GetJsonClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpClient GetAuthenticatedClient()
        {
            HttpClient client = GetJsonClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public static void SetToken(string token)
        {
            HttpClientFactory.token = token;
        }

    }
}
