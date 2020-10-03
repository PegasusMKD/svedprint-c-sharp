using MiddlewareRevisited.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Controllers
{
    public class Student
    {
        // TODO: Rework so it works like AdminPanel middleware 
        // TASK<Models.Student> -> Task zoso ne go koristis returnatiot Student
        // i plus vekje go imas updatenato lokalno Student-ot
        public static async Task<Models.Student> UpdateStudent(Models.Student student, Models.User currentUser)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = new HttpRequestMessage(HttpMethod.Put, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students");
                var json = JsonConvert.SerializeObject(student);
                data.Headers.Add("token", currentUser.token);
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var ret = await httpClient.SendAsync(data);
                if (!ret.IsSuccessStatusCode) throw new Exception(await ret.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.Student>(await ret.Content.ReadAsStringAsync());
            }
        }

        public static async Task<List<Models.Student>> GetAllStudentsShortAsync(Models.User user)
        {
            List<Models.Student> students;

            using (HttpClient http = new HttpClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students/page");
                data.Headers.Add("token", user.token);
                data.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var json = new JObject
                {
                    { "schoolClass", JsonConvert.SerializeObject(user.schoolClass) }
                }.ToString() ;

                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await http.SendAsync(data);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                students = JsonConvert.DeserializeObject<List<Models.Student>>(await response.Content.ReadAsStringAsync());
            }

            return students;
        }
    }
}
