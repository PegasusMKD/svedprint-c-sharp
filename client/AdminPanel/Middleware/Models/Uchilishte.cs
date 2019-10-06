using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Middleware.Models
{
    public class Uchilishte : INotifyPropertyChanged
    {
        /// <summary>
        /// An automagically updated list of changed values on the object
        /// </summary>
        public List<string> changed_properties = new List<string>();

        /// <summary>
        /// Pairings of Middleware fields with their server-side counterparts
        /// </summary>
        public Dictionary<string, string> pairs = new Dictionary<string, string>() {
            {"Ime",JSONRequestParameters.Admin.UchilishteImeUpdate },
            {"DelovodenBroj",JSONRequestParameters.Admin.DelovodenBroj },
            {"OdobrenoSveditelstvo",JSONRequestParameters.Admin.OdobrenoSveditelstvo },
            {"Ministerstvo",JSONRequestParameters.Admin.Ministerstvo },
            {"GlavnaKniga",JSONRequestParameters.Admin.GlavnaKniga },
            {"Akt",JSONRequestParameters.Admin.Akt },
            {"AktGodina",JSONRequestParameters.Admin.AktGodina },
            {"Direktor",JSONRequestParameters.Admin.Direktor },
            {"MozniDatiMatura",JSONRequestParameters.Admin.MozniDatiMatura },
            {"MozniDatiSveditelstva",JSONRequestParameters.Admin.MozniDatiSveditelstva },

            //Ne e staven update za ova vo back end!
            {"DataMatura",JSONRequestParameters.Admin.DataMatura },
            {"DataSveditelstva",JSONRequestParameters.Admin.DataSveditelstva }
        };


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string pair, [CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (!this.changed_properties.Contains(pair))
            {
                this.changed_properties.Add(pair);
            }
        }



        [JsonProperty(JSONRequestParameters.Admin.Uchilishte)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DelovodenBroj)]
        public string DelovodenBroj { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.OdobrenoSveditelstvo)]
        public string OdobrenoSveditelstvo { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Ministerstvo)]
        public string Ministerstvo { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.GlavnaKniga)]
        public string GlavnaKniga { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Akt)]
        public string Akt { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.AktGodina)]
        public string AktGodina { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.Direktor)]
        public string Direktor { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.MozniDatiMatura)]
        public List<string> MozniDatiMatura { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.MozniDatiSveditelstva)]
        public List<string> MozniDatiSveditelstva { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DataMatura)]
        public string DataMatura { get; set; }
        [JsonProperty(JSONRequestParameters.Admin.DataSveditelstva)]
        public string DataSveditelstva { get; set; }



        public string ImeView { get { return this.Ime; } set
            {
                if (value != this.Ime)
                {
                    this.Ime = value;
                    NotifyPropertyChanged("Ime");
                }
            } }
        public string DelovodenBrojView { get { return this.DelovodenBroj; }
            set {
                    if (value != this.DelovodenBroj)
                    {
                        NotifyPropertyChanged("DelovodenBroj");
                        this.DelovodenBroj = value;
                    }
            } }
        public string OdobrenoSveditelstvoView { get { return this.OdobrenoSveditelstvo; } set
            {
                if (value != this.OdobrenoSveditelstvo)
                {
                    NotifyPropertyChanged("OdobrenoSveditelstvo");
                    this.OdobrenoSveditelstvo = value;
                }
            } }
        public string MinisterstvoView { get { return this.Ministerstvo; } set
            {
                if (value != this.Ministerstvo)
                {
                    NotifyPropertyChanged("Ministerstvo");
                    this.Ministerstvo = value;
                }
            } }
        public string GlavnaKnigaView { get { return this.GlavnaKniga; } set
            {
                if (value != this.GlavnaKniga)
                {
                    NotifyPropertyChanged("GlavnaKniga");
                    this.GlavnaKniga = value;
                }
            } }
        public string AktView { get { return this.Akt; } set
            {
                if (value != this.Akt)
                {
                    NotifyPropertyChanged("Akt");
                    this.Akt = value;
                }
            } }
        public string AktGodinaView { get { return this.AktGodina; } set
            {
                if (value != this.AktGodina)
                {
                    NotifyPropertyChanged("AktGodina");
                    this.AktGodina = value;
                }
            } }
        public string DirektorView { get { return this.Direktor; } set
            {
                if (value != this.Direktor)
                {
                    NotifyPropertyChanged("Direktor");
                    this.Direktor = value;
                }
            }
        }
        public List<string> MozniDatiMaturaView { get { return this.MozniDatiMatura; } set
            {
                if (value != this.MozniDatiMatura)
                {
                    NotifyPropertyChanged("MozniDatiMatura");
                    this.MozniDatiMatura = value;
                }
            } }
        public List<string> MozniDatiSveditelstvaView { get { return this.MozniDatiSveditelstva; } set
            {
                if (value != this.MozniDatiSveditelstva)
                {
                    NotifyPropertyChanged("MozniDatiSveditelstva");
                    this.MozniDatiSveditelstva = value;
                }
            } }
        public string DataMaturaView { get { return this.DataMatura; } set
            {
                if (value != this.DataMatura)
                {
                    NotifyPropertyChanged("DataMatura");
                    this.DataMatura = value;
                }
            } }
        public string DataSveditelstvaView { get { return this.DataSveditelstva; } set
            {
                if (value != this.DataSveditelstva)
                {
                    NotifyPropertyChanged("DataSveditelstva");
                    this.DataSveditelstva = value;
                }
            } }
    }
}
