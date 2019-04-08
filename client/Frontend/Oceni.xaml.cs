using Middleware;
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

        Frame Main;
        Page loginPage;
        Klasen UserKlas;

        public Oceni(Frame m, Page loginpage)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;
            UserKlas = Home_Page.KlasenKlasa;

            List<Dictionary<string, string>> result = Requests.GetData(new Dictionary<string, string>() {
                {RequestParameters.token, UserKlas._token }
            }, RequestScopes.GetParalelka);
            //
            //MessageBox.Show(result[4]["ime"]);
            LoadListView(result);

            string[] str = { "Математика", "Македонски", "Физика", "Хемија", "Биологија", "Географија", "Физичко", "Ликовно", "Музичко", "Математика", "физка", "Математика", "географија", "Математика" };
            LoadOcenki(str, result);


            home_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Home);
            hide_menu_img.MouseLeftButtonDown += new MouseButtonEventHandler(Menu_hide);

        }

        void LoadListView(List<Dictionary<string, string>> result)
        {

            int i = 0;
            foreach (var x in result)
            {
                // Ucenik ucenik = new Ucenik(x);
                Menu.Items.Add(MenuDP(x["ime"], x["prezime"], i++));

            }

            string[] smer = { "Природно Математико подрачје - А", "Природно Математичко Подрачје - Б", "Општествено Х Подрачје А", "Општествено Х Подрачје Б" };
            for (int j = 0; j < smer.Length; j++)//combobox
            {
                combobox_smer.Items.Add(smer[j]);
            }
            combobox_smer.SelectedItem = combobox_smer.Items[0];

        }

        private DockPanel MenuDP(string Name, string Prezime, int brojDn)
        {
            DockPanel st = new DockPanel();
            Label tx = new Label();
            tx.Content = (brojDn + 1).ToString() + ". " + Name + " " + Prezime;
            st.Children.Add(tx);
            st.Height = 50;
            st.Width = 800;
            st.MaxWidth = 800;
            st.HorizontalAlignment = HorizontalAlignment.Left;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            tx.FontSize = 22;
            tx.BorderThickness = new Thickness(0);
            tx.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
            tx.Foreground = System.Windows.Media.Brushes.White;
            tx.VerticalAlignment = VerticalAlignment.Center;

            st.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => MouseLeftButtonDown(sender, e, brojDn + 1));
            st.MouseEnter += new MouseEventHandler(MouseEnter);
            st.MouseLeave += new MouseEventHandler(MouseLeave);

            return st;
        }

        object ClickedMenuItem;
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            if (ClickedMenuItem != sender) st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e, int brojDn)
        {
            Ucenik_Name.Content = "Име Презиме " + brojDn.ToString();
            BrojDn_label.Content = brojDn.ToString();
            if (ClickedMenuItem != null)
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;
        }

        List<TextBox> Ocenkibox = new List<TextBox>();
        private void LoadOcenki(String[] predmeti, List<Dictionary<string, string>> Result)
        {

            int Size = predmeti.Length;
            int ctr = 0;
            int txtCounter = 0;
            int ImgHeight = 80;
            int TxtHeight = 50;
            foreach (var x in Result)
            {
                predmeti = x["oceni"].Split(' ');

                for (int i = 0; ctr < Size; i++)
                {

                    RowDefinition rowDefImg = new RowDefinition();
                    OcenkiGrid.RowDefinitions.Add(rowDefImg);

                    int last = -2;

                    for (int j = 0; j < 4; j++)
                    {

                        if (ctr == Size)
                        {
                            last = j;
                            break;
                        }

                        OcenkiGrid.RowDefinitions[i].Height = new GridLength(ImgHeight);

                        System.Windows.Controls.Image img = new System.Windows.Controls.Image();
                        BitmapImage bm = new BitmapImage();
                        bm.BeginInit();
                        bm.UriSource = new Uri("ocenki_bk.png", UriKind.Relative);
                        bm.EndInit();
                        img.Stretch = Stretch.Uniform;
                        img.Source = bm;

                        Border panel = new Border();
                        Grid.SetColumn(panel, j);
                        Grid.SetRow(panel, i);
                        panel.Child = img;
                        panel.Margin = new Thickness(15);

                        OcenkiGrid.Children.Add(panel);

                        TextBox tx = new TextBox();

                        tx.VerticalAlignment = VerticalAlignment.Center;
                        tx.HorizontalAlignment = HorizontalAlignment.Center;
                        tx.FontSize = 23;
                        tx.TextAlignment = TextAlignment.Center;
                        tx.FontFamily = new System.Windows.Media.FontFamily("Crimson Text");
                        tx.FontWeight = FontWeights.Medium;
                        tx.BorderThickness = new Thickness(0, 0, 0, 2);
                        tx.BorderBrush = System.Windows.Media.Brushes.White;
                        tx.Width = 20;
                        tx.Foreground = System.Windows.Media.Brushes.White;
                        tx.CaretBrush = System.Windows.Media.Brushes.White;
                        tx.Background = System.Windows.Media.Brushes.Transparent;
                        tx.TextChanged += TextBox_Text_Changed;
                        tx.Name = "t" + (Ocenkibox.Count).ToString();
                        Ocenkibox.Add(tx);

                        Border panel2 = new Border();
                        Grid.SetColumn(panel2, j);
                        Grid.SetRow(panel2, i);
                        panel2.Child = tx;
                        OcenkiGrid.Children.Add(panel2);
                        ctr++;
                    }

                    i++;

                    RowDefinition rowDefTxt = new RowDefinition();
                    OcenkiGrid.RowDefinitions.Add(rowDefTxt);

                    for (int j = 0; j < 4; j++)
                    {
                        if (last == j) break;

                        OcenkiGrid.RowDefinitions[i].Height = new GridLength(TxtHeight);

                        Label tx = new Label();
                        tx.FontSize = 20;
                        tx.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
                        tx.Foreground = System.Windows.Media.Brushes.White;
                        tx.Content = predmeti[ctr + j - 4];

                        Border panel = new Border();
                        Grid.SetColumn(panel, j);
                        Grid.SetRow(panel, i);
                        panel.Child = tx;
                        panel.VerticalAlignment = VerticalAlignment.Top;
                        panel.HorizontalAlignment = HorizontalAlignment.Center;
                        OcenkiGrid.Children.Add(panel);

                    }

                    OcenkiGrid.Height = OcenkiGrid.Height + ImgHeight + TxtHeight;
                    if (last != -2) break;
                }
            }

        }
        int ctr = 0;
        private void TextBox_Text_Changed(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[++i % 14].Focus();
        }

        private void Menu_hide(object sender, MouseButtonEventArgs e)
        {
            if (MainGrid.ColumnDefinitions[0].Width == new GridLength(0))
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(83, GridUnitType.Star);
                //  hide_menu_img2.Visibility = Visibility.Hidden;
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.UriSource = new Uri("arrow-back-icon.png", UriKind.Relative);
                bm.EndInit();
                hide_menu_img.Source = bm;
                return;
            }
            MainGrid.ColumnDefinitions[0].Width = new GridLength(0);

            BitmapImage bm2 = new BitmapImage();
            bm2.BeginInit();
            bm2.UriSource = new Uri("arrow_back_reverse.png", UriKind.Relative);
            bm2.EndInit();
            hide_menu_img.Source = bm2;
            //hide_menu_img2.Visibility = Visibility.Visible;
        }

        private void Back_Home(object sender, MouseButtonEventArgs e)
        {
            Main.Content = loginPage;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
