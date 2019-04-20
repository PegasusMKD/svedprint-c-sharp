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
        public Smerovi_Page()
        {
            InitializeComponent();
            Dictionary<String, String> dic = new Dictionary<string, string> ();
            dic.Add("PMA", "matematika,makedonski,fizika,muzicko");
            dic.Add("PMB", "matematika,filozofija,istorija");
            dic.Add("OHA", "makedonski,fizika,fizicko,geografija");
            dic.Add("OHB", "makedonski,fizika,fizicko,geografija");

            GetData(dic);
        }

        private void GetData(Dictionary<String, String> res)
        {

            Predmeti = new List<StackPanel>();
            int ctr = 0;
            foreach (KeyValuePair<string, string> entry in res)
            {
                StackPanel st = new StackPanel();
                foreach (string s in entry.Value.Split(','))
                {
                    st.Children.Add(ContentTextBox(s));
                    st.Children.Add(TextBorderGrid());
                }

                Predmeti.Add(st);

                if (ctr % 2 == 0)
                {
                    st1.Children.Add(ContentBorder(entry.Key));
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(ContentBorder(entry.Key));
                    st2.Children.Add(st);
                }
                ctr++;
            }
        }

        private Border ContentBorder(string LabelContent)
        {

            Border bd = CreateBorder(50, 5, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 30, "Arial");
            return bd;
        }

        private Grid TextBorderGrid()
        {
            Grid gd = new Grid();
            gd.Margin = new Thickness(0, 0, 0, 10);
            gd.Children.Add(UnderTextBorder());
            Border border = CreateBorder(36, 0, 0, 6, "#FF3D84C6");
            border.Margin = new Thickness(0, -42, 35, 0);
            border.Width = 35;
            border.HorizontalAlignment = HorizontalAlignment.Right;

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("prosek_rk.png", UriKind.Relative));
            border.Child = img;

            gd.Children.Add(border);

            return gd;
        }

        private TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 0, 0);
            tx.Text = Text;
            return tx;
        }

        List<StackPanel> Predmeti;

    }
}
