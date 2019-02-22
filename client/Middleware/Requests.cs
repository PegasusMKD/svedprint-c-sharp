using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.IO;

namespace Middleware
{
    public class Requests
    {
        private static HttpClient http = new HttpClient();
        

        public static async Task<List<Ucenik>> GetUceniciAsync(Ucenik queryParams, string scope)
        {
            Request request = new Request
            {
                _type = RequestTypes.GET,
                _scope = scope,
                _params = queryParams
            };

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            Console.WriteLine("{0}: {1}", "getucenici", json);

            //string uri = string.Format(@"http://{0}:{1}/main/return", settings.Default.DB_HOST, settings.Default.DB_PORT);
            //var req = (HttpWebRequest)WebRequest.Create(uri);
            //req.Method = "POST";
            //req.ContentType = "application/json";

            //var response = await http.PostAsync(uri, new StringContent(json));
            //string responseJson = await response.Content.ReadAsStringAsync();

            List<Ucenik> queryResult = new List<Ucenik>();
            //queryResult = JsonConvert.DeserializeObject<List<Ucenik>>(responseJson);
            return queryResult;
        }

        public static async Task AddUcenikAsync(Ucenik queryParams, string scope)
        {
            Request request = new Request
            {
                _type = RequestTypes.ADD,
                _scope = scope,
                _params = queryParams
            };

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            Console.WriteLine("{0}: {1}", "adddata", json);

            //string uri = string.Format(@"http://{0}:{1}/main/setup", settings.Default.DB_HOST, settings.Default.DB_PORT);
            //var req = (HttpWebRequest)WebRequest.Create(uri);
            //req.Method = "POST";
            //req.ContentType = "application/json";

            //var response = await http.PostAsync(uri, new StringContent(json));
            //string responseJson = await response.Content.ReadAsStringAsync();
        }
    }

    class RequestConverter : JsonConverter<Request>
    {
        public override Request ReadJson(JsonReader reader, Type objectType, Request existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Request value, JsonSerializer serializer)
        {
            writer.WritePropertyNameAsync(value._type);
            writer.WriteValueAsync(value._scope);
            writer.WritePropertyNameAsync("params");
            writer.WriteValueAsync(value);
            // TODO: make custom serializer to leave out null values instead of sending empty values
        }
    }
}
