using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Models
{
    public class Klasen
    {
        [JsonProperty(JSONRequestParameters.Klasen.Ime)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.SrednoIme)]
        public string SrednoIme { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.Prezime)]
        public string Prezime { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.Username)]
        public string Username { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.Klas)]
        public string Klas { get; set; }
    }
}
