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
        public string _srednoIme { get; set; }
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
        [JsonProperty(RequestParameters.mesto_na_zhiveenje)]
        public string _mesto_na_zhiveenje { get; set; }
        [JsonProperty(RequestParameters.mesto_na_ragjanje)]
        public string _mesto_na_ragjanje { get; set; }
        [JsonProperty(RequestParameters.povedenie)]
        public string _povedenie { get; set; }
        [JsonProperty(RequestParameters.opravdani)]
        public int _opravdani { get; set; }
        [JsonProperty(RequestParameters.neopravdani)]
        public int _neopravdani { get; set; }
        [JsonProperty(RequestParameters.tip)]
        public string _tip { get; set; }
        [JsonProperty(RequestParameters.pat_polaga)]
        public string _pat_polaga { get; set; }
        [JsonProperty(RequestParameters.pat_polaga_ispit)]
        public string _pat_polaga_ispit { get; set; }
        [JsonProperty(RequestParameters.ispiten)]
        public string _ispiten { get; set; }
        [JsonProperty(RequestParameters.prethoden_delovoden)]
        public string _prethoden_delovoden { get; set; }
        [JsonProperty(RequestParameters.tatko)]
        public string _tatko { get; set; }
        [JsonProperty(RequestParameters.majka)]
        public string _majka { get; set; }
        [JsonProperty(RequestParameters.gender)]
        public string _gender { get; set; }
        [JsonProperty(RequestParameters.maturska)]
        public string _maturska { get; set; }
        [JsonProperty(RequestParameters.izborni)]
        public string _izborni { get; set; }
        [JsonProperty(RequestParameters.proektni)]
        public string _proektni { get; set; }
        [JsonProperty(RequestParameters.merki)]
        public string _merki { get; set; }
        [JsonProperty(RequestParameters.prethodna_godina)]
        public string _prethodna_godina { get; set; }
        [JsonProperty(RequestParameters.prethoden_uspeh)]
        public string _prethoden_uspeh { get; set; }
        [JsonProperty(RequestParameters.prethodno_uchilishte)]
        public string _prethodno_uchilishte { get; set; }
        [JsonProperty(RequestParameters.prethodna_uchebna)]
        public string _prethodna_uchebna { get; private set; }
        [JsonProperty(RequestParameters.delovoden_broj)]
        public string _delovoden_broj { get; set; }
        [JsonProperty(RequestParameters.datum_sveditelstvo)]
        public string _datum_sveditelstvo { get; set; }
        [JsonProperty(RequestParameters.polozhil)]
        public string _polozhil { get; set; }
        // [JsonProperty(RequestParameters.majkino)]
        // public string _majkino { get; set; }
        [JsonProperty(RequestParameters.pedagoshki_merki)]
        public string _pedagoshki_merki { get; set; }
        [JsonProperty(RequestParameters.drzavjanstvo)]
        public string _drzavjanstvo { get; set; }

        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string delovoden_broj, string datum_sveditelstvo, string polozhil,  string prethodna_uchebna, string pedagoshki_merki, string drzavjanstvo,string pat_polaga_ispit, string ispiten, string prethoden_delovoden)
        { //string majkino,
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _oceni = oceni ?? new List<int>();
            _smer = smer ?? "";
            //Pazzio: broj napravi go da se stava taka kako shto ti gi prakjam, odnosno, da go zima brojot vo dictionary-to, i toa da go koristi za broj vo dnevnik
            _broj = broj;
            _roden = roden ?? "01.01.1111";
            _mesto_na_zhiveenje = mesto_na_zhiveenje ?? "";
            _mesto_na_ragjanje = mesto_na_ragjanje ?? "";
            _povedenie = povedenie ?? "";
            _opravdani = opravdani;
            _neopravdani = neopravdani;
            _tip = tip ?? "";
            _pat_polaga = pat_polaga ?? "-1";
            _majka = majka ?? "";
            _tatko = tatko ?? "";
            _gender = gender ?? "";
            _maturska = maturska ?? "";
            _izborni = izborni ?? "";
            _proektni = proektni ?? "";
            _merki = merki ?? "";
            _prethodna_godina = prethodna_godina ?? "";
            _prethoden_uspeh = prethoden_uspeh ?? "";
            _prethodno_uchilishte = prethodno_uchilishte ?? "";
            _prethodna_uchebna = prethodna_uchebna ?? "";
            _delovoden_broj = delovoden_broj ?? "";
            _datum_sveditelstvo = datum_sveditelstvo ?? "";
            _polozhil = polozhil ?? "";
            // _majkino = majkino ?? "";
            
            _prethoden_delovoden = delovoden_broj ?? "";
            _pat_polaga_ispit = pat_polaga_ispit ?? "";
            _ispiten = ispiten ?? "";
            
            
            _pedagoshki_merki = pedagoshki_merki ?? "";
            _drzavjanstvo = drzavjanstvo ?? "";

            _s = new Smer(new List<string>(), _smer);
        }
        public Ucenik() { }
        public Ucenik(Dictionary<string, string> valuePairs)
        {
            _ime = valuePairs["ime"] ?? "";
            _srednoIme = valuePairs["srednoIme"] ?? " ";
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
            /*_mesto_na_ragjanje = valuePairs[RequestParameters.mesto_na_ragjanje] ?? "";
            _mesto_na_zhiveenje = valuePairs[RequestParameters.mesto_na_zhiveenje] ?? "";
            _povedenie = valuePairs[RequestParameters.povedenie] ?? "";
            _opravdani = int.Parse(valuePairs[RequestParameters.opravdani] ?? "0");
            _neopravdani = int.Parse(valuePairs[RequestParameters.neopravdani] ?? "0");
            _tip = valuePairs[RequestParameters.tip] ?? "";
            _pat_polaga = valuePairs[RequestParameters.pat_polaga] ?? "";
            _pat_polaga_ispit = valuePairs[RequestParameters.pat_polaga_ispit] ?? "";
            _ispiten = valuePairs[RequestParameters.ispiten] ?? "";
            _prethoden_delovoden = valuePairs[RequestParameters.prethoden_delovoden] ?? "";
            _tatko = valuePairs[RequestParameters.tatko] ?? "";
            _majka = valuePairs[RequestParameters.majka] ?? "";
            _gender = valuePairs[RequestParameters.gender] ?? "";
            _maturska = valuePairs[RequestParameters.maturska] ?? "";
            _izborni = valuePairs[RequestParameters.izborni] ?? "";
            _proektni = valuePairs[RequestParameters.proektni] ?? "";
            _merki = valuePairs[RequestParameters.merki] ?? "";
            _prethodna_godina = valuePairs[RequestParameters.prethodna_godina] ?? "";
            _prethoden_uspeh = valuePairs[RequestParameters.prethoden_uspeh] ?? "";
            _prethodno_uchilishte = valuePairs[RequestParameters.prethodno_uchilishte] ?? "";
            _delovoden_broj = valuePairs[RequestParameters.delovoden_broj] ?? "";
            _datum_sveditelstvo = valuePairs[RequestParameters.datum_sveditelstvo] ?? "";
            _polozhil = valuePairs[RequestParameters.polozhil] ?? "";*/
            // _majkino = valuePairs[RequestParameters.majkino] ?? "";
        }

        public Dictionary<string, string> ToDict()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                [RequestParameters.ime] = _ime,
                [RequestParameters.srednoIme] = _srednoIme,
                [RequestParameters.prezime] = _prezime,
                [RequestParameters.oceni] = string.Join(" ", _oceni),
                [RequestParameters.smer] = _smer,
                [RequestParameters.broj] = _broj.ToString()
            };

            return dictionary;
        }

        public string OceniToString()
        {
            string OcenkiString = "";
            foreach (int x in _oceni) OcenkiString += " " + x.ToString();
            OcenkiString = OcenkiString.Substring(1);
            return OcenkiString;
        }

        public string prosek()
        {
            return Array.ConvertAll(_oceni.ToArray(), x => (float)x).Average().ToString("n2");
        }

        public void UpdateUcenikOceni(String Token , int br)
        {
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.token , Token} , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } , { RequestParameters.broj , br.ToString() } ,  {RequestParameters.srednoIme , _srednoIme}  , { RequestParameters.oceni , OceniToString() }
            }, RequestScopes.UpdateUcenik);
        }
    }

    public class Smer
    {
        [JsonProperty(RequestParameters.predmeti)]
        public List<string> _predmeti { get; set; }
        [JsonProperty(RequestParameters.smer)]
        public string _smer { get; set; }
        [JsonProperty(RequestParameters.cel_smer)]
        public string _cel_smer { get; set; }

        public Smer(List<string> predmeti, string smer, string cel_smer = "")
        {
            _predmeti = predmeti ?? throw new ArgumentNullException(nameof(predmeti));
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
            _cel_smer = cel_smer ?? throw new ArgumentNullException(nameof(cel_smer));
        }

        public Smer() { }

        public void AddPredmet(string NovPredmet , String token)
        { 
            _predmeti.Add(NovPredmet);
            UpdateSmer(token);
        }

        public void RemovePredmet(int i , String token)
        {
            _predmeti.RemoveAt(i);
            UpdateSmer(token);
        }

        private void UpdateSmer(string token)
        {
            string res = "";
            foreach (string s in _predmeti)
            {
                res += s + ",";
            }
            res = res.Substring(0, res.Length - 1);
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.smer , _smer}, { RequestParameters.token , token } , { RequestParameters.predmeti, res}
            }, RequestParameters.smer);

        }
    }

    public class Paralelka
    {
        [JsonProperty(RequestParameters.paralelka)]
        public string _paralelka { get; set; }
        [JsonProperty(RequestParameters.ucenici)]
        public List<Ucenik> _ucenici { get; set; }
        public Dictionary<string,Smer> _smerovi;
        public Dictionary<string, Smer> _predmeti;


        public Paralelka(string paralelka, List<Ucenik> ucenici, Dictionary<string,Smer> smerovi)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _smerovi = smerovi ?? new Dictionary<string, Smer>();
        }

        public Paralelka() { }

        public void AddSmer(string SmerIme)
        {
            _smerovi.Add(SmerIme, new Smer(new List<string>(), SmerIme));
            UpdateSmerovi();
        }

        public void RemoveSmer(string SmerIme)
        {
            _smerovi.Remove(SmerIme);
            UpdateSmerovi();
        }

        private void UpdateSmerovi()
        {

        }
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
        [JsonProperty(RequestParameters.direktor)]
        public string _direktor { get; set; }
        [JsonProperty(RequestParameters.delovoden_broj)]
        public string _delovoden_broj { get; set; }

        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka, string uchilishte, string grad, int godina, string smerovi)
        {
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _token = token ?? "";
            _paralelka = paralelka;
            _p = new Paralelka(_paralelka, new List<Ucenik>(), new Dictionary<string, Smer>());
            _uchilishte = uchilishte ?? "";
            // _grad = grad ?? "";
            // _godina = godina;
            _smerovi = smerovi ?? "";
        }

        public Klasen() { }
        public string[] GetSmerovi()
        {
            char delimiter = ',';
            return _smerovi.Split(delimiter);
        }
        private void GetSmerPredmeti()
        {
            _p._smerovi.Clear();

            foreach (var x in GetSmerovi())
            {
                _p._smerovi.Add(x, new Smer(Requests.GetData(new Dictionary<string, string>(){
                    { RequestParameters.token, _token},
                    { RequestParameters.smer, x } ,
                    { RequestParameters.paralelka, _paralelka}
                }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), x));
            }

        }
        public void PopulateSmeroviFromUcenici(List<Ucenik> ucenici)
        {
            if (_p == null) _p = new Paralelka(_paralelka, ucenici, new Dictionary<string, Smer>());
            _p._ucenici = new List<Ucenik>(ucenici);
            _smerovi = string.Join(",", ucenici.ConvertAll(x => x._smer).Distinct());
            GetSmerPredmeti();
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
