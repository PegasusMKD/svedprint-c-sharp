using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

//namespace Middleware
//{
//    public class Ucenik
//    {
//        [JsonProperty(RequestParameters.ime)]
//        public string _ime { get; set; }
       

//        public Ucenik(string ime, string srednoIme, string prezime, List<int> oceni, string smer, int broj, string roden, string mesto_na_zhiveenje, string mesto_na_ragjanje, string povedenie, int opravdani, int neopravdani, string tip, string pat_polaga, string tatko, string majka, string gender, string maturska, string izborni, string proektni, string merki, string prethodna_godina, string prethoden_uspeh, string prethodno_uchilishte, string delovoden_broj, string datum_sveditelstvo, string polozhil, string prethodna_uchebna, string pedagoshki_merki, string drzavjanstvo, string pat_polaga_ispit, string ispiten, int duplicate_ctr,
//            string jazik, string jazik_ocena, string polagal, string polozhil_matura)
//        { //string majkino,
//            _ime = ime ?? "";
//            _srednoIme = srednoIme ?? "";
//            _prezime = prezime ?? "";
//            _oceni = oceni ?? new List<int>();
//            _smer = smer ?? "";
//            //Pazzio: broj napravi go da se stava taka kako shto ti gi prakjam, odnosno, da go zima brojot vo dictionary-to, i toa da go koristi za broj vo dnevnik
//            _broj = broj;
//            _roden = roden ?? "01.01.1111";
//            _mesto_na_zhiveenje = mesto_na_zhiveenje ?? "";
//            _mesto_na_ragjanje = mesto_na_ragjanje ?? "";
//            _povedenie = povedenie ?? "";
//            _opravdani = opravdani;
//            _neopravdani = neopravdani;
//            _tip = tip ?? "";
//            _pat_polaga = pat_polaga ?? "-1";
//            _majka = majka ?? "";
//            _tatko = tatko ?? "";
//            _gender = gender ?? "";
//            //Predmet, ocena, datum na polaganje, mozhebi 
//            _maturska = maturska ?? "";
//            SetMaturski(maturska);
//            //Treba da e lista, kako e vo sushtina e Predmet,dali polozhil;predmet,dali go polozhil...
//            _izborni = izborni ?? "";
//            _proektni = proektni ?? "";
//            _pedagoski_merki = merki ?? "";
//            _prethodna_godina = prethodna_godina ?? "";
//            _prethoden_uspeh = prethoden_uspeh ?? "";
//            _prethodno_uchilishte = prethodno_uchilishte ?? "";
//            _prethodna_uchebna = prethodna_uchebna ?? "";
//            //Ova se zema od kaj Klasniot
//            //_delovoden_broj = delovoden_broj ?? "";
//            //_datum_sveditelstvo = datum_sveditelstvo ?? "";

//            _polozhil = polozhil ?? "";
//            // _majkino = majkino ?? "";

//            _pat_polaga_ispit = pat_polaga_ispit ?? "";
//            _ispiten = ispiten ?? "";

//            _drzavjanstvo = drzavjanstvo ?? "";

//            _duplicate_ctr = duplicate_ctr;

//            //Dodatoci od Pazzio
//            _jazik = jazik ?? "";
//            _jazik_ocena = jazik_ocena ?? "";
//            _polagal = polagal ?? "";
//            _polozhil_matura = polozhil_matura ?? "";
//        }

      
//        public async Task<string> UpdateUcenikData(Dictionary<string, string> UpdatedData, Dictionary<string, string> OrigData, string token)
//        {
//            Dictionary<string, string> queryParams = new Dictionary<string, string>(UpdatedData);
//            queryParams[nameof(token)] = token;
//            queryParams.Add(RequestParameters.duplicate_ctr, _duplicate_ctr.ToString());
//            if (OrigData.ContainsKey("ime"))
//            {
//                queryParams["new_first_name"] = queryParams["ime"];
//                queryParams["ime"] = OrigData["ime"];
//            }
//            if (OrigData.ContainsKey("prezime"))
//            {
//                queryParams["new_last_name"] = queryParams["prezime"];
//                queryParams["prezime"] = OrigData["prezime"];
//            }
//            if (OrigData.ContainsKey("srednoIme"))
//            {
//                queryParams["new_middle_name"] = queryParams["srednoIme"];
//                queryParams["srednoIme"] = OrigData["srednoIme"];
//            }
//            if (OrigData.ContainsKey("broj"))
//            {
//                queryParams["new_broj_vo_dnevnik"] = queryParams["broj"];
//                queryParams["broj"] = OrigData["broj"];
//            }
//            string rez = await Requests.UpdateDataAsync(queryParams, "ucenik");
//            this._ime = UpdatedData["ime"];
//            this._prezime = UpdatedData["prezime"];
//            this._srednoIme = UpdatedData["srednoIme"];
//            this._smer = UpdatedData["smer"];
//            this._broj = int.Parse(UpdatedData["broj"]);
//            this._tatko = UpdatedData["tatko"];
//            this._majka = UpdatedData["majka"];
//            this._roden = UpdatedData["roden"];
//            this._mesto_na_ragjanje = UpdatedData["mesto_na_ragjanje"];
//            this._gender = UpdatedData["gender"];
//            this._roden = UpdatedData["roden"];
//            this._mesto_na_ragjanje = UpdatedData["mesto_na_ragjanje"];
//            this._mesto_na_zhiveenje = UpdatedData["mesto_na_zhiveenje"];
//            this._pat_polaga = UpdatedData["pat"];
//            this._polozhil = UpdatedData["polozhil"];
//            if (UpdatedData.Keys.Contains(RequestParameters.pedagoshki_merki)) _pedagoski_merki = UpdatedData[RequestParameters.pedagoshki_merki];
//            if (UpdatedData.Keys.Contains(RequestParameters.povedenie)) _povedenie = UpdatedData[RequestParameters.povedenie];
//            if (UpdatedData.Keys.Contains(RequestParameters.izborni)) _izborni = UpdatedData[RequestParameters.izborni];
//            if (UpdatedData.Keys.Contains(RequestParameters.jazik)) _jazik = UpdatedData[RequestParameters.jazik];
//            /*
//            int result = 0;
//            int.TryParse(UpdatedData["opravdani"], out result);
//            this._opravdani = result;
//            int.TryParse(UpdatedData["neopravdani"], out result);
//            this._neopravdani = result;
//            this._povedenie = UpdatedData["povedenie"];*/
//            //_pedagoski_merki = UpdatedData[RequestParameters.pedagoshki_merki];
//            _prethodna_godina = UpdatedData[RequestParameters.prethodna_godina];
//            _prethoden_uspeh = UpdatedData[RequestParameters.prethoden_uspeh];
//            _prethodno_uchilishte = UpdatedData[RequestParameters.prethodno_uchilishte];
//            return rez;
//        }


namespace Middleware{
    class Request
    {
        public string _scope;
        public Dictionary<string, string> _queryParams;

        public Request(string scope, Dictionary<string, string> queryParams)
        {
            _scope = scope ?? "";
            _queryParams = queryParams ?? new Dictionary<string, string>();
        }
        public Request() { }
    }

    class Admin{
        public class RequestParameters
        {
            public const string printAllowed = "printAllowed";
            public const string token = "token";
        }


        [JsonProperty(RequestParameters.token)]
        public string token { get; set; }

        [JsonProperty(RequestParameters.printAllowed)]
        public bool printAllowed { get; set; }


    }

}