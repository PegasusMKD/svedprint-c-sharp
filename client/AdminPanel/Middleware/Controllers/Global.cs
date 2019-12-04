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
    /// <summary>
    /// <para>This class is for functions which are used only once,</para>
    /// and it doesn't really fit into any certain model, nor is it a Utility which could be used multiple times
    /// </summary>
    public class Global
    {
        /// <summary>
        /// A Controller which sets off the Threads for a new school year on the server-side
        /// </summary>
        /// <param name="admin"></param>
        /// <returns>True if the threads were activated and a back up was made</returns>
        public static bool TransferYear(Models.Admin admin)
        {
            var json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token }
            });
            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateStudentsTransferYearRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            var deserialized = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.responseText);

            if (deserialized["status_code"] == "005") return true;
            else return false;
        }
    }
}
