using MiddlewareRevisited.Models;
using MiddlewareRevisited.Models.Meta;
using MiddlewareRevisited.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
        public static async Task<Models.Student> UpdateStudent(Models.Student student)
        {
            using (HttpClient httpClient = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Put, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students");
                var json = JsonConvert.SerializeObject(student);
                
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var ret = await httpClient.SendAsync(data);
                if (!ret.IsSuccessStatusCode) throw new Exception(await ret.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.Student>(await ret.Content.ReadAsStringAsync());
            }
        }

        public static async Task<Models.Student> GetStudentByIdAsync(string studentId)
        {
            Models.Student s;
            using (var http = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students/getOne");

                data.Content = new StringContent(studentId, Encoding.UTF8, MediaTypeNames.Text.Plain);
                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                s = JsonConvert.DeserializeObject<Models.Student>(await response.Content.ReadAsStringAsync());
            }

            return s;
        }

        public static async Task<List<Models.Student>> GetAllStudentsShortAsync()
        {
            PageResponse<Models.Student> page;
            using (HttpClient http = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students/page");
                // vvvvv
                // data.Content = new StringContent(null, Encoding.UTF8, "application/json");

                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    page = JsonConvert.DeserializeObject<PageResponse<Models.Student>>(await response.Content.ReadAsStringAsync());
                } catch(JsonSerializationException e)
                {
                    page = null;
                    Debug.WriteLine(e.Message);
                }
            }

            return page.content;
        }


        public static async Task<List<Models.Student>> GetAllStudentsFullAsync()
        {
            List<Models.Student> items;
            using (HttpClient http = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Get, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/students");

                var response = await http.SendAsync(data).ConfigureAwait(false);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                try
                {
                    items = JsonConvert.DeserializeObject<List<Models.Student>>(await response.Content.ReadAsStringAsync());
                }
                catch (JsonSerializationException e)
                {
                    items = null;
                    Debug.WriteLine(e.Message);
                }
            }

            return items;
        }
    }
}
