using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

        Dictionary<string,Smer> smerovi;
        List<Dictionary<string, string>> result;
        private static Dictionary<string, string> smerovi_naslov = new Dictionary<string, string>()
        {
            {"ПМА",  "Природно Математико подрачје - А" },
            {"ПМБ", "Природно Математичко Подрачје - Б" },
            {"ОХА", "Општествено Х Подрачје А" },
            {"ОХБ", "Општествено Х Подрачје Б" },
            {"ЈУА", "Јазично Уметничко Подрачје А" },
            {"ЈУБ", "Јазично Уметничко Подрачје Б" }

        };
        Dictionary<string, string> res;
        static int brPredmeti;
        public Oceni(Frame m, Page loginpage)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;
            UserKlas = Home_Page.KlasenKlasa;

            result = Home_Page.result;

            smerovi = Home_Page.smerovi;

            LoadListView(result);

            LoadOcenki(0);

            populateData(0);


            //Smer pmb = new Smer(Requests.GetData(new Dictionary<string, string>(){
            //        { RequestParameters.token, UserKlas._token},
            //        { RequestParameters.smer, "PMB"}
            //    }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), "PMA");
            //Smer oha = new Smer(Requests.GetData(new Dictionary<string, string>(){
            //        { RequestParameters.token, UserKlas._token},
            //        { RequestParameters.smer, "OHA"}
            //    }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), "PMA");
            //Smer ohb = new Smer(Requests.GetData(new Dictionary<string, string>(){
            //        { RequestParameters.token, UserKlas._token},
            //        { RequestParameters.smer, "OHB"}
            //    }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), "PMA");
            //Smer jua = new Smer(Requests.GetData(new Dictionary<string, string>(){
            //        { RequestParameters.token, UserKlas._token},
            //        { RequestParameters.smer, "JUA"}
            //    }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), "PMA");
            //Smer jub = new Smer(Requests.GetData(new Dictionary<string, string>(){
            //        { RequestParameters.token, UserKlas._token},
            //        { RequestParameters.smer, "JUB"}
            //   }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), "PMA");


            home_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Home);
            print_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Print);
            settings_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Settings);
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
            
            foreach(string val in smerovi_naslov.Values)
            {
                combobox_smer.Items.Add(val);
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

            st.MouseUp += new MouseButtonEventHandler((sender, e) => MouseUp(sender, e, brojDn));
            st.MouseEnter += new MouseEventHandler(MouseEnter);
            st.MouseLeave += new MouseEventHandler(MouseLeave); 

            return st;
        }

        object ClickedMenuItem;
        private void MenuItemMouseEnter(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
        }

        private void MenuItemMouseLeave(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            if (ClickedMenuItem != sender) st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
        }

        private void MouseUp(object sender, MouseButtonEventArgs e, int brojDn)
        {

            if (ClickedMenuItem != null)//hover
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;

            if (combobox_smer.SelectedIndex != GetSmerIndex(brojDn))
            {
                OcenkiGrid.Children.Clear();
                LoadOcenki(brojDn);
            }
            populateData(brojDn);
        }

        int GetSmerIndex(int brojDn)
        {
            string[] MozniSmerovi = { "ПМА","ПМБ", "ОХА", "ОХБ", "ЈУА", "ЈУБ" };
            int i = 0;
            foreach (string x in MozniSmerovi)
            {
                if (smerovi[result[brojDn]["smer"]]._smer == x) return i;
                i++;
            }
            return 0;
        }

        private void populateData(int brojDn)
        {
            Ucenik_Name.Content = result[brojDn]["ime"] + " " + result[brojDn]["prezime"];
            Prosek_out.Content = Array.ConvertAll(result[brojDn]["oceni"].Split(' '), x => float.Parse(x)).Average().ToString("n2");

            BrojDn_label.Content = (brojDn + 1).ToString();
            combobox_smer.SelectedValue = smerovi_naslov[result[brojDn]["smer"]];


            string[] ocenki = result[brojDn]["oceni"].Split(' ');
            //string[] predmeti = result[brojDn]["predmeti"].Split(',');
            for (int i = 0; i < brPredmeti; i++)
            {
                if (i < ocenki.Length) Ocenkibox[i].Text = ocenki[i];//5 5 5 5 5 5 5 5
                else Ocenkibox[i].Text = "";
                Predmetibox[i].Content = smerovi[result[brojDn]["smer"]]._predmeti[i];
            }
        }

        List<TextBox> Ocenkibox = new List<TextBox>();
        List<Label> Predmetibox = new List<Label>();
        private void LoadOcenki(int broj_dn)
        {
            List<string> predmeti = UserKlas._p._smerovi[result[broj_dn]["smer"]]._predmeti;
            int Size = predmeti.Count;
            brPredmeti = Size;
            int ctr = 0;
            int ImgHeight = 80;
            int TxtHeight = 50;
            OcenkiGrid.Height = 0;

            Ocenkibox.Clear();
            Predmetibox.Clear();

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
                    // OcenkiBorderBox.Add(panel);

                    OcenkiGrid.Children.Add(panel);

                    TextBox tx = new TextBox();
                    //tx.Text = ocenki[j];
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
                    tx.SelectionBrush = System.Windows.Media.Brushes.Coral;
                    tx.Background = System.Windows.Media.Brushes.Transparent;
                    tx.TextChanged += TextBox_Text_Changed;
                    tx.Name = "t" + (Ocenkibox.Count).ToString();
                    Ocenkibox.Add(tx);

                    Border panel2 = new Border();
                    Grid.SetColumn(panel2, j);
                    Grid.SetRow(panel2, i);
                    panel2.Child = tx;
                    // PredmetiBorderBox.Add(panel2);

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
                    //tx.Content = predmeti[ctr + j - 4];
                    Predmetibox.Add(tx);

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

        private void TextBox_Text_Changed(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[(i + 1) % brPredmeti].GotFocus += TextBox_GotFocus;
            Ocenkibox[(i+1) % brPredmeti].Focus();
        }

        private void TextBox_GotFocus(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[i].SelectAll();
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

        private void Back_Print(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PrintFrame(Main,loginPage);
        }

        private void Back_Settings(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main,loginPage);
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

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
    }
}
