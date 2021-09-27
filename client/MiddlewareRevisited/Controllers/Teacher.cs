using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Controllers
{
    public static class Teacher
    {
        public static async Task<Models.User> GetUserByAuthorization()
        {
            using (HttpClient httpClient = Utility.HttpClientFactory.GetAuthenticatedClient())
            {
                var data = new HttpRequestMessage(HttpMethod.Get, $"http://{Properties.Settings.Default.DB_HOST}:8080/api/teachers");
                var ret = await httpClient.SendAsync(data);
                if (!ret.IsSuccessStatusCode) throw new Exception(await ret.Content.ReadAsStringAsync());
                return JsonConvert.DeserializeObject<Models.User>(await ret.Content.ReadAsStringAsync());
            }
        }
    }
}
