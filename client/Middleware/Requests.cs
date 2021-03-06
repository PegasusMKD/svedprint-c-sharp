using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Middleware
{
    public class Requests
    {
        private static readonly HttpClient http = new HttpClient();

        public static List<Dictionary<string, string>> GetData(Dictionary<string, string> queryParams, string scope)
        {
            try
            {
                Request request = new Request(type: RequestTypes.GET, scope: scope, queryParams: queryParams);

                string json = JsonConvert.SerializeObject(request, new RequestConverter());
                string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/return/";
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Method = @"POST";
                httpRequest.ContentType = @"application/json";
                using (var writer = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    writer.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var responseJson = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

                //var response = await http.PostAsync(uri, new StringContent(json));
                //string responseJson = await response.Content.ReadAsStringAsync();

                List<Dictionary<string, string>> queryResult = new List<Dictionary<string, string>>();
                queryResult = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseJson, new DictConverter());
                // printData(json, responseJson);

                return queryResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return new List<Dictionary<string, string>>();
            }
        }

        [Conditional("DEBUG")]
        private static void printData(string json, string responseJson)
        {
            Console.WriteLine("GetData:");
            Console.WriteLine(String.Format("Request:{0}{1}", Environment.NewLine, JToken.Parse(json).ToString(Formatting.Indented)));
            Console.WriteLine(String.Format("Response:{0}{1}", Environment.NewLine, JToken.Parse(responseJson).ToString(Formatting.Indented)));
        }

        public static void AddData(Dictionary<string, string> queryParams, string scope)
        {
            try
            {

                Request request = new Request(type: RequestTypes.ADD, scope: scope, queryParams: queryParams);

                string json = JsonConvert.SerializeObject(request, new RequestConverter());
                string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/setup/";
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Method = @"POST";
                httpRequest.ContentType = @"application/json";
                using (var writer = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    writer.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var responseJson = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static async Task<string> UpdateDataAsync(Dictionary<string, string> queryParams, string scope)
        {
            try
            {
                Request request = new Request(type: RequestTypes.UPDATE, scope: scope, queryParams: queryParams);

                string json = JsonConvert.SerializeObject(request, new RequestConverter());
                string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/update/";
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Method = @"POST";
                httpRequest.ContentType = @"application/json";
                using (var writer = new StreamWriter(await httpRequest.GetRequestStreamAsync()))
                {
                    await writer.WriteAsync(json);
                }

                var httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();
                var responseJson = await new StreamReader(httpResponse.GetResponseStream()).ReadToEndAsync();

                return responseJson.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "107";
            }
        }

        public static string GetDelovoden(Dictionary<string, string> queryParams)
        {
            try
            {
                string json = JsonConvert.SerializeObject(queryParams);
                string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/get_delovoden/";
                var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpRequest.Method = @"POST";
                httpRequest.ContentType = @"application/json";
                using (var writer = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    writer.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var responseJson = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

                var zz = JToken.FromObject(JsonConvert.DeserializeObject(responseJson));
                int k = zz.First.First.Value<int>();

                return k.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "107";
            }
        }

        public static void GetCarsav(string carsav_type)
        {
            // 1. Request do serverot da mi vrati najrecent excel
            // 2. Da se zapise requestot vo file
            // https://stackoverflow.com/questions/2934295/c-sharp-save-a-file-from-a-http-request

            string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/{carsav_type}/";

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
        public const string GetKlasCarsav = "carsav";
        public const string GetFullCarsav = "fullcarsav";
    }
    
    public class RequestParameters
    {
        public const string ime = "firstName";
        public const string srednoIme = "middleName";
        public const string prezime = "lastName";
        public const string paralelka = "subjectOrientationName";
        public const string token = "token";
        public const string smer = "subjectOrientationName";
        public const string oceni = "oceni";
        public const string broj = "number";
        public const string predmeti = "predmeti";
        public const string ucenici = "ucenici";
        public const string klasen = "klasen";
        public const string ucilishte = "ucilishte";
        public const string grad = "grad";
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
        public const string new_broj_vo_dnevnik = "new_broj_vo_dnevnik";
        public const string opravdani = "justifiedAbsences";
        public const string neopravdani = "unjustifiedAbsences";
        //Dodatoci od Pazzio
        public const string mesto_na_zhiveenje = "placeOfResidence";
        public const string mesto_na_ragjanje = "placeOfBirth";
        public const string tatko = "fathersName";
        public const string majka = "mothersName";
        public const string prethoden_delovoden = "prethoden_delovoden_broj";
        public const string pat_polaga_ispit = "pat_polaga_ispit";
        public const string ispiten = "ispiten_rok";
        public const string pat_polaga = "timesStudiedYear";
        public const string cel_smer = "cel_smer";
        public const string action = "action";
        public const string delete = "del";
        public const string odobreno_sveditelstvo = "odobreno_sveditelstvo";
        public const string mesto_odobreno_sveditelstvo = "mesto_odobreno_sveditelstvo";
        public const string ministerstvo = "ministerstvo";
        public const string jazik = "jazik";
        public const string jazik_ocena = "jazik_ocena";
        //
        public const string tip = "tip_obrazovanie";
        public const string povedenie = "behaviorType";
        public const string roden = "dateOfBirth";
        public const string gender = "gender";
        public const string maturska = "maturska";
        public const string izborni = "izborni";
        public const string proektni = "optionalSubjects";
        public const string merki = "merki";
        public const string prethodna_godina = "lastGradeYear";
        public const string prethoden_uspeh = "lastSchoolYearSuccessType";
        public const string prethodno_uchilishte = "lastSchoolName";
        public const string delovoden_broj = "delovoden_broj";
        public const string datum_sveditelstvo = "datum_na_sveditelstvo";
        public const string polozhil = "passedYear";
        public const string prethodna_uchebna = "prethodna_uchebna";
        public const string pedagoshki_merki = "merki";
        public const string direktor = "direktor";
        public const string drzavjanstvo = "drzavjanstvo";
        public const string akt_godina = "akt_godina";
        public const string akt = "akt";
        public const string duplicate_ctr = "ctr";
        public const string glavna_kniga = "glavna_kniga";
        public const string drzava = "drzava";

        // gk_diploma
        public const string eksterni = "eksterni";
        public const string interni = "interni";
        public const string percentilen = "percentilen";
        public const string delovoden_predmeti = "delovoden_predmeti";
        public const string polagal= "polagal";
        public const string polozhil_matura = "polozhil_matura";
    }



    class RequestConverter : JsonConverter<Request>
    {
        public override Request ReadJson(JsonReader reader, Type objectType, Request existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Request value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(value._type);
            writer.WriteValue(value._scope);
            foreach (KeyValuePair<string, string> x in value._queryParams)
            {
                writer.WritePropertyNameAsync(x.Key);
                writer.WriteValueAsync(x.Value);
            }
            writer.WriteEndObject();
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
                    foreach (JToken k in x.PropertyValues())
                    {
                        if (k.Path == "oceni")
                        {
                            List<int> l = k.ToObject<List<int>>() ?? new List<int>();
                            pair[k.Path] = String.Join<int>(" ", l);
                        }
                        else
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
            throw new NotImplementedException();
        }
    }
}
