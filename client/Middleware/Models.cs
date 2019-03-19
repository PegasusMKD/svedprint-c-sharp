using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

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

        public Ucenik(string ime, string tatkovo, string prezime, List<int> oceni, string paralelka, string smer)
        {
            _ime = ime ?? throw new ArgumentNullException(nameof(ime));
            _tatkovo = tatkovo ?? throw new ArgumentNullException(nameof(tatkovo));
            _prezime = prezime ?? throw new ArgumentNullException(nameof(prezime));
            _oceni = oceni ?? throw new ArgumentNullException(nameof(oceni));
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
        }
        public Ucenik() { }
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
        [JsonProperty("smer")]
        public string _smer { get; set; }

        public Smer(List<string> predmeti, string smer)
        {
            _predmeti = predmeti ?? throw new ArgumentNullException(nameof(predmeti));
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
        }

        public Smer() { }
    }

    public class Paralelka
    {
        [JsonProperty("paralelka")]
        public string _paralelka { get; set; }
        [JsonProperty("ucenici")]
        public List<Ucenik> _ucenici { get; set; }
        [JsonProperty("klasen")]
        public Klasen _klasen { get; set; }

        public Paralelka(string paralelka, List<Ucenik> ucenici, Klasen klasen)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _klasen = klasen ?? throw new ArgumentNullException(nameof(klasen));
        }

        public Paralelka() { }
    }

    public class Klasen
    {
        [JsonProperty("ime")]
        public string _ime { get; set; }
        [JsonProperty("prezime")]
        public string _tatkovo { get; set; }
        [JsonProperty("prezime2")]
        public string _prezime2 { get; set; }
        [JsonProperty("username")]
        public string _username { get; set; }
        [JsonProperty("password")]
        public string _password { get; set; }

        public Klasen(string ime, string tatkovo, string prezime2, string username, string password)
        {
            _ime = ime ?? throw new ArgumentNullException(nameof(ime));
            _tatkovo = tatkovo ?? throw new ArgumentNullException(nameof(tatkovo));
            _prezime2 = prezime2 ?? throw new ArgumentNullException(nameof(prezime2));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public Klasen() { }
    }

    class Request
    {
        public string _type;
        public string _scope;
        public Dictionary<string,string> _queryParams;

        public Request(string type, string scope, Dictionary<string,string> queryParams)
        {
            _type = type ?? throw new ArgumentNullException(nameof(type));
            _scope = scope ?? throw new ArgumentNullException(nameof(scope));
            _queryParams = queryParams ?? throw new ArgumentNullException(nameof(queryParams));
        }
        public Request() { }
    }

    public static class RequestTypes
    {
        public const string GET = "looking";
        public const string ADD = "type";
    }

}
