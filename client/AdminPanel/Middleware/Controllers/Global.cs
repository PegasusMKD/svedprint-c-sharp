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

    public class Global
    {
        public static bool TransferYear(Models.Admin admin)
        {
            var json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token }
            });
            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateUsersRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            if (response.responseText == "005") return true;
            else return false;
        }
    }
}
