using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void GetAdminData(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);
            }
            Admin tmp;
            Controllers.Admin.RetrieveData(this, password, out tmp);

            Util.UpdateObject<Models.Admin>(tmp, this, new List<string> { "Username" });
        }

        

        public void UpdateData(string password)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);

            Controllers.Admin.UpdateData(this,password);

            //Util.UpdateObject<Models.Admin>(tmp, this);


        }
    }
}
