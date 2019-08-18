using AdminPanel.Middleware.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Controllers
{
    class Ucenik
    {
        public static List<Ucenik> RetrieveStudents(Models.Admin admin, string year)
        {
            List<Ucenik> tmp;
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token },
                {JSONRequestParameters.Ucenik.Godina, year }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.RetrieveStudentsRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            try
            {
                tmp = JsonConvert.DeserializeObject<List<Ucenik>>(response.responseText);
            }
            catch (Exception ex)
            {
                tmp = null;
                throw ex;
            }

            return tmp;
        }


    }
}
