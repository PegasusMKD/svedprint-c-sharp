﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        [JsonProperty(RequestParameters.eksterni)] // format - "predmet:ocena;predmet:ocena"
        public string _eksterni { get; set; }
        [JsonProperty(RequestParameters.interni)] // format - "predmet:ocena;predmet:ocena"
        public string _interni { get; set; }
        [JsonProperty(RequestParameters.percentilen)] // format - "ocena1 ocena2 ocena3"
        public string _percentilen { get; set; }
        [JsonProperty(RequestParameters.delovoden_predmeti)] // format - "delrb1 delbr2 delbr3"
        public string _delovoden_predmeti { get; set; }
        [JsonProperty(RequestParameters.polagal)]
        public string _polagal { get; set; } // array with ' ' delimiter

        /// <summary>
        /// prvite 3 se eksternite, poslednata e proektnata i site izmegju se internite
        /// </summary>
        public List<(string predmet, int ocena, decimal percentilen, string datum, string delovoden)> _maturski { get; set; }

        public string MaturskiToString()
        {
            string ret;
            using (var sw = new StringWriter())
            {
                sw.Write(string.Join("&", _maturski.ConvertAll(x => $"{x.predmet}|{x.ocena}|{x.percentilen.ToString("00.00").Replace(',', '.')}|{x.datum}|{x.delovoden}")));
                ret = sw.ToString();
            }
            return ret;
        }

        public void SetMaturski(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                _maturski = new List<(string predmet, int ocena, decimal percentilen, string datum, string delovoden)>();
                return;
            }
            if (_maturski == null)
            {
                _maturski = new List<(string predmet, int ocena, decimal percentilen, string datum, string delovoden)>();
            }
            _maturski.Clear();
            var x = val.Split('&');
            foreach (var k in x)
            {
                var data = k.Split('|');
                _maturski.Add((data[0], int.Parse(data[1]), decimal.Parse(data[2]), data[3], data[4]));
            }
        }

        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string delovoden_broj, string datum_sveditelstvo, string polozhil, string prethodna_uchebna, string pedagoshki_merki, string drzavjanstvo, string pat_polaga_ispit, string ispiten, int duplicate_ctr,
            string jazik, string jazik_ocena, string polagal)
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
            SetMaturski(maturska);
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

            _pat_polaga_ispit = pat_polaga_ispit ?? "";
            _ispiten = ispiten ?? "";

            _drzavjanstvo = drzavjanstvo ?? "";

            _duplicate_ctr = duplicate_ctr;

            //Dodatoci od Pazzio
            _jazik = jazik ?? "";
            _jazik_ocena = jazik_ocena ?? "";
            _polagal = polagal ?? "";
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

        public List<MaturskiPredmet> MaturskiPredmeti = new List<MaturskiPredmet>();

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
            // _prethoden_delovoden = valuePairs[RequestParameters.prethoden_delovoden] ?? "";
            _tatko = valuePairs[RequestParameters.tatko] ?? "";
            _majka = valuePairs[RequestParameters.majka] ?? "";
            _gender = valuePairs[RequestParameters.gender] ?? "";
            _maturska = valuePairs[RequestParameters.maturska] ?? "";
            SetMaturski(_maturska);
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

            //MaturskiPredmeti
            //LoadMaturski();
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
            if (UpdatedData.Keys.Contains(RequestParameters.izborni)) _izborni = UpdatedData[RequestParameters.izborni];
            if (UpdatedData.Keys.Contains(RequestParameters.jazik)) _jazik = UpdatedData[RequestParameters.jazik];
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

        public Dictionary<string, string> GetPolinja()
        {
            Dictionary<string, string> polinja = new Dictionary<string, string>();
            polinja.Add(RequestParameters.ime, _ime);
            polinja.Add(RequestParameters.prezime, _prezime);
            polinja.Add(RequestParameters.srednoIme, _srednoIme);
            polinja.Add(RequestParameters.smer, _smer);
            polinja.Add(RequestParameters.tatko, _tatko);
            polinja.Add(RequestParameters.majka, _majka);
            polinja.Add(RequestParameters.broj, _broj.ToString());
            polinja.Add(RequestParameters.gender, _gender);
            polinja.Add(RequestParameters.roden, _roden);
            polinja.Add(RequestParameters.mesto_na_ragjanje, _mesto_na_ragjanje);
            polinja.Add(RequestParameters.mesto_na_zhiveenje, _mesto_na_zhiveenje);
            polinja.Add(RequestParameters.pat_polaga, _pat_polaga);
            polinja.Add(RequestParameters.polozhil, _polozhil);
            polinja.Add(RequestParameters.povedenie, _povedenie);
            polinja.Add(RequestParameters.opravdani, _opravdani.ToString());
            polinja.Add(RequestParameters.neopravdani, _neopravdani.ToString());
            polinja.Add(RequestParameters.proektni, _proektni);
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
            int n = _oceni.Count;
            var polagal_oceni = _polagal.Split(' ');
            for (int i = 0; i < n; i++)
            {
                if (_oceni[i] == 1)
                {
                    if (polagal_oceni[i] == "0")
                    {
                        checker = false;
                        break;
                    }
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

        public void UpdateProektni(int i, string cb_pole, bool Isrealised, string token)
        {

            string realizirano;
            if (Isrealised) realizirano = ",Реализирал";
            else realizirano = ",Не реализирал";

            List<string> tx = _proektni.Split(';').ToList();
            tx[i] = cb_pole + realizirano;
            string rez = string.Join(";", tx);
            // rez = rez.Substring(0, rez.Length - 1);


            UpdateUcenik(RequestParameters.proektni, rez, token);
            _proektni = rez;
        }

        public void UpdateUcenikOceni(string Token)
        {
            //UpdateUcenik(br, RequestParameters.oceni, OceniToString(), Token);
            UpdateUcenik(RequestParameters.oceni, string.Join(" ", _oceni), Token);
        }

        public void UpdateMaturska(string Token)
        {
            string UpdateStr = "";
            string Delimetar = "&";
            foreach (MaturskiPredmet Predmet in MaturskiPredmeti)
            {
                UpdateStr += Predmet.GetOutParam();
                UpdateStr += Delimetar;
            }

            UpdateStr = UpdateStr.Substring(0, UpdateStr.Length - 1);

            UpdateUcenik(RequestParameters.maturska, UpdateStr, Token);
            _maturska = UpdateStr;
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

        public void LoadMaturski(Klasen UserKlas)//Eksteren1|Makedonski|Ocenka|5|Percentiran|00.0|Datum|01.01.2004|delovoden|2/5|&Eksteren2|Makedonski|Ocenka|5|Percentiran|20.00|Datum|01.01.2004|delovoden|2/5|
        {
            //_maturska = "Eksteren1|Makedonski|Ocenka|5|Percentiran||Datum|01.01.2004|delovoden|2/5&Eksteren2|Makedonski|Ocenka|5|Percentiran|20.00|Datum|01.01.2004|delovoden|2/5";
            List<string> _predmeti = new List<string> { };

            if (UserKlas._p._smerovi.Keys.Contains("Матурски Предмети")) _predmeti = UserKlas._p._smerovi["Матурски Предмети"]._predmeti;

            if (_maturska == null || _maturska == "")
            {
                MaturskiPredmeti.Add(new MaturskiPredmet("Eksteren1", _predmeti.ToArray(), null));
                MaturskiPredmeti.Add(new MaturskiPredmet("Eksteren2", _predmeti.ToArray(), null));
                MaturskiPredmeti.Add(new MaturskiPredmet("Eksteren3", _predmeti.ToArray(), null));
                MaturskiPredmeti.Add(new MaturskiPredmet("Interen", _predmeti.ToArray(), null));
                MaturskiPredmeti.Add(new MaturskiPredmet("Proektna", new string[] { }, null));
                return;
            }


            string[] predmeti = _maturska.Split('&');
            int naming_counter = 0;
            List<string> possible_naming = new List<string>() { "Екстерен Предмет 1", "Ектерен Предмет 2", "Екстерен Предмет 3", "Интерен Предмет", "Проектна задача" };
            List<string> possible_fields = new List<string>() { "Име", "Оценка", "Перцентилен", "Датум", "Деловоден број" };
            List<string> possible_values = new List<string>() { "Име на предмет", "Добиена оценка", "Перцентилен ранг", "Датум на полагање", "Деловоден број на                                                 записник" };
            foreach (string predmet in predmeti)
            {
                string[] fields = predmet.Split('|');

                List<MaturskoPole> MaturskiPolinja = new List<MaturskoPole>();
                int counter = 0;
                foreach (string field in fields)
                {
                    MaturskiPolinja.Add(new MaturskoPole(possible_fields[counter], possible_values[counter], field));
                    counter++;
                }
                MaturskiPredmeti.Add(new MaturskiPredmet(possible_naming[naming_counter], _predmeti.ToArray(), MaturskiPolinja, fields[0]));

                naming_counter++;

            }
        }

        //    string[] Predmeti = _maturska.Split('&');

        //    foreach (string Predmet in Predmeti)
        //    {
        //        string[] Polinja = Predmet.Split('|');

        //        List<MaturskoPole> MaturskiPolinja = new List<MaturskoPole>();
        //        for (int i = 2; i < Polinja.Length; i += 2)
        //        {
        //            string defaultvrednost = "    ";
        //            if (DefaultDic.ContainsKey(Polinja[i])) defaultvrednost = DefaultDic[Polinja[i]];

        //            MaturskiPolinja.Add(new MaturskoPole(Polinja[i], defaultvrednost, Polinja[i + 1]));
        //        }

        //        MaturskiPredmeti.Add(new MaturskiPredmet(Polinja[0], new string[] { }, MaturskiPolinja, Polinja[1]));
        //    }
        //}

        //Dictionary<string, string> DefaultDic = new Dictionary<string, string>()
        //{ {"Ocenka", "5" }  , { "Percentiran" , "0.00" } , { "Datum" , "0.0.2002"} };

    }



    public class Smer
    {
        [JsonProperty(RequestParameters.predmeti)]
        public List<string> _predmeti { get; set; }
        [JsonProperty(RequestParameters.smer)]
        public string _smer { get; set; }
        [JsonProperty(RequestParameters.cel_smer)]
        public string _cel_smer { get; set; }
        int[] jaziciPos = new int[] { -1, -1 };

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

        public List<string> GetCeliPredmeti(string jazici, string izbp, Dictionary<string, Smer> Smerovi)
        {
            int i = 0, j = 0, izbctr = 0;
            List<string> sj = new List<string>();
            if (Smerovi.Keys.Contains("Странски Јазици"))
            {
                // ne znam zosto, ama jazici kaj nekoi e x;y; - ima visok ; na kraj sto go buni kodot
                if (jazici != null && jazici != "" && (jazici.Length == 3 || jazici.Length == 4))
                {
                    var arr = jazici.Split(new char[] { ';', ':' }); // hardcoded
                    i = int.Parse(arr[0]);
                    j = int.Parse(arr[1]);
                }
                sj = Smerovi["Странски Јазици"]._predmeti;
            }
            else sj.Add("");

            string izboren = "";
            if (Smerovi["Изборни Предмети"]._predmeti.Count > 0)
            {
                if (izbp != null && izbp != "") izbctr = int.Parse(izbp);
                else izbctr = -1;

                if (izbctr != -1) izboren = Smerovi["Изборни Предмети"]._predmeti[izbctr];
            }

            List<string> predmeti = new List<string>();

            if(jaziciPos[0] >= 0 && jaziciPos[1] >= 0) {
                foreach (string predmet in _predmeti)
                {
                    predmeti = _predmeti;
                    predmeti[jaziciPos[0]] = sj[i];
                    predmeti[jaziciPos[1]] = sj[j];
                }
            } else
            {
                predmeti = _predmeti;
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
            if (predmeti.Count > 0 && !string.IsNullOrWhiteSpace(predmeti[0]["predmeti"]))
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

        public Dictionary<string, Smer> GetSmerovi()
        {
            Dictionary<string, Smer> rez = new Dictionary<string, Smer>();
            foreach (KeyValuePair<string, Smer> x in _smerovi)
            {
                if (x.Key != "ПА" && x.Key != "Странски Јазици" && x.Key != "Изборни Предмети") rez.Add(x.Key, x.Value);
            }
            return rez;
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

            Smerovi = LoadDefaultSmerovi(Smerovi, token);
            if (Smerovi.Count > 0) GetSmerPredmeti(Smerovi);
        }


        private List<string> LoadDefaultSmerovi(List<string> Smerovi, string token)
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

    public class MaturskiPredmet
    {
        public string Ime;
        public string IzbranPredmet = "";
        public string[] MozniPredmeti;
        public List<MaturskoPole> MaturskiPolinja = new List<MaturskoPole>();

        public MaturskiPredmet(string ime, string[] moznipredmeti, List<MaturskoPole> maturskipolinja, string izbranPredmet = "")
        {
            Ime = ime;
            MozniPredmeti = moznipredmeti;

            if (izbranPredmet != "") IzbranPredmet = izbranPredmet;
            if (MozniPredmeti.Length > 0 && IzbranPredmet == "") IzbranPredmet = moznipredmeti[0];

            //MaturskiPolinja
            if (maturskipolinja == null)
            {
                if (ime == "Proektna")
                    MaturskiPolinja.Add(new MaturskoPole("Ime", "Ime na Proektna"));

                MaturskiPolinja.Add(new MaturskoPole("Ocenka", "5"));
                if (ime != "Interen" && ime != "Proektna")
                    MaturskiPolinja.Add(new MaturskoPole("Percentiran", "00.00"));
                MaturskiPolinja.Add(new MaturskoPole("Datum", "01.01.2004"));
                MaturskiPolinja.Add(new MaturskoPole("delovoden", "2/5"));
            }
            else MaturskiPolinja = maturskipolinja;
        }

        public string GetOutParam()
        {
            string rez = Ime;
            string Delimetar = "|";
            rez += Delimetar;
            rez += IzbranPredmet;
            rez += Delimetar;
            foreach (MaturskoPole Pole in MaturskiPolinja)
            {
                rez += Pole.Ime;
                rez += Delimetar;
                rez += Pole.GetVrednost();
                rez += Delimetar;
            }

            rez = rez.Substring(0, rez.Length - 1);

            return rez;
        }
    }

    public class MaturskoPole
    {
        public string Ime;
        public string Vrednost = "";
        public string DefaultVrednost;

        public MaturskoPole(string ime, string defaultvrednost, string vrednost = "")
        {
            Ime = ime;
            DefaultVrednost = defaultvrednost;
            if (vrednost != "") Vrednost = vrednost;
        }

        public string GetVrednost()
        {
            if (Vrednost == "") return DefaultVrednost;
            else return Vrednost;
        }

        public void SetVrednost(string vred)
        {
            if (vred == "") return;
            else Vrednost = vred;
        }

    }
}
