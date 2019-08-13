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
        [JsonProperty("school_obj")]
        public Uchilishte Uchilishte { get; set; }

        public Admin(string username = "") => Username = username;

        public void RetrieveData(string password)
        {
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);
            }
            Admin tmp;
            Controllers.Admin.RetrieveData(this, password, out tmp);
            
            Type T = typeof(Admin);
            var properties = T.GetProperties().Where(p => p.Name != "Username");

            foreach (var p in properties)
            {
                var val = p.GetValue(tmp, null);
                if(val != null)
                {
                    p.SetValue(this, val, null);
                }
            }
        }

        public void UpdateData(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(password))
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);

            Controllers.Admin.UpdateData(this,password);

        }
    }
}
