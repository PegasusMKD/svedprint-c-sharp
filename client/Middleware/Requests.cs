using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace Middleware
{
    public class Requests
    {
        private static HttpClient http = new HttpClient();
        public static string DodajUcenik(Ucenik u)
        {
            string json = u.Serialize();
            Dictionary<string, string> request = new Dictionary<string, string>()
            {
                {"action", "dodaj" },
                {"ucenik", json }
            };

            //string uri = string.Format(@"http://{0}:{1}/api", settings.Default.DB_HOST, settings.Default.DB_PORT);
            //var req = (HttpWebRequest)WebRequest.Create(uri);
            //req.Method = "POST";
            //req.ContentType = "application/json";

            //var response = await http.PostAsync(uri, new FormUrlEncodedContent(request));
            //return await response.Content.ReadAsStringAsync();

            return JsonConvert.SerializeObject(request);
        }
    }
}
