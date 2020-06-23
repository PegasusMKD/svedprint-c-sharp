using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited
{
    public class Login
    {
        public static async Task<User> LoginWithCredentialsAsync(string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://35.234.92.150:8080/api/teachers");
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
    }
}
