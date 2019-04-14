using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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
        [JsonProperty(RequestParameters.smer)]
        public string _smer { get; set; }
        public Smer _s { get; set; }
        [JsonProperty(RequestParameters.broj)]
        public int _broj { get; set; }
        [JsonProperty(RequestParameters.roden)]
        public string _roden { get; set; }
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
        public string _pat { get; set; }


        public Ucenik(string ime, string tatkovo, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto, string povedenie, int opravdani, int neopravdani, string tip, string pat)
        {
            _ime = ime ?? "";
            _tatkovo = tatkovo ?? "";
            _prezime = prezime ?? "";
            _oceni = oceni ?? new List<int>();
            _smer = smer ?? "";
            _broj = broj;
            _roden = roden ?? "01.01.1111";
            _mesto = mesto ?? "";
            _povedenie = povedenie ?? "";
            _opravdani = opravdani;
            _neopravdani = neopravdani;
            _tip = tip ?? "";
            _pat = pat ?? "-1";

            _s = new Smer(new List<string>(), _smer);
        }
        public Ucenik() { }
        public Ucenik(Dictionary<string, string> valuePairs)
        {
            _ime = valuePairs["ime"] ?? "";
            _tatkovo = valuePairs["tatkovo"] ?? "";
            _prezime = valuePairs["prezime"] ?? "";

            string[] s = valuePairs[RequestParameters.oceni].Split(' ');
            _oceni = new List<int>();
            foreach (string x in s)
            {
                _oceni.Add(int.Parse(x));
            }

            _smer = valuePairs[RequestParameters.smer] ?? "";
            _s = new Smer(new List<string>(), _smer);
            _broj = int.Parse(valuePairs[RequestParameters.broj] ?? "0");
            _roden = valuePairs[RequestParameters.roden] ?? "";
            _mesto = valuePairs[RequestParameters.mesto] ?? "";
            _povedenie = valuePairs[RequestParameters.povedenie] ?? "";
            _opravdani = int.Parse(valuePairs[RequestParameters.opravdani] ?? "0");
            _neopravdani = int.Parse(valuePairs[RequestParameters.neopravdani] ?? "0");
            _tip = valuePairs[RequestParameters.tip] ?? "";
            _pat = valuePairs[RequestParameters.pat] ?? "";
        }

        public Dictionary<string, string> ToDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                [RequestParameters.ime] = _ime,
                [RequestParameters.srednoIme] = _tatkovo,
                [RequestParameters.prezime] = _prezime,
                [RequestParameters.oceni] = string.Join(" ", _oceni),
                [RequestParameters.smer] = _smer,
                [RequestParameters.broj] = _broj.ToString()
            };

            return dictionary;
        }
    }

    public class Smerovi
    {
        public const string PMA = "ПМА";
        public const string PMB = "PMB";
        public const string OHA = "OHA";
        public const string OHB = "OHB";
        public const string JUA = "JUA";
        public const string JUB = "JUB";
        // TODO: smerovite da bidat na kirilica
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
        public Dictionary<string,Smer> _smerovi;



        public Paralelka(string paralelka, List<Ucenik> ucenici, Dictionary<string,Smer> smerovi)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _smerovi = smerovi ?? new Dictionary<string, Smer>();
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
        public Paralelka _p { get; set; }
        [JsonProperty(RequestParameters.token)]
        public string _token { get; set; }
        [JsonProperty(RequestParameters.uchilishte)]
        public string _uchilishte { get; set; }
        [JsonProperty(RequestParameters.grad)]
        public string _grad { get; set; }
        [JsonProperty(RequestParameters.godina)]
        public int _godina { get; set; } // ucebna godina
        [JsonProperty("smerovi")]
        public string _smerovi { get; set; }

        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka, string uchilishte, string grad, int godina, string smerovi)
        {
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _token = token ?? "";
            _paralelka = paralelka;
            _p = new Paralelka(_paralelka, new List<Ucenik>(), new Dictionary<string, Smer>());
            _uchilishte = uchilishte ?? "";
            _grad = grad ?? "";
            _godina = godina;
            _smerovi = smerovi ?? "";

        }

        public Klasen() { }
        public string[] GetSmerovi()
        {
            char delimiter = ',';
            return _smerovi.Split(delimiter);
        }
        public void PopulateSmeroviFromUcenici(List<Ucenik> ucenici)
        {
            if (_p == null) _p = new Paralelka(_paralelka, ucenici, new Dictionary<string, Smer>());
            _p._ucenici = new List<Ucenik>(ucenici);
            HashSet<string> smerovi = new HashSet<string>(ucenici.ConvertAll(x => x._smer));
            _smerovi = string.Join(",", smerovi);
        }
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
