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
    class Uchilishte
    {
        ///<summary>
        ///<para>Controller for updating the school server-side</para>
        ///It uses the same technique as the one for the Klasen, with the changed_properties and so on
        ///</summary>
        public static int UpdateSchool(Models.Admin admin,Models.Uchilishte school)
        {
            
            Dictionary<string, object> tmp_dict = new Dictionary<string, object>()
            {
                {JSONRequestParameters.Token, admin.Token },
            };


            if (school.changed_properties.Count() != 0)
            {
                Type T = typeof(Models.Klasen);

                var properties = T.GetProperties().Where(p => school.changed_properties.Contains(p.Name));

                foreach (var p in properties)
                {
                    var val = p.GetValue(school);
                    if (val != null)
                    {
                        tmp_dict[school.pairs[p.Name]] = val.ToString();
                    }
                }

                school.changed_properties.Clear();
            }
            else return -1;

            string json = JsonConvert.SerializeObject(tmp_dict);

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateUsersRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);


            return 0;
        }

        public static int UpdateDates(Models.Admin admin, Models.Uchilishte school)
        {

            Dictionary<string, object> tmp_dict = new Dictionary<string, object>()
            {
                {JSONRequestParameters.Token, admin.Token },
                {JSONRequestParameters.Admin.DataMatura, school.DataMatura },
                {JSONRequestParameters.Admin.DataSveditelstva, school.DataSveditelstva }

            };


            string json = JsonConvert.SerializeObject(tmp_dict);

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateDatesRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);


            return 0;
        }

    }
}
