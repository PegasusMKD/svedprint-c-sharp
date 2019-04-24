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
            BrojDnLabel.BorderThickness = new Thickness(0);
            GetData(0);
        }

        List<TextBox> Results;
        void GetData(int dn)
        {
            MainGrid.Children.Clear();
            Results = new List<TextBox>();
            String[] Fields = { "Име", "Средно Име", "Презиме", "Смер", "број во дневник", "родител(Татко или Мајка)", "роден, место на раѓање", "пат на запишување", "вид на образование, пол", "нахнаден" };
            String[] Content = { "", "", "", "", "", "", "", "", "Прв", "", "", "" };
            MainGrid.Height = 0;
            for (int i = 0; i < Fields.Length; i++)
            {
                StackPanel st = new StackPanel();
                if (i % 2 == 0)
                {
                    MainGrid.RowDefinitions.Add(new RowDefinition());
                    MainGrid.RowDefinitions[MainGrid.RowDefinitions.Count - 1].Height = new GridLength(100);
                    MainGrid.Height += 100;
                }

                Results.Add(ContentTextBox(Content[i]));
                st.Children.Add(ContentBorder(Fields[i]));
                st.Children.Add(Results[i]);
                st.Children.Add(UnderTextBorder());

                Grid.SetRow(st, i / 2);
                Grid.SetColumn(st, i % 2);
                MainGrid.Children.Add(st);
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
            GetData(BrojDn-1);
        }

        private void RightTriangleClicked(object sender, MouseButtonEventArgs e)
        {
            BrojDnLabel.Text = valid(+1);
            GetData(BrojDn -1);
        }

        String valid(int x)
        {
            BrojDn = int.Parse(BrojDnLabel.Text);
            if (BrojDn + x >= 1 && BrojDn + x <= 35) BrojDn += x;
            return BrojDn.ToString();
        }
    }
}
