using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware
{
    public class Requests
    {
        private static HttpClient http = new HttpClient();

        // DONE
        public static async Task<List<Ucenik>> GetUceniciAsync(Ucenik queryParams, string scope)
        {
            Request request = new Request(type: RequestTypes.GET, scope: scope, queryParams: queryParams);

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            //Console.WriteLine("{0}: {1}", "getucenici", json);

            string uri = string.Format(@"http://{0}:{1}/main/return", settings.Default.DB_HOST, settings.Default.DB_PORT);
            //string uri = "https://webhook.site/27a1b91a-81ed-458b-a54c-cbd813691c48";
            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            using (var writer = new StreamWriter(await httpRequest.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(json);
                await writer.FlushAsync();
                writer.Close();
            }

            var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
            var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

            //var response = await http.PostAsync(uri, new StringContent(json));
            //string responseJson = await response.Content.ReadAsStringAsync();

            List<Ucenik> queryResult = new List<Ucenik>();
            queryResult = JsonConvert.DeserializeObject<List<Ucenik>>(responseJson);
            return queryResult;
        }

        // TODO
        public static async Task AddUcenikAsync(Ucenik queryParams, string scope)
        {
            Request request = new Request
                (type: RequestTypes.ADD,
                scope: scope,
                queryParams: queryParams);

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

    class RequestType
    {
        public const string GetCelSmer = "cel_smer";
        public const string GetParalelka = "paralelka";
        public const string GetUcenik = "ucenik";
        public const string GetSmerVoKlas = "smer_vo_klas";
        public const string GetPredmetiSmer = "smer";
    }

    class RequestConverter : JsonConverter<Request>
    {
        public override Request ReadJson(JsonReader reader, Type objectType, Request existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Request value, JsonSerializer serializer)
        {
            writer.WriteRawAsync(@"{");
            writer.WritePropertyNameAsync(value._type);
            writer.WriteValueAsync(value._scope);
            writer.WritePropertyNameAsync("_params");
            serializer.Converters.Add(new UcenikConverter());
            serializer.Serialize(writer, value._queryParams);
            writer.WriteRawAsync(@"}");
            //writer.WriteValueAsync(value._params);
            // TODO: make custom serializer to leave out null values instead of sending empty values
        }
    }

    class UcenikConverter : JsonConverter<Ucenik>
    {
        public override Ucenik ReadJson(JsonReader reader, Type objectType, Ucenik existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Ucenik value, JsonSerializer serializer)
        {
            writer.WriteRawAsync(JsonConvert.SerializeObject(value));
        }
    }
}
