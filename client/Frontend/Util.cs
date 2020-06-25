using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend;

namespace MiddlewareRevisited
{
    public class Util
    {
        /*
         * Workaround for the dumb assed update we had...
         */
        public static void UpdateObjectByFields<TYPE>(List<Pole> source, TYPE dest)
        {
            Type T = typeof(TYPE);
            var properties = T.GetFields();
            List<string> fieldNames = properties.Select(property => property.Name).ToList();
            foreach(Pole field in source)
            {
                if(fieldNames.Contains(field.RequestParametar) && field.GetOdgovor() != null)
                {
                    var val = field.GetOdgovor();
                    if(val != null)
                    {
                        int intRes = int.MaxValue;
                        DateTime dateVal = DateTime.Now;
                        if(DateTime.TryParse(val, out dateVal)) properties[fieldNames.IndexOf(field.RequestParametar)].SetValue(dest, dateVal);
                        else if (int.TryParse(val, out intRes)) properties[fieldNames.IndexOf(field.RequestParametar)].SetValue(dest, intRes);
                        else properties[fieldNames.IndexOf(field.RequestParametar)].SetValue(dest, val);
                    }
                }
            }
        }
    }
}
