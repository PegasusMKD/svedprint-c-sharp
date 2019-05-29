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
        [JsonProperty(RequestParameters.drzavjanstvo)]
        public string _drzavjanstvo { get; set; }
        [JsonProperty(RequestParameters.cel_smer)]
        public string _cel_smer { get; set; }

        /* vaka nekako treba da lici release verzija na constructor
         * treba da frla exception ako fali nekoj podatok vo baza
        
        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, Smer s, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string pat_polaga_ispit, string ispiten, string prethoden_delovoden, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string prethodna_uchebna, string delovoden_broj, string datum_sveditelstvo, string polozhil, string pedagoshki_merki, string drzavjanstvo)
        {
            _ime = ime ?? throw new ArgumentNullException(nameof(ime));
            _srednoIme = srednoIme ?? throw new ArgumentNullException(nameof(srednoIme));
            _prezime = prezime ?? throw new ArgumentNullException(nameof(prezime));
            _oceni = oceni ?? throw new ArgumentNullException(nameof(oceni));
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
            _s = s ?? throw new ArgumentNullException(nameof(s));
            _broj = broj;
            _roden = roden ?? throw new ArgumentNullException(nameof(roden));
            _mesto_na_zhiveenje = mesto_na_zhiveenje ?? throw new ArgumentNullException(nameof(mesto_na_zhiveenje));
            _mesto_na_ragjanje = mesto_na_ragjanje ?? throw new ArgumentNullException(nameof(mesto_na_ragjanje));
            _povedenie = povedenie ?? throw new ArgumentNullException(nameof(povedenie));
            _opravdani = opravdani;
            _neopravdani = neopravdani;
            _tip = tip ?? throw new ArgumentNullException(nameof(tip));
            _pat_polaga = pat_polaga ?? throw new ArgumentNullException(nameof(pat_polaga));
            _pat_polaga_ispit = pat_polaga_ispit ?? throw new ArgumentNullException(nameof(pat_polaga_ispit));
            _ispiten = ispiten ?? throw new ArgumentNullException(nameof(ispiten));
            _prethoden_delovoden = prethoden_delovoden ?? throw new ArgumentNullException(nameof(prethoden_delovoden));
            _tatko = tatko ?? throw new ArgumentNullException(nameof(tatko));
            _majka = majka ?? throw new ArgumentNullException(nameof(majka));
            _gender = gender ?? throw new ArgumentNullException(nameof(gender));
            _maturska = maturska ?? throw new ArgumentNullException(nameof(maturska));
            _izborni = izborni ?? throw new ArgumentNullException(nameof(izborni));
            _proektni = proektni ?? throw new ArgumentNullException(nameof(proektni));
            _merki = merki ?? throw new ArgumentNullException(nameof(merki));
            _prethodna_godina = prethodna_godina ?? throw new ArgumentNullException(nameof(prethodna_godina));
            _prethoden_uspeh = prethoden_uspeh ?? throw new ArgumentNullException(nameof(prethoden_uspeh));
            _prethodno_uchilishte = prethodno_uchilishte ?? throw new ArgumentNullException(nameof(prethodno_uchilishte));
            _prethodna_uchebna = prethodna_uchebna ?? throw new ArgumentNullException(nameof(prethodna_uchebna));
            _delovoden_broj = delovoden_broj ?? throw new ArgumentNullException(nameof(delovoden_broj));
            _datum_sveditelstvo = datum_sveditelstvo ?? throw new ArgumentNullException(nameof(datum_sveditelstvo));
            _polozhil = polozhil ?? throw new ArgumentNullException(nameof(polozhil));
            _pedagoshki_merki = pedagoshki_merki ?? throw new ArgumentNullException(nameof(pedagoshki_merki));
            _drzavjanstvo = drzavjanstvo ?? throw new ArgumentNullException(nameof(drzavjanstvo));
        } */

        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string delovoden_broj, string datum_sveditelstvo, string polozhil, string prethodna_uchebna, string pedagoshki_merki, string drzavjanstvo, string pat_polaga_ispit, string ispiten, string prethoden_delovoden)
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

            _drzavjanstvo = drzavjanstvo ?? "";
        }
        public Ucenik() { }

        public Ucenik(string ime, string srednoime, string prezime, Smer smer, string br)
        {
            _ime = ime;
            _prezime = prezime;
            _srednoIme = srednoime;
            _smer = smer._smer;
            _broj = int.Parse(br);
            _tatko = " ";
            _majka = " ";
            _roden = " ";
            _mesto_na_ragjanje = " ";
            _oceni = new List<int>();
            foreach (string predmet in smer._predmeti)
            {
                _oceni.Add(0);
            }
        }

        public void CreateServerUcenik(string token)
        {
            Requests.AddData(new Dictionary<string, string>() {
            { RequestParameters.token , token} , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } , { RequestParameters.broj , _broj.ToString() } ,  {RequestParameters.srednoIme , _srednoIme}  , { RequestParameters.smer, _smer }
            }, RequestScopes.AddUcenici);
        }

        public void DeleteUcenik(string token)
        {
            Requests.UpdateData(new Dictionary<string, string>()
            {
                { RequestParameters.token , token} , { RequestParameters.action , RequestParameters.delete } , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } ,  {RequestParameters.srednoIme , _srednoIme}
            }, RequestScopes.UpdateUcenik);
        }

        public Ucenik(Dictionary<string, string> valuePairs)
        {
            _ime = valuePairs[RequestParameters.ime] ?? "";
            _srednoIme = valuePairs[RequestParameters.srednoIme] ?? ""; // "srednoIme"
            _prezime = valuePairs[RequestParameters.prezime] ?? "";

            string[] s = valuePairs[RequestParameters.oceni].Split(' ');
            _oceni = new List<int>();
            foreach (string x in s)
            {
                _oceni.Add(int.Parse(x));
            }

            _smer = valuePairs[RequestParameters.smer] ?? "";
            _broj = int.Parse(valuePairs[RequestParameters.broj] ?? "0");
            _roden = valuePairs[RequestParameters.roden] ?? "";
            _mesto_na_ragjanje = valuePairs[RequestParameters.mesto_na_ragjanje] ?? "";
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
            _polozhil = valuePairs[RequestParameters.polozhil] ?? "";
            // _majkino = valuePairs[RequestParameters.majkino] ?? "";
        }

        public void UpdateUcenikData(Dictionary<string, string> UpdatedData, Dictionary<string, string> OrigData, string token)
        {
            /*
             Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.token , Token} , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } , { RequestParameters.broj , br.ToString() } ,  {RequestParameters.srednoIme , _srednoIme}  , { UpdateParametar, value }
            }, RequestScopes.UpdateUcenik);
             */
            Dictionary<string, string> toBeChanged = new Dictionary<string, string>(UpdatedData);
            toBeChanged[RequestParameters.token] = token;

            if (OrigData.ContainsKey(RequestParameters.ime))
            {
                toBeChanged[RequestParameters.new_first_name] = toBeChanged[RequestParameters.ime];
                toBeChanged[RequestParameters.ime] = OrigData[RequestParameters.ime];
            }

            if (OrigData.ContainsKey(RequestParameters.prezime))
            {
                toBeChanged[RequestParameters.new_last_name] = toBeChanged[RequestParameters.prezime];
                toBeChanged[RequestParameters.prezime] = OrigData[RequestParameters.prezime];
            }

            if (OrigData.ContainsKey(RequestParameters.srednoIme))
            {
                toBeChanged[RequestParameters.new_middle_name] = toBeChanged[RequestParameters.srednoIme];
                toBeChanged[RequestParameters.srednoIme] = OrigData[RequestParameters.srednoIme];
            }

            if (OrigData.ContainsKey(RequestParameters.broj))
            {
                toBeChanged[RequestParameters.new_broj_vo_dnevnik] = toBeChanged[RequestParameters.broj];
                toBeChanged[RequestParameters.broj] = OrigData[RequestParameters.broj];
            }

            Requests.UpdateData(toBeChanged, RequestScopes.UpdateUcenik);

            _ime = UpdatedData[RequestParameters.ime];
            _srednoIme = UpdatedData[RequestParameters.srednoIme];
            _prezime = UpdatedData[RequestParameters.prezime];
            _smer = UpdatedData[RequestParameters.smer];
            _broj = int.Parse(UpdatedData[RequestParameters.broj]);
            _tatko = UpdatedData[RequestParameters.tatko];
            _majka = UpdatedData[RequestParameters.majka];
            _roden = UpdatedData[RequestParameters.roden];
            _mesto_na_ragjanje = UpdatedData[RequestParameters.mesto_na_ragjanje];
            _drzavjanstvo = UpdatedData[RequestParameters.drzavjanstvo];
        }
        string[] Request = {
            RequestParameters.new_first_name,
            RequestParameters.new_middle_name,
            RequestParameters.new_last_name,
            RequestParameters.new_smer,
            RequestParameters.new_broj_vo_dnevnik,
            RequestParameters.tatko,
            RequestParameters.majka,
            RequestParameters.roden,
            RequestParameters.mesto_na_ragjanje,
            RequestParameters.drzavjanstvo
            //RequestParameters
        };

        public string OceniToString()
        {
            string OcenkiString = "";
            foreach (int x in _oceni) OcenkiString += " " + x.ToString();
            OcenkiString = OcenkiString.Substring(1);
            return OcenkiString;
        }

        public string prosek()
        {
            if (_oceni.Count == 0) return "0.00";
            return Array.ConvertAll(_oceni.ToArray(), x => (float)x).Average().ToString("n2");
        }

        public void UpdateUcenikOceni(int br, string Token)
        {
            UpdateUcenik(br, RequestParameters.oceni, OceniToString(), Token);
        }

        public void UpdateUcenikSmer(int br, string Token)
        {
            UpdateUcenik(br, RequestParameters.smer, _smer, Token);
        }

        public void UpdateUcenik(int br, string UpdateParametar, string value, string Token)
        {
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.token , Token} , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } , { RequestParameters.broj , br.ToString() } ,  {RequestParameters.srednoIme , _srednoIme}  , { UpdateParametar, value }
            }, RequestScopes.UpdateUcenik);
        }

        public void ChangeSmer(Smer NovSmer, int br, string token)
        {
            _smer = NovSmer._smer;
            _oceni.Clear();
            foreach (string predmet in NovSmer._predmeti)
            {
                _oceni.Add(0);
            }

            UpdateUcenik(br, RequestParameters.new_smer, NovSmer._smer, token);
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

        public Smer(string smer, string cel_smer = "")
        {
            _predmeti = new List<string>();
            _smer = smer ?? throw new ArgumentNullException(nameof(smer));
            _cel_smer = cel_smer ?? throw new ArgumentNullException(nameof(cel_smer));
        }

        public Smer() { }

        public void AddPredmet(string NovPredmet, String token)
        {
            _predmeti.Add(NovPredmet);
            UpdateSmer(token);
        }

        public void UpdatePredmet(int ctr, string UpdatePredmet, string token)
        {
            _predmeti[ctr] = UpdatePredmet;
            UpdateSmer(token);
        }

        public void RemovePredmet(int i, String token)
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
            if (res.Length > 0) res = res.Substring(0, res.Length - 1);
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.smer , _smer}, { RequestParameters.token , token } , { RequestParameters.predmeti, res}
            }, RequestParameters.smer);

        }

        public void RemoveSmer(string token)
        {
            Requests.UpdateData(new Dictionary<string, string>()
            {
                { RequestParameters.token , token} , { RequestParameters.action , RequestParameters.delete } , { RequestParameters.smer , _smer }
            }, RequestScopes.UpdateSmer);
        }
    }

    public class Paralelka
    {
        [JsonProperty(RequestParameters.paralelka)]
        public string _paralelka { get; set; }
        [JsonProperty(RequestParameters.ucenici)]
        public List<Ucenik> _ucenici { get; set; }
        public Dictionary<string, Smer> _smerovi;
        public Dictionary<string, Smer> _predmeti;


        public Paralelka(string paralelka, List<Ucenik> ucenici, Dictionary<string, Smer> smerovi)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _smerovi = smerovi ?? new Dictionary<string, Smer>();
        }

        public Paralelka() { }

        public void AddSmer(Smer NovSmer, string token)
        {
            _smerovi.Add(NovSmer._smer, new Smer(NovSmer._smer, NovSmer._cel_smer));
            AddtoServerSmer(NovSmer, token);
        }

        public void RemoveSmer(string SmerIme)
        {
            _smerovi.Remove(SmerIme);
            //UpdateSmerovi();
        }

        private void AddtoServerSmer(Smer NovSmer, string token)
        {
            Requests.AddData(new Dictionary<string, string>() {
            { RequestParameters.token , token} , { RequestParameters.smer_add , NovSmer._smer } , {RequestParameters.cel_smer , NovSmer._cel_smer } , { RequestParameters.predmeti , string.Join(",",NovSmer._predmeti)}
            }, RequestScopes.UpdateSmer);
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
        [JsonProperty(RequestParameters.ucilishte)]
        public string _ucilishte { get; set; }
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
        [JsonProperty(RequestParameters.akt_godina)]
        public string _akt_godina { get; set; }
        [JsonProperty(RequestParameters.akt)]
        public string _akt { get; set; }


        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka, string ucilishte, string grad, int godina, string smerovi, string akt_godina, string akt)
        {
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _token = token ?? "";
            _paralelka = paralelka;
            _p = new Paralelka(_paralelka, new List<Ucenik>(), new Dictionary<string, Smer>());
            _ucilishte = ucilishte ?? "";
            // _grad = grad ?? "";
            // _godina = godina;
            _smerovi = smerovi ?? "";
            _akt_godina = akt_godina ?? "";
            _akt = _akt ?? "";
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
            if (ucenici.Count == 0) return;
            _smerovi = string.Join(",", ucenici.ConvertAll(x => x._smer).Distinct());
            GetSmerPredmeti();
        }

        public void PopulateSmerovi(List<Ucenik> ucenici)
        {
            if (_p == null) _p = new Paralelka(_paralelka, ucenici, new Dictionary<string, Smer>());
            _p._ucenici = ucenici;
            GetSmerPredmeti();
        }

        public void GetSmerPredmeti()
        {
            _p._smerovi.Clear();
            if (GetSmerovi().Length == 0) return;
            foreach (var x in GetSmerovi())
            {
                _p._smerovi.Add(x, new Smer(Requests.GetData(new Dictionary<string, string>(){
                    { RequestParameters.token, _token},
                    { RequestParameters.smer, x } ,
                    { RequestParameters.paralelka, _paralelka}
                }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), x));
            }

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
