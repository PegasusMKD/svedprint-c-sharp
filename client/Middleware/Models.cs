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
        public string _pedagoski_merki { get; set; }
        [JsonProperty(RequestParameters.prethodna_godina)]
        public string _prethodna_godina { get; set; }
        [JsonProperty(RequestParameters.prethoden_uspeh)]
        public string _prethoden_uspeh { get; set; }
        [JsonProperty(RequestParameters.prethodno_uchilishte)]
        public string _prethodno_uchilishte { get; set; }
        [JsonProperty(RequestParameters.prethodna_uchebna)]
        public string _prethodna_uchebna { get; set; }
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
        [JsonProperty(RequestParameters.duplicate_ctr)]
        public int _duplicate_ctr { get; set; }
        //Dodatoci od Pazzio
        [JsonProperty(RequestParameters.jazik)]
        public string _jazik { get; set; }
        [JsonProperty(RequestParameters.jazik_ocena)]
        public string _jazik_ocena { get; set; }

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

        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string delovoden_broj, string datum_sveditelstvo, string polozhil, string prethodna_uchebna, string pedagoshki_merki, string drzavjanstvo, string pat_polaga_ispit, string ispiten, string prethoden_delovoden, int duplicate_ctr,
            string jazik, string jazik_ocena)
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
            //Predmet, ocena, datum na polaganje, mozhebi 
            _maturska = maturska ?? "";
            //Treba da e lista, kako e vo sushtina e Predmet,dali polozhil;predmet,dali go polozhil...
            _izborni = izborni ?? "";
            _proektni = proektni ?? "";
            _pedagoski_merki = merki ?? "";
            _prethodna_godina = prethodna_godina ?? "";
            _prethoden_uspeh = prethoden_uspeh ?? "";
            _prethodno_uchilishte = prethodno_uchilishte ?? "";
            _prethodna_uchebna = prethodna_uchebna ?? "";
            //Ova se zema od kaj Klasniot
            //_delovoden_broj = delovoden_broj ?? "";
            //_datum_sveditelstvo = datum_sveditelstvo ?? "";

            _polozhil = polozhil ?? "";
            // _majkino = majkino ?? "";

            _prethoden_delovoden = delovoden_broj ?? "";
            _pat_polaga_ispit = pat_polaga_ispit ?? "";
            _ispiten = ispiten ?? "";

            _drzavjanstvo = drzavjanstvo ?? "";

            _duplicate_ctr = duplicate_ctr;

            //Dodatoci od Pazzio
            _jazik = jazik ?? "";
            _jazik_ocena = jazik_ocena ?? "";
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

        //Od pazzio, mozhno e da zatreba i se koristi vo Print.cs


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
                { RequestParameters.token , token} , { RequestParameters.action , RequestParameters.delete } , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } ,  {RequestParameters.srednoIme , _srednoIme}  , {RequestParameters.duplicate_ctr, _duplicate_ctr.ToString()}
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
                int a = int.TryParse(x, out int DefaultOcena) ? DefaultOcena : 0;
                _oceni.Add(a);
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
            _prethodna_godina = valuePairs[RequestParameters.prethodna_godina] ?? "";
            _prethoden_uspeh = valuePairs[RequestParameters.prethoden_uspeh] ?? "";
            _prethodno_uchilishte = valuePairs[RequestParameters.prethodno_uchilishte] ?? "";
            _delovoden_broj = valuePairs[RequestParameters.delovoden_broj] ?? "";
            _datum_sveditelstvo = valuePairs[RequestParameters.datum_sveditelstvo] ?? "";
            _polozhil = valuePairs[RequestParameters.polozhil] ?? "";
            _duplicate_ctr = int.Parse(valuePairs[RequestParameters.duplicate_ctr] ?? "-1");
            _jazik = valuePairs[RequestParameters.jazik] ?? "";
            _pedagoski_merki = valuePairs[RequestParameters.pedagoshki_merki] ?? "";
            string outvar;
            bool success = valuePairs.TryGetValue(RequestParameters.prethodna_uchebna, out outvar);
            _prethodna_uchebna = success ? outvar : "";
            _drzavjanstvo = valuePairs[RequestParameters.drzavjanstvo] ?? "";
            // _majkino = valuePairs[RequestParameters.majkino] ?? "";
        }

        public string UpdateUcenikData(Dictionary<string, string> UpdatedData, Dictionary<string, string> OrigData, string token)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>(UpdatedData);
            queryParams[nameof(token)] = token;
            queryParams.Add(RequestParameters.duplicate_ctr, _duplicate_ctr.ToString());
            if (OrigData.ContainsKey("ime"))
            {
                queryParams["new_first_name"] = queryParams["ime"];
                queryParams["ime"] = OrigData["ime"];
            }
            if (OrigData.ContainsKey("prezime"))
            {
                queryParams["new_last_name"] = queryParams["prezime"];
                queryParams["prezime"] = OrigData["prezime"];
            }
            if (OrigData.ContainsKey("srednoIme"))
            {
                queryParams["new_middle_name"] = queryParams["srednoIme"];
                queryParams["srednoIme"] = OrigData["srednoIme"];
            }
            if (OrigData.ContainsKey("broj"))
            {
                queryParams["new_broj_vo_dnevnik"] = queryParams["broj"];
                queryParams["broj"] = OrigData["broj"];
            }
            string rez = Requests.UpdateData(queryParams, "ucenik");
            this._ime = UpdatedData["ime"];
            this._prezime = UpdatedData["prezime"];
            this._srednoIme = UpdatedData["srednoIme"];
            this._smer = UpdatedData["smer"];
            this._broj = int.Parse(UpdatedData["broj"]);
            this._tatko = UpdatedData["tatko"];
            this._majka = UpdatedData["majka"];
            this._roden = UpdatedData["roden"];
            this._mesto_na_ragjanje = UpdatedData["mesto_na_ragjanje"];
            this._gender = UpdatedData["gender"];
            this._roden = UpdatedData["roden"];
            this._mesto_na_ragjanje = UpdatedData["mesto_na_ragjanje"];
            this._mesto_na_zhiveenje = UpdatedData["mesto_na_zhiveenje"];
            this._pat_polaga = UpdatedData["pat"];
            this._polozhil = UpdatedData["polozhil"];
            if (UpdatedData.Keys.Contains(RequestParameters.pedagoshki_merki)) _pedagoski_merki = UpdatedData[RequestParameters.pedagoshki_merki];
            if (UpdatedData.Keys.Contains(RequestParameters.povedenie)) _povedenie = UpdatedData[RequestParameters.povedenie];
            /*
            int result = 0;
            int.TryParse(UpdatedData["opravdani"], out result);
            this._opravdani = result;
            int.TryParse(UpdatedData["neopravdani"], out result);
            this._neopravdani = result;
            this._povedenie = UpdatedData["povedenie"];*/
            //_pedagoski_merki = UpdatedData[RequestParameters.pedagoshki_merki];
            _prethodna_godina = UpdatedData[RequestParameters.prethodna_godina];
            _prethoden_uspeh = UpdatedData[RequestParameters.prethoden_uspeh];
            _prethodno_uchilishte = UpdatedData[RequestParameters.prethodno_uchilishte];
            return rez;
        }

        public Dictionary<string,string> GetPolinja ()
        {
            Dictionary<string, string> polinja = new Dictionary<string, string>();
            polinja.Add(RequestParameters.ime , _ime);
            polinja.Add(RequestParameters.prezime , _prezime);
            polinja.Add(RequestParameters.srednoIme, _srednoIme);
            polinja.Add(RequestParameters.smer, _smer );
            polinja.Add(RequestParameters.tatko, _tatko);
            polinja.Add(RequestParameters.majka, _majka );
            polinja.Add(RequestParameters.broj, _broj.ToString() );
            polinja.Add(RequestParameters.gender,_gender);
            polinja.Add(RequestParameters.roden , _roden );
            polinja.Add(RequestParameters.mesto_na_ragjanje , _mesto_na_ragjanje);
            polinja.Add(RequestParameters.mesto_na_zhiveenje , _mesto_na_zhiveenje);
            polinja.Add(RequestParameters.pat_polaga , _pat_polaga);
            polinja.Add(RequestParameters.polozhil  , _polozhil);
            polinja.Add(RequestParameters.povedenie, _povedenie);
            polinja.Add(RequestParameters.opravdani , _opravdani.ToString() );
            polinja.Add(RequestParameters.neopravdani , _neopravdani.ToString());
            polinja.Add(RequestParameters.proektni , _proektni);
            polinja.Add(RequestParameters.pedagoshki_merki, _pedagoski_merki);
            polinja.Add(RequestParameters.prethodna_godina, _prethodna_godina);
            polinja.Add(RequestParameters.prethodno_uchilishte, _prethodno_uchilishte);
            polinja.Add(RequestParameters.prethoden_uspeh, _prethoden_uspeh);
            return polinja;
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
            if (_oceni.Count == 0) return "0.00";
            return Array.ConvertAll(_oceni.ToArray(), x => (float)x).Average().ToString("n2");
        }
        public bool CheckPass()
        {
            bool checker = true;
            foreach (int i in _oceni)
            {
                if (i == 0 || i == 1)
                {
                    checker = false;
                    break;
                }
            }
            return checker;
        }

        public string ProektniToString(List<string> tx)
        {
            int i = 0;
            string s = "";
            foreach (string x in tx)
            {
                if (i % 2 == 0)
                {
                    s += x + ",";
                }
                else s += x + ";";
                i++;
            }
            s = s.Substring(0, s.Length - 1);
            return s;
        }

        public void UpdateProektni(int i , string cb_pole, bool Isrealised, string token)
        {

            string realizirano;
            if (Isrealised) realizirano = ",Реализирал";
            else realizirano = ",Не реализирал";

            List<string> tx = _proektni.Split(';').ToList();
            tx[i] = cb_pole + realizirano;
            string rez = string.Join(";", tx);

            UpdateUcenik(RequestParameters.proektni, rez, token);
            _proektni = rez;
        }

        public void UpdateUcenikOceni(string Token)
        {
            //UpdateUcenik(br, RequestParameters.oceni, OceniToString(), Token);
            UpdateUcenik(RequestParameters.oceni, string.Join(" ", _oceni), Token);
        }

        public string UpdateUcenik(string UpdateParametar, string value, string Token)
        {
            return Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.token , Token} , { RequestParameters.ime , _ime } , {RequestParameters.prezime , _prezime } ,  {RequestParameters.srednoIme , _srednoIme}  , { UpdateParametar, value }, {RequestParameters.duplicate_ctr, _duplicate_ctr.ToString()}
            }, RequestScopes.UpdateUcenik);
        }

        public void ChangeSmer(Smer NovSmer, string token)
        {
            _smer = NovSmer._smer;
            _oceni.Clear();
            foreach (string predmet in NovSmer._predmeti)
            {
                _oceni.Add(0);
            }

            UpdateUcenik(RequestParameters.new_smer, NovSmer._smer, token);
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
        int[] jaziciPos = new int[] { -1 , -1 };

        public Smer(List<string> predmeti, string smer, string cel_smer)
        {
            _predmeti = predmeti ?? new List<string>();
            _smer = smer ?? "";
            _cel_smer = cel_smer ?? "";
        }

        public Smer(string smer, string cel_smer)
        {
            _predmeti = new List<string>();
            _smer = smer ?? "";
            _cel_smer = cel_smer ?? "";
        }

        public Smer() { }

        public List<string> GetCeliPredmeti(string jazici, string izbp , Dictionary<string , Smer > Smerovi)
        {
            int i = 0, j = 0 , izbctr = 0;
            List<string> sj = new List<string>();
            if (Smerovi.Keys.Contains("Странски Јазици"))
            {
                if (jazici != null && jazici != "" && jazici.Length == 3)
                {
                    i = int.Parse(jazici.Split(';')[0]);
                    j = int.Parse(jazici.Split(';')[1]);
                }
                sj = Smerovi["Странски Јазици"]._predmeti;
            }
            else sj.Add("");

            string izboren = "";
            if(Smerovi["Изборни Предмети"]._predmeti.Count > 0)
            {
                if (izbp != null && izbp != "") izbctr = int.Parse(izbp);
                else izbctr = -1;

                //izbctr = 1;
                if (izbctr != -1) izboren = Smerovi["Изборни Предмети"]._predmeti[izbctr];
            }

            List<string> predmeti = new List<string>();

            int ctr = 0;
            if (jaziciPos[0] == -1)
                foreach (string predmet in _predmeti)
                {
                    string s = predmet;
                    if (predmet == "1 СЈ") { s = sj[i]; jaziciPos[0] = ctr; }
                    if (predmet == "2 СЈ") { s = sj[j]; jaziciPos[1] = ctr; }
                    if (predmet == "Изборен Предмет 1") { s = izboren; }
                    predmeti.Add(s);
                    ctr++;
                }
            else
            {
                predmeti = _predmeti;
                predmeti[jaziciPos[0]] = sj[i]; 
                predmeti[jaziciPos[1]] = sj[j];
            }
            return predmeti;
        }

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
            var res = string.Join(",", _predmeti);
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.smer , _smer}, { RequestParameters.token , token } , { RequestParameters.predmeti, res}
            }, RequestScopes.UpdateSmer);

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


        public Paralelka(string paralelka, List<Ucenik> ucenici, Dictionary<string, Smer> smerovi)
        {
            _paralelka = paralelka ?? throw new ArgumentNullException(nameof(paralelka));
            _ucenici = ucenici ?? throw new ArgumentNullException(nameof(ucenici));
            _smerovi = smerovi ?? new Dictionary<string, Smer>();
        }

        public Paralelka() { }

        public void AddSmer(Smer NovSmer, string token)
        {
            //_smerovi.Add(NovSmer._smer, new Smer(NovSmer._smer, NovSmer._cel_smer));
            AddtoServerSmer(NovSmer, token);

            //_smerovi.Add(NovSmer._smer, new Smer(Requests.GetData(new Dictionary<string, string>(){
            //    { RequestParameters.token, token},
            //    { RequestParameters.smer, NovSmer._smer }
            //}, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), NovSmer._smer, NovSmer._cel_smer));

            var predmeti = Requests.GetData(new Dictionary<string, string>(){
                { RequestParameters.token, token},
                { RequestParameters.smer, NovSmer._smer }
            }, RequestScopes.GetPredmetiSmer);
            List<string> Lpredmeti = new List<string>();
            if (predmeti.Count > 0 && !string.IsNullOrEmpty(predmeti[0]["predmeti"]))
            {
                Lpredmeti = predmeti[0]["predmeti"].Split(',').ToList();
            }
            _smerovi.Add(NovSmer._smer, new Smer(Lpredmeti, NovSmer._smer, NovSmer._cel_smer));
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
        [JsonProperty(RequestParameters.drzava)]
        public string _drzava { get; set; }
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
        //Dodavano od Pazzio        
        [JsonProperty(RequestParameters.odobreno_sveditelstvo)]
        public string _odobreno_sveditelstvo { get; set; }
        [JsonProperty(RequestParameters.ministerstvo)]
        public string _ministerstvo { get; set; }
        [JsonProperty(RequestParameters.glavna_kniga)]
        public string _glavna_kniga { get; set; }

        public Klasen(string ime, string srednoIme, string prezime, string token, string paralelka, string ucilishte, string grad, int godina, string smerovi, string akt_godina, string akt, string odobreno_sveditelstvo,
            string ministerstvo, string glavna_kniga, string drzava)
        {
            _ime = ime ?? "";
            _srednoIme = srednoIme ?? "";
            _prezime = prezime ?? "";
            _token = token ?? "";
            _paralelka = paralelka ?? "";
            _p = new Paralelka(_paralelka, new List<Ucenik>(), new Dictionary<string, Smer>());
            _ucilishte = ucilishte ?? "";
            _grad = grad ?? "";
            _godina = godina;
            _smerovi = smerovi ?? "";
            _akt_godina = akt_godina ?? "";
            _akt = akt ?? "";
            _odobreno_sveditelstvo = odobreno_sveditelstvo ?? "";
            _ministerstvo = ministerstvo ?? "";
            _glavna_kniga = glavna_kniga ?? "";
            _drzava = drzava ?? "";
        }

        public Klasen() { }
        public string[] GetSmerovi()
        {
            char delimiter = ',';
            return _smerovi.Split(delimiter);
        }

        public void SetSmeroviPredmeti(string token)
        {
            List<string> Smerovi = new List<string>(); 
            if (_p == null)
            {
                _p = new Paralelka(_paralelka, new List<Ucenik>(), new Dictionary<string, Smer>());
                if (_smerovi != "") Smerovi = _smerovi.Split(',').ToList();
            }
            else
            {
                Smerovi = _p._smerovi.Keys.ToList();
            }

            Smerovi = LoadDefaultSmerovi(Smerovi , token);
            if( Smerovi.Count > 0) GetSmerPredmeti(Smerovi);
        }

        
        private List<string> LoadDefaultSmerovi(List<string> Smerovi , string token)
        {
            if (!Smerovi.Contains("ПА")) _p.AddSmer(new Smer("ПА", "цел смер"), token);
            if (!Smerovi.Contains("Странски Јазици")) _p.AddSmer(new Smer("Странски Јазици", "цел смер"), token);
            if (!Smerovi.Contains("Изборни Предмети")) _p.AddSmer(new Smer("Изборни Предмети", "цел смер"), token);
            return Smerovi;
        }


        public void GetSmerPredmeti(List<string> Smerovi)
        {
            if (Smerovi.Count == 0) return;
            foreach (var x in Smerovi)
            {
                var req = Requests.GetData(new Dictionary<string, string>(){
                    { RequestParameters.token, _token},
                    { RequestParameters.smer, x } ,
                    { RequestParameters.paralelka, _paralelka}
                }, RequestScopes.GetPredmetiSmer)[0];

                List<string> predmeti = req["predmeti"].Split(',').ToList();
                if (req["predmeti"].Length == 0) predmeti = new List<string>();

                Smer NovSmer = new Smer(predmeti, x, req["cel_smer"]);

                if (!_p._smerovi.Keys.Contains(x)) _p._smerovi.Add(x, NovSmer);
                else _p._smerovi[x]._predmeti = predmeti;
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
