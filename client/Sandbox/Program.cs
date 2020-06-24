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
        static void Main(string[] args)
        {
            var task = Login.httpClientLogin("7sMViyYn3B", "pfvTi1NzxE");
            task.Wait();
            StreamWriter writer = new StreamWriter("output.txt");
            // writer.WriteLine(JsonConvert.SerializeObject(task.Result));
           
            
            writer.Close();
        }
    }
}
