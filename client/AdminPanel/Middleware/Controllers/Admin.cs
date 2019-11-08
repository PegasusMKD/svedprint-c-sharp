using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AdminPanel.Middleware.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdminPanel.Middleware.Controllers
{
    static class Admin
    {
        public static void RetrieveData(Models.Admin admin, string password, out Models.Admin retval)
        {
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Admin.Username, admin.Username},
                {JSONRequestParameters.Admin.Password, password}
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.LoginRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            try
            {
                retval = JsonConvert.DeserializeObject<Models.Admin>(response.responseText, new AdminConverter());
            } catch (Exception ex)
            {
                retval = null;
                throw ex;
            }
        }

        
        public static void UpdateData(Models.Admin admin, string password)
        {
            var json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token },
                {JSONRequestParameters.Admin.UsernameUpdated, admin.Username },
                {JSONRequestParameters.Admin.PasswordUpdated, password }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateAdminRoute);

            Dictionary<string, string> retval = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.responseText);

            if (retval["status_code"] != "000")
            {
                switch (retval["status_code"])
                {
                    case "016":
                        throw new Exception(Properties.ExceptionMessages.GeneralError);
                }
            }
        }
    }


    class AdminConverter : JsonConverter<Models.Admin>
    {
        public override Models.Admin ReadJson(JsonReader reader, Type objectType, Models.Admin existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Models.Admin admin = new Models.Admin();
            admin.Uchilishte = new Models.Uchilishte();

            JObject obj = JObject.Load(reader);
            if(obj.Properties().Where(x => x.Name == "status_code" && x.Value.Value<string>() != "000").Count() > 0)
            {
                switch(obj.Property("status_code").Value.Value<string>())
                {
                    case "001":
                        throw new Exception(Properties.ExceptionMessages.IncorrectPasswordMessage); // invalid password
                    case "002":
                        throw new Exception(Properties.ExceptionMessages.UserNotFoundMessage); // user not found
                }
                return null;
            }

            var output = JsonConvert.DeserializeObject<Dictionary<string, object>>(obj.ToString());

            serializer.Populate(obj.CreateReader(), admin);

            return admin;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, Models.Admin value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

       
    }
}
