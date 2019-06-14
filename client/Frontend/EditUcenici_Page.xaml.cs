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
            // SortUcenici();
            GetData();
        }

        List<TextBox> Answer;
        List<ComboBox> CBList;
        private void GetData()
        {
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
            dictionary.Add("Пол", "Машко/Женско");
            dictionary.Add("ден на раѓање", "00.00.0000");
            dictionary.Add("место на раѓање", "Скопје");
            dictionary.Add("место на живеење", "Скопје");
            dictionary.Add("по кој пат ја учи годината", "прв/втор пат");
            dictionary.Add("дали е положена годината", "Положил");
            dictionary.Add("број на оправдани изостаноци", "0");
            dictionary.Add("број на неоправдани изостаноци", "0");
            dictionary.Add("Поведение", "Примарно");

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
            }
            /*
            MainGrid.Height += 150;
            StackPanel CombostackPanel = new StackPanel();
            if (index % 2 == 0)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
                MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100.0);
                MainGrid.Height += 150.0;
            }
            ComboBox CB = new ComboBox();
            CB.Tag = "proektni";
            CB.Margin = new Thickness(30, 5, 30, 5);
            if (UserKlas._p._smerovi.ContainsKey("ПА")) CB.ItemsSource = UserKlas._p._smerovi["ПА"]._predmeti;
            CBList.Add(CB);

            CombostackPanel.Children.Add(ContentBorder("Проектни Активности"));
            CombostackPanel.Children.Add(CBList.Last());

            string[] odgovori = { "Реализирал", "Не Реализирал" };
            ComboBox res = new ComboBox();
            res.Tag = "Izboren1_Odg";
            res.Margin = new Thickness(30, 5, 30, 5);
            res.ItemsSource = odgovori;
            CBList.Add(res);

            CombostackPanel.Children.Add(CBList.Last());
            CombostackPanel.Children.Add(UnderTextBorder());

            Grid.SetRow(CombostackPanel, index / 2);
            Grid.SetColumn(CombostackPanel, index % 2);
            MainGrid.Children.Add(CombostackPanel);*/


        }

        private void ContentTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            // changes[((TextBox)sender).Name] = ((TextBox)sender).Text;
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
            GetData();
        }

        private void RightTriangleClicked(object sender, MouseButtonEventArgs e)
        {
            BrojDnLabel.Text = Valid(+1);
            GetData();
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
            GetData();
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
            // SortUcenici();
            GetData();
        }

        
    }
}
