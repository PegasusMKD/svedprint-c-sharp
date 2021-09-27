using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited
{
    public class Login
    {

        private static HttpClient httpClient = new HttpClient();

        public static async Task<User> httpClientLogin(string username, string password)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/teachers");
            var json = new JObject();
            json.Add("username", username);
            data.Headers.Add("password", Utility.Encryption.EncryptSHA256(password));
            data.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage responseMessage = await httpClient.SendAsync(data);
                responseMessage.EnsureSuccessStatusCode();
                string str = await responseMessage.Content.ReadAsStringAsync();
                Debug.WriteLine(str);
                return JsonConvert.DeserializeObject<User>(str);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("-- END OF EXCEPTION --");
                throw ex;
            }
        }
    }
}
