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
    public class Klasen : INotifyPropertyChanged
    {

        public List<string> changed_properties = new List<string>();

        public Dictionary<string, string> pairs = new Dictionary<string, string>() {
            {"Ime",JSONRequestParameters.Klasen.Ime },
            {"SrednoIme",JSONRequestParameters.Klasen.SrednoIme },
            {"Prezime",JSONRequestParameters.Klasen.Prezime },
            {"Username",JSONRequestParameters.Klasen.UsernameUpdated },
            {"Klas",JSONRequestParameters.Klasen.Klas }
        };

        [JsonProperty(JSONRequestParameters.Klasen.Ime)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.SrednoIme)]
        public string SrednoIme { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.Prezime)]
        public string Prezime { get; set; }

        [JsonProperty(JSONRequestParameters.Klasen.Username)]
        public string Username { get; set; }

        public string UsernamePERMA { get; set; }

        [JsonProperty(JSONRequestParameters.Klasen.Klas)]
        public string Klas { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string pair, [CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (!this.changed_properties.Contains(pair))
            {
                this.changed_properties.Add(pair);
            }
        }

        public string SrednoImeBind
        {
            get { return this.SrednoIme; }
            set
            {
                if (value != this.SrednoIme)
                {
                    this.SrednoIme = value;
                    NotifyPropertyChanged("SrednoIme");
                }
            }
        }

        public string UsernameBind
        {
            get { return Username; }
            set
            {
                if (value != this.Username)
                {
                    if (UsernamePERMA == null)
                    {
                        UsernamePERMA = Username;
                    }

                    this.Username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }

        public string PrezimeBind
        {
            get { return this.Prezime; }
            set
            {
                if (value != this.Prezime)
                {
                    this.Prezime = value;
                    NotifyPropertyChanged("Prezime");
                }
            }
        }
    }
}
