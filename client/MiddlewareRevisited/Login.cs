using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited
{
    public class Login
    {

        private static HttpClient httpClient = new HttpClient();

/*      public static async Task<User> LoginWithCredentialsAsync(string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://34.107.121.20:8080/api/teachers");
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add("password", password);
            request.ContentType = "application/json";
            request.Proxy = null;

            using (var dataStream = new StreamWriter(await request.GetRequestStreamAsync()))
            {
                JObject requestBody = new JObject();
                requestBody.Add("username", username);
                await dataStream.WriteAsync(requestBody.ToString());
            }

            User u = null;

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
            {

                string responseJson;
                using (var dataStream = new StreamReader(response.GetResponseStream()))
                {
                    responseJson = await dataStream.ReadToEndAsync();
                }

                u = JsonConvert.DeserializeObject<User>(responseJson);
            }
            return u;
        }
DEPRECATED */

        public static async Task<User> httpClientLogin(string username, string password)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Post, "http://35.234.92.150:8080/api/teachers");
            // 35.234.92.150; 34.107.121.20
            // var json = JsonConvert.SerializeObject(new Dictionary<string, string>() { {"username", username } });
            var json = new JObject();
            json.Add("username", username);
            data.Headers.Add("password", password);
            data.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            User u;
            try
            {
                var responseMessage = await httpClient.SendAsync(data);
                responseMessage.EnsureSuccessStatusCode();
                var str = await responseMessage.Content.ReadAsStringAsync();
                u = JsonConvert.DeserializeObject<User>(str);
                return u;
            }  catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("-- END OF EXCEPTION --");
                throw ex;
            }
        }
    }
}
