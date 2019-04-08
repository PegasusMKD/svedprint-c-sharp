using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Middleware
{
    public class Ucenik
    { 
        [JsonProperty(RequestParameters.ime)]
        public string _ime { get; set; }
        [JsonProperty(RequestParameters.srednoIme)]
        public string _tatkovo { get; set; }
        [JsonProperty(RequestParameters.prezime)]
        public string _prezime { get; set; }
        [JsonProperty(RequestParameters.oceni)]
        public List<int> _oceni { get; set; }
        [JsonProperty(RequestParameters.paralelka)]
        public string _paralelka { get; set; }
        [JsonProperty(RequestParameters.smer)]
        public string _smer { get; set; }
        [JsonProperty(RequestParameters.broj)]
        public int _broj { get; set; }

        public Ucenik(string ime, string tatkovo, string prezime, List<int> oceni, string paralelka, string smer, int broj)
        {
            _ime = ime ?? throw new ArgumentNullException(nameof(ime));
            _tatkovo = tatkovo ?? throw new ArgumentNullException(nameof(tatkovo));
            _prezime = prezime ?? throw new ArgumentNullException(nameof(prezime));
            _oceni = oceni ?? throw new ArgumentNullException(nameof(oceni));
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
            _broj = broj;
        }
        public Ucenik() { }
        public Ucenik(Dictionary<string,string> valuePairs)
        {
            _ime = valuePairs[RequestParameters.ime];
            _tatkovo = valuePairs[RequestParameters.srednoIme];
            _prezime = valuePairs[RequestParameters.prezime];

            string[] s = valuePairs[RequestParameters.oceni].Split(' ');
            _oceni = new List<int>();
            foreach(string x in s)
            {
                _oceni.Add(int.Parse(x));
            }

            _paralelka = valuePairs[RequestParameters.paralelka];
            _smer = valuePairs[RequestParameters.smer];
            _broj = int.Parse(valuePairs[RequestParameters.broj]);
        }

        public Dictionary<string,string> ToDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary[RequestParameters.ime] = _ime;
            dictionary[RequestParameters.srednoIme] = _tatkovo;
            dictionary[RequestParameters.prezime] = _prezime;
            dictionary[RequestParameters.oceni] = string.Join(" ", _oceni);
            dictionary[RequestParameters.paralelka] = _paralelka;
            dictionary[RequestParameters.smer] = _smer;
            dictionary[RequestParameters.broj] = _broj.ToString();

            return dictionary;
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
        [JsonProperty(RequestParameters.predmeti)]
        public List<string> _predmeti { get; set; }
        [JsonProperty(RequestParameters.smer)]
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
        [JsonProperty(RequestParameters.paralelka)]
        public string _paralelka { get; set; }
        [JsonProperty(RequestParameters.ucenici)]
        public List<Ucenik> _ucenici { get; set; }
        [JsonProperty(RequestParameters.klasen)]
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
        [JsonProperty(RequestParameters.ime)]
        public string _ime { get; set; }
        [JsonProperty(RequestParameters.srednoIme)]
        public string _srednoIme { get; set; }
        [JsonProperty(RequestParameters.prezime)]
        public string _prezime { get; set; }
        [JsonProperty(RequestParameters.paralelka)]
        public string _paralelka { get; set; }
        [JsonProperty(RequestParameters.token)]
        public string _token { get; set; }

        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka)
        {
            _ime = ime ?? throw new ArgumentNullException(nameof(ime));
            _srednoIme = srednoIme ?? throw new ArgumentNullException(nameof(srednoIme));
            _prezime = prezime ?? throw new ArgumentNullException(nameof(prezime));
            _token = token ?? throw new ArgumentNullException(nameof(token));
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
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
        public const string UPDATE = "updating";
    }

}
