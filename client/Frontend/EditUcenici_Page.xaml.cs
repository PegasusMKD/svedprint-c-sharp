using Middleware;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Frontend.SettingsDesign;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für EditUcenici_Page.xaml
    /// </summary>
    public partial class EditUcenici_Page : Page
    {

        Klasen UserKlas;
        List<Ucenik> Ucenici;
        int BrojDn = 0;

        private readonly Dictionary<string, string> names = new Dictionary<string, string>(){
      {"Име", RequestParameters.ime},
      {"Презиме", RequestParameters.prezime},
      {"Средно Име", RequestParameters.srednoIme},
      {"Смер", RequestParameters.smer},
      {"родител(Татко)",RequestParameters.tatko},
      {"родител(Мајка)",RequestParameters.majka},
      {"број во дневник",RequestParameters.broj},
      { "Државјанство",RequestParameters.drzavjanstvo},
      {"Пол",RequestParameters.gender},
      {"ден на раѓање",RequestParameters.roden},
      {"место на раѓање",RequestParameters.mesto_na_ragjanje},
      {"место на живеење",RequestParameters.mesto_na_zhiveenje},
      {"по кој пат ја учи годината",RequestParameters.pat_polaga},
      {"дали е положена годината",RequestParameters.polozhil},
      {"број на оправдани изостаноци",RequestParameters.opravdani},
      {"број на неоправдани изостаноци",RequestParameters.neopravdani},
      {"Поведение",RequestParameters.povedenie}
};


        public EditUcenici_Page()
        {
            InitializeComponent();
            UserKlas = Home_Page.KlasenKlasa;
            Ucenici = Home_Page.ucenici;
            Refresh();
        }

        List<TextBox> Answer;
        List<ComboBox> CBList;
        private void GetData5()
        { 
            /*
            Answer = new List<TextBox>();
            CBList = new List<ComboBox>();
            MainGrid.Children.Clear();
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("Име", "Име");
            dictionary.Add("Презиме", "Презиме");
            dictionary.Add("Средно Име", "Средно Име");
            dictionary.Add("Смер", "Смер");
            dictionary.Add("родител(Татко)", "Име Презиме");
            dictionary.Add("родител(Мајка)", "Име Презиме");
            dictionary.Add("број во дневник", "00");
            dictionary.Add("Државјанство", "Македонско");
            dictionary.Add("Пол", "Машки/Женски");
            dictionary.Add("ден на раѓање", "00.00.0000");
            dictionary.Add("место на раѓање", "Скопје");
            dictionary.Add("место на живеење", "Скопје");
            dictionary.Add("по кој пат ја учи годината", "прв/втор пат");
            dictionary.Add("дали е положена годината", "Положил");
            dictionary.Add("број на оправдани изостаноци", "0");
            dictionary.Add("број на неоправдани изостаноци", "0");
            dictionary.Add("Поведение", "Примeрно");

            if (Ucenici.Count > BrojDn)
            {
                dictionary["Име"] = Ucenici[BrojDn]._ime;
                dictionary["Презиме"] = Ucenici[BrojDn]._prezime;
                dictionary["Средно Име"] = Ucenici[BrojDn]._srednoIme;
                dictionary["Смер"] = Ucenici[BrojDn]._smer;
                Ucenici[BrojDn]._broj = BrojDn;
                dictionary["број во дневник"] = (Ucenici[BrojDn]._broj + 1).ToString();
                dictionary["родител(Татко)"] = Ucenici[BrojDn]._tatko;
                dictionary["родител(Мајка)"] = Ucenici[BrojDn]._majka;
                dictionary["Државјанство"] = Ucenici[BrojDn]._drzavjanstvo;
                dictionary["Пол"] = Ucenici[BrojDn]._gender;
                dictionary["ден на раѓање"] = Ucenici[BrojDn]._roden;
                dictionary["место на раѓање"] = Ucenici[BrojDn]._mesto_na_ragjanje;
                dictionary["место на живеење"] = Ucenici[BrojDn]._mesto_na_zhiveenje;
                dictionary["по кој пат ја учи годината"] = Ucenici[BrojDn]._pat_polaga;
                dictionary["дали е положена годината"] = Ucenici[BrojDn]._polozhil;
                dictionary["број на оправдани изостаноци"] = Ucenici[BrojDn]._opravdani.ToString();
                dictionary["број на неоправдани изостаноци"] = Ucenici[BrojDn]._neopravdani.ToString();
                dictionary["Поведение"] = Ucenici[BrojDn]._povedenie;
            }
            MainGrid.Height = 0.0;
            int index = 0;
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                StackPanel stackPanel = new StackPanel();
                if (index % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100.0);
                    MainGrid.Height += 100.0;
                }
                TextBox textBox = ContentTextBox(keyValuePair.Value);
                textBox.Name = names[keyValuePair.Key];
                Answer.Add(textBox);
                stackPanel.Children.Add(ContentBorder(keyValuePair.Key));
                stackPanel.Children.Add(Answer[index]);
                stackPanel.Children.Add(UnderTextBorder());
                Grid.SetRow(stackPanel, index / 2);
                Grid.SetColumn(stackPanel, index % 2);
                MainGrid.Children.Add(stackPanel);
                ++index;
            }

            string[][] cb = new string[][] { new string[] { "Проектни Активности", "proektni", "izboren1_odg", }, new string[] { "Проектни Активности", "proektni", "izboren2_odg" } };
            foreach (string[] x in cb)
            {
                StackPanel stackPanel = new StackPanel();
                if (index % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100.0);
                    MainGrid.Height += 130.0;
                }

                ComboBox CB = new ComboBox();
                CB.Tag = x[1];
                CB.Margin = new Thickness(30, 5, 30, 5);
                if (UserKlas._p._smerovi.ContainsKey("ПА"))
                {
                    CB.ItemsSource = UserKlas._p._smerovi["ПА"]._predmeti;
                    CB.SelectedIndex = 0;
                }
                CBList.Add(CB);

                stackPanel.Children.Add(ContentBorder(x[0]));
                stackPanel.Children.Add(CBList.Last());

                string[] odgovori = { "Реализирал", "Не Реализирал" };
                ComboBox res = new ComboBox();
                res.Tag = x[2];
                res.Margin = new Thickness(30, 5, 30, 5);
                res.ItemsSource = odgovori;
                CBList.Add(res);
                res.SelectedIndex = 0;

                stackPanel.Children.Add(CBList.Last());
                stackPanel.Children.Add(UnderTextBorder());

                Grid.SetRow(stackPanel, index / 2);
                Grid.SetColumn(stackPanel, index % 2);
                MainGrid.Children.Add(stackPanel);

                ++index;
            }*/
        }

        private void ContentTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // changes[((TextBox)sender).Name] = ((TextBox)sender).Text;
        }

        List<TextBox> ListPolinjaTxt;
        List<ComboBox> ListPolinjaCB;
        List<Pole> polinja = new List<Pole>();
        private void Refresh()
        {
            ListPolinjaTxt = new List<TextBox>();
            ListPolinjaCB = new List<ComboBox>();
            string[] PApredmeti = { "Ги немате пополнето Проектните Активности" };
            string[] Smerovi = { "Ги немате пополнето Смеровите" };
            if (UserKlas._p._smerovi.Keys.Contains("ПА")) PApredmeti = UserKlas._p._smerovi["ПА"]._predmeti.ToArray();
            if (UserKlas._p._smerovi.Count > 0)
            {
                var x = UserKlas._p._smerovi.Keys.ToList();
                x.Remove("ПА");
                Smerovi = x.ToArray();
            }
            
            List<string> Saved = new List<string>();
            for(int i=0;i<19;i++)
            { Saved.Add(""); }

            if(Ucenici.Count > 0)
            {
                Saved[0]= Ucenici[BrojDn]._ime;
                Saved[1] = Ucenici[BrojDn]._prezime;
                Saved[2] = Ucenici[BrojDn]._tatko;
                Saved[3] = Ucenici[BrojDn]._smer;
                Saved[4] = Ucenici[BrojDn]._tatko;
                Saved[5] = Ucenici[BrojDn]._majka;
                Saved[6] = Ucenici[BrojDn]._broj.ToString();
                Saved[7] = Ucenici[BrojDn]._drzavjanstvo;
                Saved[8] = Ucenici[BrojDn]._gender;
                Saved[9] = Ucenici[BrojDn]._roden;
                Saved[10] = Ucenici[BrojDn]._mesto_na_ragjanje;
                Saved[11] = Ucenici[BrojDn]._mesto_na_zhiveenje;
                Saved[12] = Ucenici[BrojDn]._pat_polaga;
                Saved[13] = Ucenici[BrojDn]._polozhil;
                Saved[14] = Ucenici[BrojDn]._opravdani.ToString();
                Saved[15] = Ucenici[BrojDn]._neopravdani.ToString();
                Saved[16] = Ucenici[BrojDn]._povedenie;
                Saved[17] = Ucenici[BrojDn]._proektni.Split(';')[0]; // " , ;"
                Saved[18] = Ucenici[BrojDn]._proektni.Split(';')[1];
            }

            polinja.Add(new Pole("Име", RequestParameters.ime,  new string[] { "Име" } , Saved[0]) );
            polinja.Add(new Pole("Презиме", RequestParameters.ime, new string[] { "Презиме" }, Saved[1]));
            polinja.Add(new Pole("Татково име", RequestParameters.srednoIme, new string[] { "Татково име" }, Saved[2]));
            polinja.Add(new Pole("Смер", RequestParameters.smer, Smerovi, Saved[3]));
            polinja.Add(new Pole("Родител(Татко)", RequestParameters.majka, new string[] { "Име Презиме" }, Saved[4]));
            polinja.Add(new Pole("Родител(Мајка)", RequestParameters.tatko, new string[] { "Име Презиме" }, Saved[5]));
            polinja.Add(new Pole("број во дневник", RequestParameters.broj, new string[] { "00" }, Saved[6]));
            polinja.Add(new Pole("Државјанство", RequestParameters.drzavjanstvo, new string[] { "Македонско" }, Saved[7]));
            polinja.Add(new Pole("Пол", RequestParameters.gender, new string[] { "Машки" , "Женски" }, Saved[8]));
            polinja.Add(new Pole("ден на раѓање", RequestParameters.roden, new string[] { "00.00.0000" }, Saved[9]));
            polinja.Add(new Pole("место на раѓање", RequestParameters.mesto_na_ragjanje, new string[] { "Скопје" }, Saved[10]));
            polinja.Add(new Pole("место на живеење", RequestParameters.mesto_na_zhiveenje, new string[] { "Скопје" }, Saved[11]));
            polinja.Add(new Pole("по кој пат ја учи годината", RequestParameters.pat_polaga, new string[] { "прв пат" , "втор пат" , "трет пат" }, Saved[12]));
            polinja.Add(new Pole("дали е положена годината", RequestParameters.polozhil, new string[] { "Положил" , "Не Положил" }, Saved[13]));
            polinja.Add(new Pole("број на оправдани изостаноци", RequestParameters.opravdani, new string[] { "0" }, Saved[14]));
            polinja.Add(new Pole("број на неоправдани изостаноци", RequestParameters.neopravdani, new string[] { "0" }, Saved[15]));
            polinja.Add(new Pole("Поведение", RequestParameters.povedenie, new string[] { "Примeрно" , "Добро" , "Незадоволно" }, Saved[16]));
            polinja.Add(new Pole("Проектна Активност 1", RequestParameters.povedenie, PApredmeti, Saved[17]));
            polinja.Add(new Pole("Проектна Активност 2", RequestParameters.povedenie, PApredmeti, Saved[18]));

            int index = 0;
            foreach (Pole Pole in polinja)
            {
                StackPanel stackPanel = new StackPanel();
                if (index % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100.0);
                    MainGrid.Height += 100.0;
                }

                StackPanel st = new StackPanel();

                if (Pole.isCB)
                {
                    ComboBox CB = CreateComboBox(Pole.Ime , Pole.DefaultVrednost);
                    if(Pole.Odgovor == "")CB.SelectedIndex = 0;
                    else CB.SelectedValue = Pole.Odgovor;
                    ListPolinjaCB.Add(CB);
                    st.Children.Add(CB);
                    if(Pole.Ime == "Проектна Активност 1" || Pole.Ime == "Проектна Активност 2")
                    {
                        ComboBox CBPoleodg = CreateComboBox(Pole.Ime + "ODG", new string[] { "Реализирал", "Не Реализирал" });
                        CBPoleodg.SelectedIndex = 0;
                        ListPolinjaCB.Add(CBPoleodg);
                        st.Children.Add(CBPoleodg);
                    }
                }
                else
                {
                    TextBox textBox = ContentTextBox(Pole.GetOdgovor());
                    textBox.GotFocus += txtPolinjaGotFocus;
                    textBox.Tag = Pole.Ime;
                    ListPolinjaTxt.Add(textBox);
                    st.Children.Add(textBox);
                }

                stackPanel.Children.Add(ContentBorder(Pole.Ime));
                stackPanel.Children.Add(st);
                stackPanel.Children.Add(UnderTextBorder());
                Grid.SetRow(stackPanel, index / 2);
                Grid.SetColumn(stackPanel, index % 2);
                MainGrid.Children.Add(stackPanel);
                ++index;
            }
        }

        private void txtPolinjaGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if (((TextBox)sender).Text == pol.DefaultVrednost[0]) RemoveText(sender);
        }

        private void AddText(object sender, string v)
        {
            if (string.IsNullOrWhiteSpace(((System.Windows.Controls.TextBox)sender).Text))
            {
                ((System.Windows.Controls.TextBox)sender).Text = v;
            }
        }

        private void RemoveText(object sender)
        {
            ((System.Windows.Controls.TextBox)sender).Text = "";
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(40, 5, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 20, "Arial");
            return bd;
        }

        private void LeftTriangleClicked(object sender, MouseEventArgs e)
        {
            BrojDnLabel.Text = Valid(-1);
            Refresh();
        }

        private void RightTriangleClicked(object sender, MouseButtonEventArgs e)
        {
            BrojDnLabel.Text = Valid(+1);
            Refresh();
        }

        String Valid(int x)
        {
            if (BrojDn + x >= 0 && BrojDn + x < Ucenici.Count) BrojDn += x;
            return (BrojDn + 1).ToString();
        }

        private void SaveBtnClicked(object sender, MouseButtonEventArgs e)
        {
            if (BrojDn >= Ucenici.Count)
            {
                MessageBox.Show("Ученикот со таков број во дневник не постои", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Save();
        }

        private void Save()
        {
            Dictionary<string, string> tx = new Dictionary<string, string>();
            Dictionary<string, string> OrigData = new Dictionary<string, string>();
            Answer.ForEach(x => tx.Add(x.Name, x.Text));
            tx.Add("proektni", Ucenici[BrojDn].ProektniToString(CBList.ConvertAll(x => x.SelectedValue.ToString())));
            OrigData["ime"] = Ucenici[BrojDn]._ime;
            OrigData["prezime"] = Ucenici[BrojDn]._prezime;
            OrigData["srednoIme"] = Ucenici[BrojDn]._srednoIme;
            OrigData["broj"] = BrojDn.ToString();
            var z = Output(Ucenici[BrojDn].UpdateUcenikData(tx, OrigData, UserKlas._token));
            MessageBox.Show(z.message, "SvedPrint", MessageBoxButton.OK, z.img);
            // SortUcenici();
        }

        private (string message, MessageBoxImage img)  Output(string answer)
        {
            switch (answer)
            {
                case "000":
                    return ("Успешно зачувување на ученикот", MessageBoxImage.Information);
                case "123":
                    return ("Паралелката нема ученици", MessageBoxImage.Exclamation);
                case "502":
                    return ("Не постои ученикот, ве молиме креирајте го", MessageBoxImage.Error);
                case "107":
                    return ("Сервисот не е достапен, ве молиме исконтактирајте ги админите", MessageBoxImage.Error);
                default:
                    return (answer, MessageBoxImage.Exclamation);
            }
        }

        private void CreateUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
            if (!UserKlas._p._smerovi.ContainsKey(Answer[3].Text))
            {
                MessageBox.Show("Смерот не се совпаѓа", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                CreateUcenik(Answer[0].Text, Answer[2].Text, Answer[1].Text, Answer[3].Text, (BrojDn + 1).ToString());
                int bk = BrojDn;
                BrojDn = Ucenici.Count - 1;
                Save();
                BrojDn = bk;
                // SortUcenici();
            }
        }

        private void CreateUcenik(string ime, string srednoime, string prezime, string smer, string br)
        {
            if (UserKlas._p._smerovi.ContainsKey(smer) == false)
            {
                MessageBox.Show("Смерот не се совпаѓа", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            List<Ucenik> match = Ucenici.Where(x => (x._prezime == prezime) && (x._ime == ime) && (x._srednoIme == srednoime)).ToList();
            Ucenici.Add(new Ucenik(ime, srednoime, prezime, UserKlas._p._smerovi[smer], br));
            Ucenici.Last()._duplicate_ctr = match.Count();
            Ucenici.Last().CreateServerUcenik(UserKlas._token);
            MessageBox.Show("успешно креирање на нов ученик", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private void SortUcenici()
        {
            // var ordered = Ucenici.OrderBy(x => x._broj).ThenBy(x => x._duplicate_ctr);
            
            CultureInfo culture = new CultureInfo("mk-MK");
            StringComparer strcomparer = StringComparer.Create(culture, true);
            var ordered = Ucenici.OrderBy(x => x._prezime, strcomparer).ThenBy(x => x._ime, strcomparer).ThenBy(x => x._duplicate_ctr);

            Ucenici = ordered.ToList();
            Home_Page.ucenici = Ucenici;
            //updateBrojDn
            Refresh();
        }

        private void DeleteUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
            if (BrojDn >= Ucenici.Count)
            {
                MessageBox.Show("Ученикот со тој број не постои", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Ucenici[BrojDn].DeleteUcenik(UserKlas._token);
            Ucenici.RemoveAt(BrojDn);
            Refresh();
        }
    }

    public class Pole
    {
        public string Ime;
        public string RequestParametar;
        public string[] DefaultVrednost;
        public string Odgovor;
        public bool isCB = false;

        public Pole(string ime, string requestparameter , string[] defaultvrednost , string odgovor = "")
        {
            Ime = ime;
            RequestParametar = requestparameter;
            DefaultVrednost = defaultvrednost;
            Odgovor = odgovor;
            if (defaultvrednost.Length > 1) isCB = true;
        }

        public string GetOdgovor()
        {
            if (Odgovor == "") return DefaultVrednost[0];
            else return Odgovor;
        }

    }
}
