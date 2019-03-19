using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private static readonly HttpClient http = new HttpClient();

        // DONE
        public static async Task<List<Dictionary<string, string>>> GetDataAsync(Dictionary<string, string> queryParams, string scope)
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

            var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
            var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

            //var response = await http.PostAsync(uri, new StringContent(json));
            //string responseJson = await response.Content.ReadAsStringAsync();

            List<Dictionary<string, string>> queryResult = new List<Dictionary<string, string>>();
            queryResult = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseJson, new DictConverter());
            return queryResult;
        }

        // TODO
        public static async Task AddDataAsync(Dictionary<string, string> queryParams, string scope)
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
        public const string AddUcenici = "ucenici";
    }

    public class RequestParameters
    {
        public const string ime = "first_name";
        public const string srednoIme = "middle_name";
        public const string prezime = "last_name";
        public const string paralelka = "paralelka";
        public const string token = "token";
        
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
            writer.WriteStartObjectAsync();
            writer.WritePropertyNameAsync(value._type);
            writer.WriteValueAsync(value._scope);
            foreach (KeyValuePair<string, string> x in value._queryParams)
            {
                writer.WritePropertyNameAsync(x.Key);
                writer.WriteValueAsync(x.Value);
            }
            writer.WriteEndObjectAsync();
            //serializer.Serialize(writer, value._queryParams);
            //writer.WriteRawAsync(@"}");
            //writer.WriteValueAsync(value._params);
            // TODO: make custom serializer to leave out null values instead of sending empty values
        }
    }

    class DictConverter : JsonConverter<List<Dictionary<string, string>>>
    {
        public override List<Dictionary<string, string>> ReadJson(JsonReader reader, Type objectType, List<Dictionary<string, string>> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            List<Dictionary<string, string>> pairs = new List<Dictionary<string, string>>();
            string property, value;
            JObject x;
            do
            {
                if (reader.TokenType == JsonToken.StartObject)
                {
                    Dictionary<string, string> pair = new Dictionary<string, string>();
                    x = JObject.Load(reader);
                    foreach(JToken k in x.PropertyValues())
                    {
                        if(k.Path == "oceni")
                        {
                            List<int> l = k.ToObject<List<int>>();
                            pair[k.Path] = String.Join<int>(" ", l);
                        } else
                        {
                            pair[k.Path] = k.ToObject<string>().ToString();
                        }
                    }
                    pairs.Add(pair);
                }
            } while (reader.Read());

            return pairs;
        }

        public override void WriteJson(JsonWriter writer, List<Dictionary<string, string>> value, JsonSerializer serializer)
        {
            
        }
    }
}
