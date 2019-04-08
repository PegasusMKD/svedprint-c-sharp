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
        public Smer _s;
        public int _broj { get; set; }
        [JsonProperty(RequestParameters.dob)]
        public string _dob { get; set; }
        [JsonProperty(RequestParameters.mesto)]
        public string _mesto { get; set; }
        [JsonProperty(RequestParameters.povedenie)]
        public string _povedenie { get; set; }
        [JsonProperty(RequestParameters.opravdani)]
        public int _opravdani { get; set; }
        [JsonProperty(RequestParameters.neopravdani)]
        public int _neopravdani { get; set; }
        [JsonProperty(RequestParameters.tip)]
        public string _tip { get; set; }
        [JsonProperty(RequestParameters.pat)]
        public int _pat {get;set;}


        public Ucenik(string ime, string tatkovo, string prezime, List<int> oceni, string paralelka, string smer, int broj, string dob, string mesto, string povedenie, int opravdani, int neopravdani, string tip, int pat)
        {
            _ime = ime ?? "";
            _tatkovo = tatkovo ?? "";
            _prezime = prezime ?? "";
            _oceni = oceni ?? new List<int>();
            _paralelka = paralelka ?? "";
            _smer = smer ?? "";
            _broj = broj;
            _dob = dob ?? "01.01.1111";
            _mesto = mesto ?? "";
            _povedenie = povedenie ?? "";
            _opravdani = opravdani;
            _neopravdani = neopravdani;
            _tip = tip ?? "";
            _pat = pat;

            _s = new Smer(new List<string>(), _smer);
        }
        public Ucenik() { }
        public Ucenik(Dictionary<string, string> valuePairs)
        {
            _ime = valuePairs[RequestParameters.ime];
            _tatkovo = valuePairs[RequestParameters.srednoIme];
            _prezime = valuePairs[RequestParameters.prezime];

            string[] s = valuePairs[RequestParameters.oceni].Split(' ');
            _oceni = new List<int>();
            foreach (string x in s)
            {
                _oceni.Add(int.Parse(x));
            }

            _paralelka = valuePairs[RequestParameters.paralelka];
            _smer = valuePairs[RequestParameters.smer];
            _broj = int.Parse(valuePairs[RequestParameters.broj]);
        }

        public Dictionary<string, string> ToDict()
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
        public List<Smer> _smerovi;
        


        public Paralelka(string paralelka, List<Ucenik> ucenici, Klasen klasen, List<Smer> smerovi)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _klasen = klasen ?? throw new ArgumentNullException(nameof(klasen));
            _smerovi = smerovi ?? new List<Smer>();
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
        public Paralelka _p {get;set;}
        [JsonProperty(RequestParameters.token)]
        public string _token { get; set; }
        [JsonProperty(RequestParameters.uchilishte)]
        public string _uchilishte { get; set; }
        [JsonProperty(RequestParameters.grad)]
        public string _grad { get; set; }
        [JsonProperty(RequestParameters.godina)]
        public int _godina { get; set; } // ucebna godina

        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka, string uchilishte, string grad, int godina)
        {
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _token = token ?? "";
            _paralelka = paralelka ?? "";
            _uchilishte = uchilishte ?? "";
            _grad = grad ?? "";
            _godina = godina;

            _p = new Paralelka(_paralelka, new List<Ucenik>(), this, new List<Smer>());
        }

        public Klasen() { }
    }

    class Request
    {
        public string _type;
        public string _scope;
        public Dictionary<string, string> _queryParams;

        public Request(string type, string scope, Dictionary<string, string> queryParams)
        {
            _type = type ?? "";
            _scope = scope ?? "";
            _queryParams = queryParams ?? new Dictionary<string, string>();
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
