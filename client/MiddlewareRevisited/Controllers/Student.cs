using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        // TASK<Models.Student> -> Task zoso ne go koristis returnatiot Student
        // i plus vekje go imas updatenato lokalno Student-ot
        public static async Task<Models.Student> updateStudent(Models.Student student, Models.User currentUser)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var data = new HttpRequestMessage(HttpMethod.Put, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students");
            var json = JsonConvert.SerializeObject(student);
            data.Headers.Add("token", currentUser.token);
            data.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var ret = await httpClient.SendAsync(data);
            if (!ret.IsSuccessStatusCode) throw new Exception("status code poopy");
            return JsonConvert.DeserializeObject<Models.Student>(await ret.Content.ReadAsStringAsync());
        }
    }
}
