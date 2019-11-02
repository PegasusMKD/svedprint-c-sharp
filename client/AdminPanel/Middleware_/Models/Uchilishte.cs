using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Models
{
    public class Uchilishte
    {
        [JsonProperty(JSONRequestParameters.Admin.Uchilishte)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DelovodenBroj)]
        public string DelovodenBroj { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.OdobrenoSveditelstvo)]
        public string OdobrenoSveditelstvo { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Ministerstvo)]
        public string Ministerstvo { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.GlavnaKniga)]
        public string GlavnaKniga { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Akt)]
        public string Akt { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.AktGodina)]
        public string AktGodina { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Direktor)]
        public string Direktor { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.MozniDatiMatura)]
        public List<string> MozniDatiMatura { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.MozniDatiSveditelstva)]
        public List<string> MozniDatiSveditelstva { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DataMatura)]
        public string DataMatura { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DataSveditelstva)]
        public string DataSveditelstva { get; set; }
    }
}
