using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiddlewareRevisited;
using System.IO;

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
        static async Task Main(string[] args)
        {
            var user = Login.LoginWithCredentialsAsync("7sMViyYn3B", "pfvTi1NzxE");
            StreamWriter writer = new StreamWriter("output.txt");
            var dt = DateTime.Now;
            writer.WriteLine(dt.ToLongTimeString() + dt.Millisecond);
            writer.WriteLine(JsonConvert.SerializeObject(await user));
            dt = DateTime.Now;
            writer.WriteLine(dt.ToLongTimeString() + dt.Millisecond);
            writer.Close();
        }
    }
}
