using Middleware;
using System;
using System.Collections.Generic;
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
        List<Dictionary<string, string>> result;
        int BrojDn = 0;
        int Ucenici_Size;
        bool Saved = false;

        public EditUcenici_Page()
        {
            InitializeComponent();
            UserKlas = Home_Page.KlasenKlasa;
            result = Home_Page.result;

            GetData();

        }

        List<TextBox> Answer;
        Dictionary<string, string> res;
        void GetData()
        {
            Ucenici_Size = result.Count;
            Answer = new List<TextBox>();
            MainGrid.Children.Clear();
            Saved = true;


            res = new Dictionary<string, string>();
            res.Add("Име", "");
            res.Add("Средно Име", "");
            res.Add("Презиме", "");
            res.Add("Смер", "");
            res.Add("број во дневник", "");
            res.Add("родител(Татко)", "");
            res.Add("роден", "");
            res.Add("место на раѓање", "");
            res.Add("пат на запишување", "прв");
            res.Add("вид на образование", "");
            res.Add("пол", "");
            res.Add("нахнадно", "1");

            res["Име"] = result[BrojDn]["ime"];
            res["Средно Име"] = result[BrojDn]["tatkovo"];
            res["Презиме"] = result[BrojDn]["prezime"];
            res["Смер"] = result[BrojDn]["smer"];

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
                tx.LostFocus += new RoutedEventHandler((sender, e) => ContentTextBoxLostFocusEvent(sender, e, x.Key));
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

        private void ContentTextBoxLostFocusEvent(object sender, RoutedEventArgs e, string i)
        {
            Saved = false;
           /* TextBox tx = (TextBox)sender;
            Dictionary<string, string> RequestsString = new Dictionary<string, string>() { { "Име", RequestParameters.new_first_name }, { "Средно Име", RequestParameters.new_middle_name }, { "Презиме", RequestParameters.new_last_name }, { "Смер", RequestParameters.smer } }; ;
            Dictionary<string, string> resultDic = new Dictionary<string, string>() { { "Име" , "ime" } , { "Средно Име", "tatkovo" } , { "Презиме" , "prezime" } , { "Смер" , "smer" }  };
            UpdateUcenik(BrojDn-1, RequestsString[i], tx.Text);
            result[BrojDn-1][resultDic[i]] = tx.Text;*/
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
            if (Saved == false) MessageBox.Show("Ги немате сочувано новите работи за ученикот");

            if (BrojDn + x >= 0 && BrojDn + x < Ucenici_Size) BrojDn += x;
            return (BrojDn +1).ToString();
        }

        string[] Request = { RequestParameters.new_first_name, RequestParameters.new_middle_name, RequestParameters.new_last_name, RequestParameters.new_smer };
        private void SaveBtnClicked(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            foreach(TextBox tx in Answer)
            {
                UpdateUcenik(BrojDn, Request[i++] , tx.Text);
                if (i == Request.Length) break;
            }
            Saved = true;
        }

        private void CreateUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
            CreateUcenik(Answer[0].Text, Answer[1].Text, Answer[2].Text, Answer[3].Text, Answer[4].Text);
        }

        private void CreateUcenik(string ime, string srednoime, string prezime, string smer, string br)
        {
            Requests.AddData(new Dictionary<string, string>() {
                { RequestParameters.ime, ime} , {RequestParameters.srednoIme , srednoime} , { RequestParameters.prezime , prezime } , { RequestParameters.broj , br} , { RequestParameters.smer , smer}
            }, RequestScopes.AddUcenici);

            result = Requests.GetData(new Dictionary<string, string>() {
                {RequestParameters.token, UserKlas._token }
            }, RequestScopes.GetParalelka);
            Ucenici_Size = result.Count;
        }

        private void UpdateUcenik(int BrojDn, string UpdateParametar, string Value)
        {
            Requests.UpdateData(new Dictionary<string, string>() {
            { RequestParameters.token , UserKlas._token} , { RequestParameters.ime , result[BrojDn]["ime"] } , {RequestParameters.prezime , result[BrojDn]["prezime"] } , { RequestParameters.broj , BrojDn.ToString() } ,  {RequestParameters.srednoIme , result[BrojDn]["tatkovo"] }  , { UpdateParametar , Value }
            }, RequestScopes.UpdateUcenik);

        }

        private void DeleteUcenik()
        {

        }

    }
}
