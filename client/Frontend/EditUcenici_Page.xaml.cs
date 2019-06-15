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
            SortUcenici();
            Refresh();
        }

        private void ContentTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
        }

        List<TextBox> ListPolinjaTxt;
        List<ComboBox> ListPolinjaCB;
        List<Pole> polinja = new List<Pole>();
        private void Refresh()
        {
            ListPolinjaTxt = new List<TextBox>();
            ListPolinjaCB = new List<ComboBox>();
            st1.Children.Clear();
            st2.Children.Clear();
            polinja.Clear();
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
            for (int i = 0; i < 19; i++)
            { Saved.Add(""); }

            if (Ucenici.Count > 0 && Ucenici.Count > BrojDn)
            {
                Saved[0] = Ucenici[BrojDn]._ime;
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
            if (Saved[6] == "") Saved[6] = (BrojDn + 1).ToString();

            polinja.Add(new Pole("Име", RequestParameters.ime, new string[] { "Име" }, Saved[0]));
            polinja.Add(new Pole("Презиме", RequestParameters.prezime, new string[] { "Презиме" }, Saved[1]));
            polinja.Add(new Pole("Татково име", RequestParameters.srednoIme, new string[] { "Татково име" }, Saved[2]));
            polinja.Add(new Pole("Смер", RequestParameters.smer, Smerovi, Saved[3]));
            polinja.Add(new Pole("Родител(Татко)", RequestParameters.tatko, new string[] { "Име Презиме" }, Saved[4]));
            polinja.Add(new Pole("Родител(Мајка)", RequestParameters.majka, new string[] { "Име Презиме" }, Saved[5]));
            polinja.Add(new Pole("број во дневник", RequestParameters.broj, new string[] { "0" }, Saved[6]));
            polinja.Add(new Pole("Државјанство", RequestParameters.drzavjanstvo, new string[] { "Македонско" }, Saved[7]));
            polinja.Add(new Pole("Пол", RequestParameters.gender, new string[] { "Машки", "Женски" }, Saved[8]));
            polinja.Add(new Pole("ден на раѓање", RequestParameters.roden, new string[] { "00.00.0000" }, Saved[9]));
            polinja.Add(new Pole("место на раѓање", RequestParameters.mesto_na_ragjanje, new string[] { "Скопје" }, Saved[10]));
            polinja.Add(new Pole("место на живеење", RequestParameters.mesto_na_zhiveenje, new string[] { "Скопје" }, Saved[11]));
            polinja.Add(new Pole("по кој пат ја учи годината", RequestParameters.pat_polaga, new string[] { "прв пат", "втор пат", "трет пат" }, Saved[12]));
            polinja.Add(new Pole("дали е положена годината", RequestParameters.polozhil, new string[] { "Положил", "Не Положил" }, Saved[13]));
            polinja.Add(new Pole("број на оправдани изостаноци", RequestParameters.opravdani, new string[] { "0" }, Saved[14]));
            polinja.Add(new Pole("број на неоправдани изостаноци", RequestParameters.neopravdani, new string[] { "0" }, Saved[15]));
            polinja.Add(new Pole("Поведение", RequestParameters.povedenie, new string[] { "Примeрно", "Добро", "Незадоволно" }, Saved[16]));
            polinja.Add(new Pole("Проектна Активност 1", RequestParameters.proektni, PApredmeti, Saved[17]));
            polinja.Add(new Pole("Проектна Активност 2", RequestParameters.proektni, PApredmeti, Saved[18]));

            int index = 0;
            foreach (Pole Pole in polinja)
            {
                StackPanel stackPanel = new StackPanel();

                StackPanel st = new StackPanel();

                if (Pole.isCB)
                {
                    ComboBox CB = CreateComboBox(Pole.Ime, Pole.DefaultVrednost);
                    CB.SelectionChanged += CB_SelectionChanged;
                    if (Pole.Odgovor == "") CB.SelectedIndex = 0;
                    else CB.SelectedValue = Pole.Odgovor;
                    ListPolinjaCB.Add(CB);
                    st.Children.Add(CB);
                    if (Pole.Ime == "Проектна Активност 1" || Pole.Ime == "Проектна Активност 2")
                    {
                        ComboBox CBPoleodg = CreateComboBox(Pole.Ime + "ODG", new string[] { "Реализирал", "Не Реализирал" });
                        CBPoleodg.SelectedIndex = 0;
                        CBPoleodg.Tag = RequestParameters.proektni;
                        ListPolinjaCB.Add(CBPoleodg);
                        st.Children.Add(CBPoleodg);
                    }
                }
                else
                {
                    TextBox textBox = ContentTextBox(Pole.GetOdgovor());
                    textBox.GotFocus += txtPolinjaGotFocus;
                    textBox.LostFocus += txtPolinjaLostFocus;
                    textBox.TextChanged += txtPolinjaTxtChanged;
                    textBox.Tag = Pole.Ime;
                    ListPolinjaTxt.Add(textBox);
                    st.Children.Add(textBox);
                }

                stackPanel.Children.Add(ContentBorder(Pole.Ime));
                stackPanel.Children.Add(st);
                stackPanel.Children.Add(UnderTextBorder());
                /*
                Grid.SetRow(stackPanel, index / 2);
                Grid.SetColumn(stackPanel, index % 2);
                MainGrid.Children.Add(stackPanel);
                if (index % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength();
                    MainGrid.Height += stackPanel.ActualHeight;
                }
                */
                if (index % 2 == 0)
                {
                    st1.Children.Add(stackPanel);
                }
                else
                {
                    st2.Children.Add(stackPanel);
                }
                ++index;
            }
        }

        private void txtPolinjaTxtChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if (tx.Text != "") polinja.Find(x => x.Ime == tx.Tag.ToString()).Odgovor = tx.Text;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)(sender);
            polinja.Find(x => x.Ime == cb.Tag.ToString()).Odgovor = cb.SelectedValue.ToString();
        }

        private void txtPolinjaLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if (tx.Text == "") AddText(sender, pol.DefaultVrednost[0]);
            else polinja.Find(x => x.Ime == tx.Tag.ToString()).Odgovor = tx.Text;
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
            if (BrojDn + x >= 0 && BrojDn + x <= Ucenici.Count) BrojDn += x;
            return (BrojDn + 1).ToString();
        }

        private void SaveBtnClicked(object sender, MouseButtonEventArgs e)
        {
            if (BrojDn >= Ucenici.Count)
            {
                MessageBox.Show("Ученикот со таков број во дневник не постои", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Save2();
        }
        private void Save2()
        {
            Dictionary<string, string> tx = new Dictionary<string, string>();
            Dictionary<string, string> OrigData = new Dictionary<string, string>();
            List<string> proektni = new List<string>();
            foreach (Pole x in polinja)
            {
                if (x.RequestParametar != RequestParameters.proektni) tx.Add(x.RequestParametar, x.GetOdgovor());
                else
                {
                    proektni.Add(x.GetOdgovor());
                    proektni.Add(ListPolinjaCB.Find(y => y.Tag.ToString() == x.RequestParametar).SelectedValue.ToString());
                }
                if (x.RequestParametar == RequestParameters.broj)
                {
                    if (int.Parse(x.GetOdgovor()) != BrojDn + 1)
                    {

                        int i = int.Parse(x.GetOdgovor());
                        Ucenici[i - 1]._broj = BrojDn + 1;
                        Ucenici[i - 1].UpdateUcenik(RequestParameters.broj, (BrojDn + 1).ToString(), UserKlas._token);
                        Ucenici[BrojDn]._broj = i;
                    }
                }
            }
            if (proektni.Count > 0)
            {
                tx.Add(RequestParameters.proektni, ProektniToString(proektni));
            }

            OrigData["ime"] = Ucenici[BrojDn]._ime;
            OrigData["prezime"] = Ucenici[BrojDn]._prezime;
            OrigData["srednoIme"] = Ucenici[BrojDn]._srednoIme;
            OrigData["broj"] = BrojDn.ToString();
            var z = Output(Ucenici[BrojDn].UpdateUcenikData(tx, OrigData, UserKlas._token));
            MessageBox.Show(z.message, "SvedPrint", MessageBoxButton.OK, z.img);

            SortUcenici();
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

        private (string message, MessageBoxImage img) Output(string answer)
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
            if (ListPolinjaCB.Find(x => x.Tag.ToString() == "Смер").Items.Count == 0)
            {
                MessageBox.Show("Смерот не се совпаѓа", "SvedPrint", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                CreateUcenik(polinja[0].Odgovor, polinja[2].Odgovor, polinja[1].Odgovor, polinja[3].Odgovor, (BrojDn + 1).ToString());
                int bk = BrojDn;
                BrojDn = Ucenici.Count - 1;
                Save2();
                BrojDn = bk;
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
            var ordered = Ucenici.OrderBy(x => x._broj);

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

        public Pole(string ime, string requestparameter, string[] defaultvrednost, string odgovor = "")
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
