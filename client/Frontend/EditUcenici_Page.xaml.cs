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
    /// Interaktionslogik für EditUcenici_Page.xaml
    /// </summary>
    public partial class EditUcenici_Page : Page
    {

        int BrojDn = 0;
        public EditUcenici_Page()
        {
            InitializeComponent();
            GetData();
        }

        List<TextBox> Answer;
        void GetData()
        {
            Answer = new List<TextBox>();
            Dictionary<string, string> res = new Dictionary<string, string>();
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

                Answer.Add(ContentTextBox(x.Value));
                st.Children.Add(ContentBorder(x.Key));
                st.Children.Add(Answer[i]);
                st.Children.Add(UnderTextBorder());

                Grid.SetRow(st, i / 2);
                Grid.SetColumn(st, i % 2);
                MainGrid.Children.Add(st);
                i++;
            }
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(40, 5 , 20,10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 20, "Arial");
            return bd;
        }

        private void LeftTriangleClicked(object sender, MouseEventArgs e)
        {
            BrojDnLabel.Text = valid(-1);
            GetData();
        }

        private void RightTriangleClicked(object sender, MouseButtonEventArgs e)
        {
            BrojDnLabel.Text = valid(+1);
            GetData();
        }

        String valid(int x)
        {
            BrojDn = int.Parse(BrojDnLabel.Text);
            if (BrojDn + x >= 1 && BrojDn + x <= 35) BrojDn += x;
            return BrojDn.ToString();
        }
    }
}
