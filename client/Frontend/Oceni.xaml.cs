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
        bool CanWork = false;

        Dictionary<string,Smer> smerovi;
        List<Dictionary<string, string>> result;
        private static Dictionary<string, string> smerovi_naslov = new Dictionary<string, string>() //treba da vlece od server
        {
            {"ПМА",  "Природно Математико подрачје - А" },
            {"ПМБ", "Природно Математичко Подрачје - Б" },
            {"ОХА", "Општествено Х Подрачје А" },
            {"ОХБ", "Општествено Х Подрачје Б" },
            {"ЈУА", "Јазично Уметничко Подрачје А" },
            {"ЈУБ", "Јазично Уметничко Подрачје Б" }

        };

        List<Ucenik> Ucenici;
        static int brPredmeti;
        public Oceni(Frame m, Page loginpage)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;

            UserKlas = Home_Page.KlasenKlasa;
            result = Home_Page.result;
            smerovi = Home_Page.smerovi;
            Ucenici = Home_Page.ucenici;

            LoadListView();

            LoadOcenkiView(0);
            FillOcenki(0);

            home_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Home);
            print_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Print);
            settings_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Settings);
            hide_menu_img.MouseLeftButtonDown += new MouseButtonEventHandler(Menu_hide);

        }

        private void LoadListView()
        {

            int i = 0;
            foreach (var x in Ucenici)
            {
                Menu.Items.Add(MenuDP(x._ime, x._prezime, i++));
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

            st.MouseUp += new MouseButtonEventHandler((sender, e) => MenuItemClicked(sender, e, brojDn));
            st.MouseEnter += new MouseEventHandler(MenuItemMouseEnter);
            st.MouseLeave += new MouseEventHandler(MenuItemMouseLeave); 

            return st;
        }

        List<TextBox> Ocenkibox = new List<TextBox>();
        List<Label> Predmetibox = new List<Label>();
        private void LoadOcenkiView(int BrojDn)
        {
            List<string> predmeti = UserKlas._p._smerovi[result[BrojDn]["smer"]]._predmeti;
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
                    tx.TextChanged += OcenkiBox_Text_Changed;
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

        private void FillOcenki(int brojDn)
        {
            Ucenik SelectedUcenik = Ucenici[brojDn];

            //fill Menu
            Ucenik_Name.Content = SelectedUcenik._ime + " " + SelectedUcenik._prezime;
            Prosek_out.Content = SelectedUcenik.prosek();
            BrojDn_label.Content = (brojDn + 1).ToString();
            combobox_smer.SelectedValue = smerovi_naslov[SelectedUcenik._smer];

            //fill OcenkiView
            for (int i = 0; i < UserKlas._p._smerovi[SelectedUcenik._smer]._predmeti.Count; i++)
            {
                if (i < SelectedUcenik._oceni.Count) Ocenkibox[i].Text = SelectedUcenik._oceni[i].ToString();//5 5 5 5 5 5 5 5
                else Ocenkibox[i].Text = "0";
                Predmetibox[i].Content = UserKlas._p._smerovi[SelectedUcenik._smer]._predmeti[i];
            }

            CanWork = true;
        }

        private void OcenkiBox_Text_Changed(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;

            if (int.TryParse(tx.Text, out int n) == false || tx.Text.Length > 1)
            {
                tx.Text = "5";
            }

            if (CanWork == false) return;

            //sledna OcenkaTx Focus da dobie
            int TextBoxBr = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[(TextBoxBr + 1) % brPredmeti].GotFocus += OcenkiBox_GetFocus;
            Ocenkibox[(TextBoxBr + 1) % brPredmeti].Focus();

            //update
            int br = int.Parse(BrojDn_label.Content.ToString());
            Ucenici[br-1]._oceni = Array.ConvertAll(Ocenkibox.ToArray(), x => int.Parse(x.Text)).ToList();
            //Ucenici[br - 1].UpdateUcenikOceni(UserKlas._token , br-1);

            //Menu
            Prosek_out.Content = Ucenici[br - 1].prosek();
        }

        private void OcenkiBox_GetFocus(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[i].SelectAll();
        }

        int GetSmerIndex(int brojDn)
        {
            string[] MozniSmerovi = { "ПМА", "ПМБ", "ОХА", "ОХБ", "ЈУА", "ЈУБ" };
            int i = 0;
            foreach (string x in MozniSmerovi)
            {
                if (smerovi[result[brojDn]["smer"]]._smer == x) return i;
                i++;
            }
            return 0;
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

        private void MenuItemClicked(object sender, MouseButtonEventArgs e, int brojDn)
        {
            CanWork = false;

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
                LoadOcenkiView(brojDn);
            }
            FillOcenki(brojDn);
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

    }
}
