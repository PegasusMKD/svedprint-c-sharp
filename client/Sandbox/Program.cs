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
            Klasa k = new Klasa()
            {
                ime = "Darijan",
                prezime = "Sekerov",
                broj = 14235
            };

            Console.WriteLine(JsonConvert.SerializeObject(k));
        }
    }
}
