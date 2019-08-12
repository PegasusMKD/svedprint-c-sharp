using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Middleware.Models;
using Newtonsoft.Json;

namespace AdminPanel.Middleware.Controllers
{
    static class Admin
    {
        public static void RetrieveData(Models.Admin admin, string password)
        {
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Admin.Username, admin.Username},
                {JSONRequestParameters.Admin.Password, password}
            });
            
            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json);
            if (string.IsNullOrWhiteSpace(response.responseText)) throw new Exception("error");
        }
    }
}
