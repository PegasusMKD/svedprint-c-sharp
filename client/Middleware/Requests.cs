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

        public static async Task UpdateDataAsync(Dictionary<string, string> queryParams, string scope) {
            Request request = new Request(type: RequestTypes.UPDATE, scope: scope, queryParams: queryParams);

            string json = JsonConvert.SerializeObject(request, new RequestConverter());
            string uri = string.Format(@"http://{0}:{1}/main/update/", settings.Default.DB_HOST, settings.Default.DB_PORT);
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
        public const string UpdateKlasen = "user";
        public const string UpdateParalelka = "paralelka";        
        public const string UpdateSmer = "smer";
        public const string UpdateUcenik = "ucenik";
        public const string UpdateUchilishte = "uchilishte";
    }

    public class RequestParameters
    {
        public const string ime = "first_name";
        public const string srednoIme = "middle_name";
        public const string prezime = "last_name";
        public const string paralelka = "paralelka";
        public const string token = "token";
        public const string smer = "smer";
        public const string oceni = "oceni";
        public const string broj = "broj";
        public const string predmeti = "predmeti";
        public const string ucenici = "ucenici";
        public const string klasen = "klasen";
        public const string uchilishte = "uchilishte";
        public const string new_school = "new_school";
        public const string new_password = "new_password";
        public const string smer_add = "smer_add";
        public const string smer_remove = "smer_remove";
        public const string godina = "godina";
        public const string godishte = "godishte";
        public const string new_smer = "new_smer";
        public const string new_first_name = "new_first_name";
        public const string new_middle_name = "new_middle_name";
        public const string new_last_name = "new_last_name";
        public const string new_first_name = "new_first_name";
        public const string new_broj_vo_dnevnik = "new_broj_vo_dnevnik";
        public const string roditel = "roditel";
        public const string roden = "roden";
        public const string mesto = "mesto";
        public const string opravdani = "opravdani";
        public const string neopravdani = "neopravdani";
        public const string tip = "tip";
        public const string povedenie = "povedenie";


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
