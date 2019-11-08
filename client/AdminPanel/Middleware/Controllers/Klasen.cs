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

    public class Klasen
    {
        public static Dictionary<string, List<Models.Klasen>> RetrieveUsers(Models.Admin admin)
        {
            Dictionary<string, List<Models.Klasen>> tmp;
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                {JSONRequestParameters.Token, admin.Token }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.RetrieveUsersRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);

            try
            {
                tmp = JsonConvert.DeserializeObject<Dictionary<string, List<Models.Klasen>>>(response.responseText);
                foreach(var list in tmp.Values)
                { 
                    foreach(var user in list)
                    {
                        user.UsernamePERMA = user.Username;
                    }
                }
            }
            catch (Exception ex)
            {
                tmp = null;
                throw ex;
            }

            return tmp;
        }


        public static int UpdateUsers(Models.Admin admin, Dictionary<string, List<Models.Klasen>> users)
        {
            List<Dictionary<string, string>> tmp = new List<Dictionary<string, string>>();
            foreach (KeyValuePair<string, List<Models.Klasen>> year in users)
            {

                foreach (Models.Klasen user in year.Value)
                {
                    Dictionary<string, string> tmp_dict = new Dictionary<string, string>() {
                        {JSONRequestParameters.Klasen.Username, user.UsernamePERMA}
                    };


                    if (user.changed_properties.Count() != 0)
                    {
                        Type T = typeof(Models.Klasen);

                        if (user.changed_properties.Contains("Username")) user.UsernamePERMA = user.Username;
                        var properties = T.GetProperties().Where(p => user.changed_properties.Contains(p.Name));

                        foreach (var p in properties)
                        {
                            var val = p.GetValue(user);
                            if (val != null)
                            {
                                tmp_dict[user.pairs[p.Name]] = val.ToString();
                            }
                        }

                        tmp.Add(tmp_dict);
                        user.changed_properties.Clear();
                    }
                }
            }

            if (tmp.Count() == 0) return -1;

            string json = JsonConvert.SerializeObject(new Dictionary<string, object>
            {
                {JSONRequestParameters.Token, admin.Token },
                {JSONRequestParameters.Klasen.UsersUpdate, tmp }
            });

            (string responseText, HttpStatusCode responseCode) response = Util.GetWebResponse(json, Properties.Resources.UpdateUsersRoute);
            if (string.IsNullOrWhiteSpace(response.responseText) || response.responseCode != HttpStatusCode.OK) throw new Exception(Properties.ExceptionMessages.InvalidDataMessage);


            return 0;
        }
    }
}
