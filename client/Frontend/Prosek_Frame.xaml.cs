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
    /// Interaktionslogik für Prosek_Frame.xaml
    /// </summary>
    public partial class Prosek_Frame : Page
    {
        public Prosek_Frame()
        {
            InitializeComponent();

            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("Македонски", "5.0");
            dic.Add("Математика", "4.2");
            dic.Add("Физика", "2.7");
            dic.Add("Географија", "4.2");
            dic.Add("Историја", "5.0");
            dic.Add("Филозофија", "1.9");
            dic.Add("Француски", "2.7");
            dic.Add("Музичко", "4.9");
            GetData(dic);

        }

        private void GetData(Dictionary<String, String> res)
        {

            int ctr = 0;
            foreach (KeyValuePair<string, string> entry in res)
            {
                StackPanel st = new StackPanel();
                st.Children.Add(ContentTextBox(entry.Key));
                st.Children.Add(TextBorderGrid(entry.Value));

                if (ctr % 2 == 0)
                {
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(st);
                }
                ctr++;
            }
        }

        private Grid TextBorderGrid(string TextBoxText)
        {
            Grid gd = new Grid();
            gd.Margin = new Thickness(0, 0, 0, 10);

            Border border = CreateBorder(10, 0, 0, 5, "#FF3D84C6");
            border.Margin = new Thickness(25, 0, 20, 0);

            Border Circle = CreateBorder(50, 0, 0, 20, "#FF3D84C6");
            Circle.Margin = new Thickness(0, -35, 5, -10);
            Circle.VerticalAlignment = VerticalAlignment.Top;
            Circle.HorizontalAlignment = HorizontalAlignment.Right;
            Circle.Width = 50;

            TextBox tx = ContentTextBox(TextBoxText);
            tx.FontSize = 25;
            tx.Margin = new Thickness(0);
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            Circle.Child = tx;

            gd.Children.Add(border);
            gd.Children.Add(Circle);

            return gd;
        }     

    }
}
