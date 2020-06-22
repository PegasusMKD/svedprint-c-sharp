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
        public static User LoginWithCredentials(string username, string password)
        {
            User u = new User();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://34.107.121.20:8080/api/teachers");
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add("password", password);
            request.ContentType = "application/json";
            JObject requestBody = new JObject();
            requestBody.Add("username", username);

            using (var dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(requestBody.ToString());
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string responseJson;
            using (var dataStream = new StreamReader(response.GetResponseStream()))
            {
                responseJson = dataStream.ReadToEnd();
            }

            u = JsonConvert.DeserializeObject<User>(responseJson);

            return u;
        }
    }
}
