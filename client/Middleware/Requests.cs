using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Middleware
{
    public class Requests
    {
        private static readonly HttpClient http = new HttpClient();

        // DONE
        public static async Task<List<Dictionary<string,string>>> GetDataAsync(Dictionary<string,string> queryParams, string scope)
        {
            Request request = new Request(type: RequestTypes.GET, scope: scope, queryParams: queryParams);

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            string uri = string.Format(@"http://{0}:{1}/main/return/", settings.Default.DB_HOST, settings.Default.DB_PORT);
            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.Method = @"POST";
            httpRequest.ContentType = @"application/json";
            using (var writer = new StreamWriter(await httpRequest.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(json);
                await writer.FlushAsync();
                writer.Close();
            }

            var httpResponse = (HttpWebResponse) await httpRequest.GetResponseAsync();
            var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

            //var response = await http.PostAsync(uri, new StringContent(json));
            //string responseJson = await response.Content.ReadAsStringAsync();

            List<Dictionary<string,string>> queryResult = new List<Dictionary<string,string>>();
            queryResult = JsonConvert.DeserializeObject<List<Dictionary<string,string>>>(responseJson);
            return queryResult;
        }

        // TODO
        public static async Task AddDataAsync(Dictionary<string,string> queryParams, string scope)
        {
            Request request = new Request(type: RequestTypes.ADD, scope: scope, queryParams: queryParams);

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            string uri = string.Format(@"http://{0}:{1}/main/setup/", settings.Default.DB_HOST, settings.Default.DB_PORT);
            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.Method = @"POST";
            httpRequest.ContentType = @"application/json";
            using (var writer = new StreamWriter(await httpRequest.GetRequestStreamAsync()))
            {
                await writer.WriteAsync(json);
                await writer.FlushAsync();
                writer.Close();
            }

            var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
            var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

        }
    }

    public class RequestScopes
    {
        public const string GetCelSmer = "cel_smer";
        public const string GetParalelka = "paralelka";
        public const string GetUcenik = "ucenik";
        public const string GetSmerVoKlas = "smer_vo_klas";
        public const string GetPredmetiSmer = "smer";
    }

    public class RequestParameters
    {
        //public const string 
    }
    //_queryParams : { "ime" : "asfasf", ... }
    class RequestConverter : JsonConverter<Request>
    {
        public override Request ReadJson(JsonReader reader, Type objectType, Request existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Request value, JsonSerializer serializer)
        {
            //writer.WriteRawAsync(@"{");
            writer.WritePropertyNameAsync(value._type);
            writer.WriteValueAsync(value._scope);
            foreach(KeyValuePair<string,string> x in value._queryParams)
            {
                writer.WritePropertyNameAsync(x.Key);
                writer.WriteValueAsync(x.Value);
            }

            //serializer.Serialize(writer, value._queryParams);
            //writer.WriteRawAsync(@"}");
            //writer.WriteValueAsync(value._params);
            // TODO: make custom serializer to leave out null values instead of sending empty values
        }
    }
}
