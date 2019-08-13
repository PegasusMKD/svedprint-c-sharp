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
            if(string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception(Properties.ExceptionMessages.MissingLoginInfoMessage);
            }
            Admin tmp;
            Controllers.Admin.RetrieveAdminData(this, password, out tmp);
            
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
        /// <summary>
        /// dictionary od godina i lista od klasni za taa godina
        /// </summary>
        public Dictionary<string, List<Klasen>> GetUsers()
        {
            try
            {
                Dictionary<string, List<Klasen>> retval = Controllers.Admin.RetrieveUsers(this);
                return retval;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
