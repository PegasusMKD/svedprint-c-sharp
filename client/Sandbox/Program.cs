using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {

            string json = "{" +
                " \"companies\": {" +
                    "\"abc\": \"Abc Company\", " +
                    "\"def\": \"Def Company\", " +
                    "\"ghi\": { " +
                        "\"abc\": 4, " +
                        "\"ee\": \"fof\"" +
                    " }" +
                "}," +
                "\"name\" : \"darijan\"" +
            "}";
            JObject root = JObject.Parse(json);
            Console.WriteLine(root.Value<JObject>("companies").Property("abc")); 
        }

    }
}
