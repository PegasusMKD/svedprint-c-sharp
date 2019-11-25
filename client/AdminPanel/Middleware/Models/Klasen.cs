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
            {"Klas",JSONRequestParameters.Klasen.Klas },
            {"Password",JSONRequestParameters.Klasen.PasswordUpdated }
        };

        [JsonProperty(JSONRequestParameters.Klasen.Ime)]
        public string Ime { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.SrednoIme)]
        public string SrednoIme { get; set; }
        [JsonProperty(JSONRequestParameters.Klasen.Prezime)]
        public string Prezime { get; set; }

        [JsonProperty(JSONRequestParameters.Klasen.Username)]
        public string Username { get; set; }

        public string Password { get; set; }

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

        public string ImeBind
        {
            get { return this.Ime; }
            set
            {
                if (value != this.Ime)
                {
                    this.Ime = value;
                    NotifyPropertyChanged("Ime");
                }
            }
        }

        public string PasswordBind
        {
            get {
                if (this.Password != null) return this.Password;
                else return "";
            }
            set
            {
                if (value != this.Password)
                {
                    this.Password = value;
                    NotifyPropertyChanged("Password");
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
                    PoleNPC("Prezime");

                }
            }
        }

        public void PoleNPC(string pole)
        {
            NotifyPropertyChanged(pole);
        }

        public List<Pole> Polinja;
        public List<Pole> GetPolinja(Dictionary<string, List<Klasen>> users)
        {

           Polinja = new List<Pole>
                    {
                        new Pole ("Корисничко име" , new string[] { UsernameBind } , "UsernameBind" , this),
                        new Pole ("Лозинка" ,  new string[] { PasswordBind } , "PasswordBind" , this, "PW"),
                        new Pole ("Име" , new string[] { ImeBind } , "ImeBind"  , this),
                        new Pole ("Презиме" , new string[] { PrezimeBind } , "PrezimeBind"  ,this ),
                        //new Pole ("PMA" , new string[] { "mat" , "mak" , "ger" , "asdf" , "fdas" , "dfass"  } , "parametar" , "Predmeti" , "dfass" ),
                    };
            return Polinja;
        }
    }
}
