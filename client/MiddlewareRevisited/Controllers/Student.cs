using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Controllers
{
    public class Student
    {
        // TODO: Rework so it works like AdminPanel middleware 
        public static async Task<Models.Student> updateStudent(Models.Student student, Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Put, "http://34.107.121.20:8080/api/students");
            var json = JsonConvert.SerializeObject(student);
            data.Headers.Add("token", currentUser.token);
            data.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var ret = await httpClient.SendAsync(data);
            return JsonConvert.DeserializeObject<Models.Student>(await ret.Content.ReadAsStringAsync());
        }
    }
}
