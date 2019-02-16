using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Middleware
{
    public class Ucenik
    {
        [JsonProperty("ime")]
        public string _ime { get; set; }
        [JsonProperty("tatkovo")]
        public string _tatkovo { get; set; }
        [JsonProperty("prezime")]
        public string _prezime { get; set; }
        [JsonProperty("oceni")]
        public List<int> _oceni { get; set; }
        [JsonProperty("paralelka")]
        public string _paralelka { get; set; }
        [JsonProperty("smer")]
        public string _smer { get; set; }

        public Ucenik()
        {
            _ime = "";
            _tatkovo = "";
            _prezime = "";
            _oceni = new List<int>();
            _paralelka = "";
            _smer = "";
        }

        public string Serialize()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }

    static public class Smerovi
    {
        public const string PMA = "PMA";
        public const string PMB = "PMB";
        public const string OHA = "OHA";
        public const string OHB = "OHB";
        public const string JUA = "JUA";
        public const string JUB = "JUB";

    }

    public class Smer
    {
        [JsonProperty("predmeti")]
        public List<string> _predmeti { get; set; }

        public Smer()
        {
            _predmeti = new List<string>();
        }

        public string Serialize()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
}
