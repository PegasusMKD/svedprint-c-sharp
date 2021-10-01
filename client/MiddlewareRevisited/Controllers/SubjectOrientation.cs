using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MiddlewareRevisited.Utility;

namespace MiddlewareRevisited.Controllers
{
    public class SubjectOrientation
    {

        public static async Task<Models.SubjectOrientation> AddSubjectOrientation(Models.SubjectOrientation subjectOrientation)
        {
            using(HttpClient httpClient = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Post, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/subjectOrientations");
                var json = JsonConvert.SerializeObject(subjectOrientation);
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(data);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.SubjectOrientation>(await response.Content.ReadAsStringAsync());
            }
            
        }


        public static async Task RemoveSubjectOrientation(Models.SubjectOrientation subjectOrientation)
        {
            using (HttpClient httpClient = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Delete, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/subjectOrientations");
                var json = JsonConvert.SerializeObject(subjectOrientation);
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                await httpClient.SendAsync(data);
            }
                
        }

        public static async Task<List<Models.SubjectOrientation>> GetSubjectOrientations()
        {
            using (HttpClient httpClient = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Get, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/subjectOrientations");
                var ret = await httpClient.SendAsync(data);
                return JsonConvert.DeserializeObject<List<Models.SubjectOrientation>>(await ret.Content.ReadAsStringAsync());
            }
                
        }

        public static async Task<Models.SubjectOrientation> UpdateSubjectOrientation(Models.SubjectOrientation subjectOrientation)
        {
            using (HttpClient httpClient = HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Put, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/subjectOrientations");
                var json = JsonConvert.SerializeObject(subjectOrientation); // This should be just the fields that should get updated and shortName, nothing more, nothing less
                data.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.SendAsync(data);
                if (!response.IsSuccessStatusCode) throw new Exception(await response.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.SubjectOrientation>(await response.Content.ReadAsStringAsync());
            }
                
        }
    }
}
