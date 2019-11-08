using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Controllers
{
    static class Util
    {
        public static (string responseText, HttpStatusCode responseCode) GetWebResponse(string payload, string route)
        {
            HttpWebRequest request = WebRequest.CreateHttp(Properties.Resources.ServerURI + route);
            request.Method = "POST";
            request.ContentType = "application/json";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(payload);
            }

            string returnValue = "";
            HttpStatusCode responseCode;

            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    responseCode = response.StatusCode;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        returnValue = reader.ReadToEnd();
                    }
                }
            } catch (WebException ex)
            {
                responseCode = ((HttpWebResponse)ex.Response).StatusCode;
                Debug.WriteLine(ex.Message + " " + responseCode.ToString());
            }

            return (returnValue, responseCode);
        }

        public static Boolean isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }
    }

}
