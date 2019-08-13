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
        public static void RetrieveData(Models.Admin admin, string password)
        {
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Admin.Username, admin.Username},
                {JSONRequestParameters.Admin.Password, password}
            });
            
            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json);
            if (string.IsNullOrWhiteSpace(response.responseText)) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            Models.Admin tmp = JsonConvert.DeserializeObject<Models.Admin>(response.responseText, new AdminConverter());
        }
    }

    class AdminConverter : JsonConverter<Models.Admin>
    {
        public override Models.Admin ReadJson(JsonReader reader, Type objectType, Models.Admin existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            Models.Admin admin = new Models.Admin();
            JObject obj = JObject.Load(reader);

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
