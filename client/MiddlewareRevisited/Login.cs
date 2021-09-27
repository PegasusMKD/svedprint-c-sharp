using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited
{
    public class Login
    {

        public static async Task<string> httpClientLogin(string username, string password)
        {
            var account = new Dictionary<string, string>();
            account.Add("grant_type", "password");
            account.Add("username", username);
            account.Add("password", Utility.Encryption.EncryptSHA256(password));

            using (HttpClient httpClient = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/oauth/token");
                var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes("svedprintclientid:FHSs82BbPzagP"));
                data.Headers.Add("Authorization", $"Basic {base64authorization}");

                data.Content = new FormUrlEncodedContent(account);

                HttpResponseMessage responseMessage = await httpClient.SendAsync(data);
                if (!responseMessage.IsSuccessStatusCode) throw new Exception(await responseMessage.Content.ReadAsStringAsync());
                string str = await responseMessage.Content.ReadAsStringAsync();

                try
                {
                    return JsonConvert.DeserializeObject<JObject>(str)["access_token"].ToString();
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
}
