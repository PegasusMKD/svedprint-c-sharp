using Newtonsoft.Json;
using System;

namespace AdminPanel.Middleware.Models
{
    public class Ucenik
    {
        [JsonProperty(JSONRequestParameters.Ucenik.Ime)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.SrednoIme)]
        public string SrednoIme { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Prezime)]
        public string Prezime { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Paralelka)]
        public string Paralelka { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Tatko)]
        public string Tatko{ get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Majka)]
        public string Majka { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Roden)]
        public string Roden { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Smer)]
        public string Smer { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.CelSmer)]
        public string CelSmer { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Broj)]
        public string Broj { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Pol)]
        public string Pol { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.PrethodnaGodina)]
        public string PrethodnaGodina{ get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.PrethodenUspeh)]
        public string PrethodenUspeh { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Polozhil)]
        public string Polozhil { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.Counter)]
        public string Counter { get; set; }
        [JsonProperty(JSONRequestParameters.Ucenik.PolagalOceni)]
        public int[] PolagalOceni { get; set; }

        public bool transferClass = false;

        public bool transferClassView { get { return this.transferClass; } set { if (value != this.transferClass) this.transferClass = value; } }
    }
}