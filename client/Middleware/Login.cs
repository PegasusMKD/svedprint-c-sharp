using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace Middleware
{
    public class Login
    {
        private static readonly HttpClient http = new HttpClient();
        public static async System.Threading.Tasks.Task<Klasen> LoginWithCredAsync(string username, string password)
        {
            string uri = $"{settings.Default.DB_HTTP}://{settings.Default.DB_HOST}{settings.Default.DB_PORT}/{settings.Default.DB_BRANCH}/login/";
            //string uri = string.Format(@"https://{0}{1}/stable/login/", settings.Default.DB_HOST, settings.Default.DB_PORT);
            string loginJson = JsonConvert.SerializeObject(new Dictionary<string, string>()
            {
                {"user", username},
                {"pass", password}
            });

            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            Klasen klasen = new Klasen();
            try
            {
                using (var writer = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    writer.Write(loginJson);
                }

                var httpResponse = (HttpWebResponse) await httpRequest.GetResponseAsync();
                var responseJson = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();

                // Console.WriteLine(JToken.Parse(responseJson).ToString(Formatting.Indented));
                
                try
                {
                    klasen = JsonConvert.DeserializeObject<Klasen>(responseJson);
                    // Console.WriteLine(klasen._ime);
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    klasen._ime = "100";
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
            return klasen;
        }

        public static string ServerBranch => settings.Default.DB_BRANCH;
    }
}
