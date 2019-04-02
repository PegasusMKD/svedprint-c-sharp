using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Oceni.xaml
    /// </summary>
    public partial class Oceni : Page
    {
        public Oceni()
        {
            InitializeComponent();
            LoadListView();

            string[] str = { "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика", "Математика" };
            LoadOcenki(str);
        }

        void LoadListView()
        {

            for(int i=0;i<34;i++)
            {
               
                Menu.Items.Add(MenuDP("Име" , "Презиме", i));
            }
           

        }

        private DockPanel MenuDP (string Name , string Prezime , int brojDn)
        {
            DockPanel st = new DockPanel();
            Label tx = new Label();
            tx.Content = (brojDn + 1).ToString() + ". " + Name + " " + Prezime;
            st.Children.Add(tx);
            st.Height = 50;
            st.Width = 400;
            st.MaxWidth = 400;
            st.HorizontalAlignment = HorizontalAlignment.Left;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            tx.FontSize = 22;
            tx.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
            tx.Foreground = System.Windows.Media.Brushes.White;
            tx.VerticalAlignment = VerticalAlignment.Center;

            st.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => MouseLeftButtonDown(sender, e, brojDn + 1));
            st.MouseEnter += new MouseEventHandler(MouseEnter);
            st.MouseLeave += new MouseEventHandler(MouseLeave);

            return st;
        }


        private void LoadOcenki(String[] predmeti)
        {

            int Size = predmeti.Length;
            int ctr = 0;
            int ImgHeight = 150;
            int TxtHeight = 75;

            for (int i=0;i<Size*2;i++)
            {

                RowDefinition rowDefImg = new RowDefinition();
                OcenkiGrid.RowDefinitions.Add(rowDefImg);

                int last = -2;

                for (int j=0;j<4;j++)
                {

                    if (ctr == Size - 1)
                    {
                        last = j ;
                        break ;
                    }

                    OcenkiGrid.RowDefinitions[i].Height = new GridLength(ImgHeight);
                 
                    System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                    BitmapImage bm = new BitmapImage();
                    bm.BeginInit();
                    bm.UriSource = new Uri("print.png", UriKind.Relative);
                    bm.EndInit();
                    img.Stretch = Stretch.Uniform;
                    img.Source = bm;

                    Border panel = new Border();
                    Grid.SetColumn(panel, j);
                    Grid.SetRow(panel, i);
                    panel.Child = img;
                    panel.Margin = new Thickness(15);

                    OcenkiGrid.Children.Add(panel);
                    ctr++;
                }
          
                i++;


                RowDefinition rowDefTxt = new RowDefinition();
                OcenkiGrid.RowDefinitions.Add(rowDefTxt);

                for (int j=0;j<4;j++)
                {
                    if (last == j) break;

                    Label tx = new Label();
                    tx.FontSize = 15;
                    tx.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
                    tx.Foreground = System.Windows.Media.Brushes.White;
                    tx.VerticalAlignment = VerticalAlignment.Center;
                    tx.HorizontalAlignment = HorizontalAlignment.Center;
                    tx.Content = predmeti[ctr];

                    OcenkiGrid.RowDefinitions[i].Height = new GridLength(TxtHeight);
                    Border panel = new Border();
                    Grid.SetColumn(panel, j);
                    Grid.SetRow(panel, i);
                    panel.Child = tx;
                    panel.Margin = new Thickness(15);

                    OcenkiGrid.Children.Add(panel);

                }

                OcenkiGrid.Height = OcenkiGrid.Height + ImgHeight + TxtHeight;
                if (last != -2) break;
            }


        }

        private void MouseEnter(object sender,MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
        }

        private void MouseLeave(object sender,MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e , int brojDn)
        {
            Ucenik_Name.Content = "Име Презиме " + brojDn.ToString();
        }

    }
}
