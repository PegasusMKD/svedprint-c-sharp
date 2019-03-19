using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Middleware
{
    public class Login
    {
        private static readonly HttpClient http = new HttpClient();
        public static async Task<string> GetAuthToken(string username, string password)
        {
            string uri = string.Format(@"http://{0}:{1}/main/login/", settings.Default.DB_HOST, settings.Default.DB_PORT);
            //string uri = "http://webhook.site/bc72a5ae-7ea1-4145-b0a9-44cac9cde141";
            string loginJson = JsonConvert.SerializeObject(new Dictionary<string, string>()
            {
                {"user", username},
                {"pass", password }
            });

            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var writer = new StreamWriter(httpRequest.GetRequestStream()))
            {
                writer.Write(loginJson);
                writer.Flush();
                writer.Close();
            }

            var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
            var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

            string token = JsonConvert.DeserializeObject<string>(responseJson);
            return token;
        }
    }
}
