using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Frontend.SettingsDesign;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Smerovi_Page.xaml
    /// </summary>
    public partial class Smerovi_Page : Page
    {

        Klasen UserKlas;
        Dictionary<string, Smer> smerovi;
        List<Dictionary<string, string>> result;


        public Smerovi_Page()
        {
            InitializeComponent();

            UserKlas = Home_Page.KlasenKlasa;
            smerovi = Home_Page.smerovi;
            result = Home_Page.result;

            GetData();
        }

        List<TextBox> DodajPredmeti = new List<TextBox>();

        private void GetData()
        {

            DodajPredmeti.Clear();
            st1.Children.Clear();
            st2.Children.Clear();
            int SmerCtr = 0;

            String[] smer = UserKlas._smerovi.Split(',');
            foreach (string x in smer)
            {
                List<String> Predmeti = UserKlas._p._smerovi[x]._predmeti;

                StackPanel st = new StackPanel();
                int PredmetCtr = 0;
                foreach (string s in Predmeti)
                {
                    st.Children.Add(ContentTextBox(s));
                    st.Children.Add(TextBorderGrid(true ,SmerCtr, PredmetCtr++));
                }

                if (SmerCtr % 2 == 0)
                {
                    st1.Children.Add(ContentBorder(x));
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(ContentBorder(x));
                    st2.Children.Add(st);
                }
                DodajPredmeti.Add(ContentTextBox("Додавај Предмет"));
                st.Children.Add(DodajPredmeti[DodajPredmeti.Count-1]);
                st.Children.Add(TextBorderGrid(false , SmerCtr , DodajPredmeti.Count - 1));

                SmerCtr++;
            }
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(50, 5, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 30, "Arial");
            return bd;
        }

        private Grid TextBorderGrid(bool IsX,int i , int j)
        {
            Grid gd = new Grid();
            gd.Margin = new Thickness(0, 0, 0, 10);
            gd.Children.Add(UnderTextBorder());
            Border border = CreateBorder(36, 0, 0, 6, "#FF3D84C6");
            border.Margin = new Thickness(0, -42, 35, 0);
            border.Width = 35;
            border.HorizontalAlignment = HorizontalAlignment.Right;

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("check_icon.png", UriKind.Relative));
            border.Child = img;

            gd.Children.Add(border);
            if(IsX == false)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => NewPredmetImgClicked(sender, e, i, j));
            }
            if(IsX == true)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => RemovePredmetImgClicked(sender, e, i , j));
                img.MouseEnter += RemovePredmedimgMouseEnter;
                img.MouseLeave += RemovePredmedimgMouseLeave;
            }
            return gd;
        }

        private void NewPredmetImgClicked(object sender, MouseButtonEventArgs e , int i , int j)
        {
            UserKlas._p._smerovi[UserKlas._smerovi.Split(',')[i]]._predmeti.Add(DodajPredmeti[j].Text);
            GetData();
        }

        private void RemovePredmetImgClicked(object sender, MouseButtonEventArgs e, int i , int j)
        {
            UserKlas._p._smerovi[UserKlas._smerovi.Split(',')[i]]._predmeti.RemoveAt(j);
            GetData();
        }

        private void RemovePredmedimgMouseEnter(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri("x_2.png", UriKind.Relative));
        }

        private void RemovePredmedimgMouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri("check_icon.png", UriKind.Relative));
        }

        private TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 70, 0);
            tx.Text = Text;
            return tx;
        }

        private void Update()
        {
            foreach(string x in UserKlas._smerovi.Split(','))
            {
                string res = "";
                foreach(string s in UserKlas._p._smerovi[x]._predmeti)
                {
                    res += s + ",";
                }
                res = res.Substring(0, res.Length - 1);
                Requests.UpdateData(new Dictionary<string, string>() {
                 {RequestParameters.smer , x }, { RequestParameters.token , UserKlas._token }
                }, res);
            }
        }

    }
}
