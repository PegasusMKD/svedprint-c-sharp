using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AdminPanel.Middleware;
using System.Security;
using System.Diagnostics;

namespace AdminPanel.Middleware.Models
{
    public class Admin
    {
        [JsonProperty(JSONRequestParameters.Admin.Username)]
        public string Username { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.PrintAllowed)]
        public bool IsPrintAllowed { get; set; }
        [JsonProperty(JSONRequestParameters.Token)]
        public string Token { get; set; }
        public Uchilishte Uchilishte { get; set; }

        public Admin(string username = "") => Username = username;

        public void RetrieveData(string password)
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);
            }
            Controllers.Admin.RetrieveData(this, password);
        }
    }
}
