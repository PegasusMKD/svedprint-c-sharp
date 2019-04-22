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

            int ctr = 0;
            DodajPredmeti.Clear();
            st1.Children.Clear();
            st2.Children.Clear();

            String[] smer = UserKlas._smerovi.Split(',');
            foreach (string x in smer)
            {
                List<String> Predmeti = UserKlas._p._smerovi[x]._predmeti;

                StackPanel st = new StackPanel();
                foreach (string s in Predmeti)
                {
                    st.Children.Add(ContentTextBox(s));
                    st.Children.Add(TextBorderGrid(true , ctr));
                }

                if (ctr % 2 == 0)
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
                st.Children.Add(DodajPredmeti[Predmeti.Count-1]);
                st.Children.Add(TextBorderGrid(false , ctr));

                ctr++;
            }
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(50, 5, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 30, "Arial");
            return bd;
        }

        private Grid TextBorderGrid(bool IsX,int i)
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
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => NewPredmetImgClicked(sender, e, i));
            }
            if(IsX == true)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => RemovePredmetImgClicked(sender, e, i));
            }
            return gd;
        }

        private void NewPredmetImgClicked(object sender, MouseButtonEventArgs e , int i)
        {
            MessageBox.Show(DodajPredmeti[i].Text);
            UserKlas._p._smerovi[UserKlas._smerovi.Split(',')[i]]._predmeti.Add(DodajPredmeti[i].Text);
            GetData();
        }

        private void RemovePredmetImgClicked(object sender, MouseButtonEventArgs e, int i)
        {
            MessageBox.Show(DodajPredmeti[i].Text);
            UserKlas._p._smerovi[UserKlas._smerovi.Split(',')[i]]._predmeti.RemoveAt(i);
            GetData();
        }

        private TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 0, 0);
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
                Requests.UpdateData(new Dictionary<string, string>() {
                 {RequestParameters.smer , x }, { RequestParameters.token , UserKlas._token }
                }, res);
            }

        }

    }
}
