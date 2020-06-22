using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareRevisited;

namespace Sandbox
{
    class Klasa
    {
        public string ime;
        public string prezime;
        public int broj;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var user = Login.LoginWithCredentials("7sMViyYn3B", "pfvTi1NzxE");
            Console.WriteLine(JsonConvert.SerializeObject(user));
        }
    }
}
