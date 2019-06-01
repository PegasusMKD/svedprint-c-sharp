using Middleware;
using System;
using System.Collections.Generic;
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
        bool Saved = true;

        public EditUcenici_Page()
        {
            InitializeComponent();
            UserKlas = Home_Page.KlasenKlasa;
            Ucenici = Home_Page.ucenici;
            GetData();
        }

        List<TextBox> Answer;
        void GetData()
        {
            Answer = new List<TextBox>();
            MainGrid.Children.Clear();
            Saved = true;

            Dictionary<string, string> res= new Dictionary<string, string>();
            res.Add("Име", "Име");
            res.Add("Средно Име", "Средно Име");
            res.Add("Презиме", "Презиме");
            res.Add("Смер", "Смер");
            res.Add("број во дневник", "00");
            res.Add("родител(Татко)", "Име Презиме");
            res.Add("родител(Мајка)", "Име Презиме");
            res.Add("ден на раѓање", "00.00.0000");
            res.Add("место на раѓање", "Скопје");
            res.Add("Државјанство", "РСМ");
            //res.Add("пол", "");

            if (Ucenici.Count > BrojDn)
            {
                res["Име"] = Ucenici[BrojDn]._ime;
                res["Средно Име"] = Ucenici[BrojDn]._srednoIme;
                res["Презиме"] = Ucenici[BrojDn]._prezime;
                res["Смер"] = Ucenici[BrojDn]._smer;
                Ucenici[BrojDn]._broj = BrojDn;
                res["број во дневник"] = (Ucenici[BrojDn]._broj + 1).ToString();
                res["родител(Татко)"] = Ucenici[BrojDn]._tatko;
                res["родител(Мајка)"] = Ucenici[BrojDn]._majka;
                res["ден на раѓање"] = Ucenici[BrojDn]._roden;
                res["место на раѓање"] = Ucenici[BrojDn]._mesto_na_ragjanje;
                res["Државјанство"] = Ucenici[BrojDn]._drzavjanstvo;
            }
            MainGrid.Height = 0;

            int i = 0;
            foreach (KeyValuePair<string, string> x in res)
            {
                StackPanel st = new StackPanel();
                if (i % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100);
                    MainGrid.Height += 100;
                }

                TextBox tx = ContentTextBox(x.Value);
                tx.TextChanged += ContentTextBoxTextChanged;
                Answer.Add(tx);

                st.Children.Add(ContentBorder(x.Key));
                st.Children.Add(Answer[i]);
                st.Children.Add(UnderTextBorder());

                Grid.SetRow(st, i / 2);
                Grid.SetColumn(st, i % 2);
                MainGrid.Children.Add(st);
                i++;
            }
        }

        private void ContentTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            Saved = true;
        }


        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(40, 5 , 20,10, "#FFED6A3E");
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
            return (BrojDn +1).ToString();
        }

        private void SaveBtnClicked(object sender, MouseButtonEventArgs e)
        {
            List<string> tx = Answer.ConvertAll(x => x.Text);
            Ucenici[BrojDn].UpdateUcenikData(tx, UserKlas._token);
            SortUcenici();
        }

        private void CreateUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
            CreateUcenik(Answer[0].Text, Answer[1].Text, Answer[2].Text, Answer[3].Text, Answer[4].Text);
            List<string> tx = Answer.ConvertAll(x => x.Text);
            Ucenici[BrojDn].UpdateUcenikData(tx, UserKlas._token);
            SortUcenici();
        }

        private void CreateUcenik(string ime, string srednoime, string prezime, string smer, string br)
        {
            if(UserKlas._p._smerovi.ContainsKey(smer) == false)
            {
                MessageBox.Show("Смерот не се совпаѓа");
                return;
            }
            Ucenici.Add(new Ucenik(ime, srednoime, prezime, UserKlas._p._smerovi[smer], br));
            Ucenici.Last().CreateServerUcenik(UserKlas._token);
            MessageBox.Show("успешно креирање на нов ученик");
        }

        private void SortUcenici()
        {
            var ordered = Ucenici.OrderBy(x => x._prezime).ThenBy(x => x._ime).ThenBy(x => x._duplicate_ctr);
            Ucenici = ordered.ToList();
            Home_Page.ucenici = Ucenici;
            //updateBrojDn
            GetData();
        }

        private void DeleteUcenikImgClicked(object sender , MouseButtonEventArgs e)
        {
            Ucenici[BrojDn].DeleteUcenik(UserKlas._token);
            Ucenici.RemoveAt(BrojDn);
            SortUcenici();
            GetData();
        }

    }
}
