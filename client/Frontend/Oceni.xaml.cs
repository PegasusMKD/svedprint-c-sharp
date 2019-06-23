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
        private static Dictionary<string, Smer> smerovi_naslov = new Dictionary<string, Smer>();

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

            smerovi_naslov = UserKlas._p.GetSmerovi();

            if (UserKlas._paralelka.Contains("IV")) MaturskiPanel.Visibility = Visibility.Visible;

            LoadListView();

            LoadOcenkiView(0);
            Load_stranski_jazici(0);
            FillOcenki(0);


            home_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Home);
            print_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Print);
            settings_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Settings);
            hide_menu_img.MouseLeftButtonDown += new MouseButtonEventHandler(Menu_hide);

            OpravdaniTxt.TextChanged += OpravdaniTxt_TextChanged;
            NeopravdaniTxt.TextChanged += NeopravdaniTxt_TextChanged;

            PovedenieCB.ItemsSource = new string[] { "Примeрно", "Добро", "Незадоволително" };
            PedagoskiMerkiCB.ItemsSource = new string[] { "нема", "усмена опомена", "писмена опомена", "3", "4" };
            if (UserKlas._p._smerovi.Keys.Contains("Изборни Предмети")) IzborenPredmetCB.ItemsSource = UserKlas._p._smerovi["Изборни Предмети"]._predmeti;

            CanWork = false;

            SJ_1_CB.SelectionChanged += SJ_1_CB_SelectionChanged;
            SJ_2_CB.SelectionChanged += SJ_1_CB_SelectionChanged;
            IzborenPredmetCB.SelectionChanged += izborniSeletionChanged;

            LoadExtraPolinja(0);

            PovedenieCB.SelectionChanged += PovedenieCB_SelectionChanged;
            PedagoskiMerkiCB.SelectionChanged += PedagoskiMerkiCB_SelectionChanged;

            CanWork = true;

        }

        private void izborniSeletionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanWork) return;
            CanWork = false;
            string str = IzborenPredmetCB.SelectedIndex.ToString();
            Ucenici[Br].UpdateUcenik(RequestParameters.izborni, IzborenPredmetCB.SelectedIndex.ToString(), UserKlas._token);
            Ucenici[Br]._izborni = str;

            FillOcenki(Br);
        }

        private void SJ_1_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanWork) return;
            CanWork = false;    
            string str = SJ_1_CB.SelectedIndex.ToString() + ";" + SJ_2_CB.SelectedIndex.ToString();
            Ucenici[Br].UpdateUcenik(RequestParameters.jazik, str, UserKlas._token);
            Ucenici[Br]._jazik = str;

            FillOcenki(Br);
        }

        private void Load_stranski_jazici(int BrojDn)
        {
            CanWork = false;
            if (UserKlas._p._smerovi.Keys.Contains("Странски Јазици"))
            {
                if(SJ_1_CB.Items.Count==0) SJ_1_CB.ItemsSource = UserKlas._p._smerovi["Странски Јазици"]._predmeti;
                if(SJ_2_CB.Items.Count==0) SJ_2_CB.ItemsSource = UserKlas._p._smerovi["Странски Јазици"]._predmeti;
                if (Ucenici[BrojDn]._jazik != null && Ucenici[BrojDn]._jazik != "" && Ucenici[BrojDn]._jazik.Length > 2)
                {
                    string[] jazici = Ucenici[BrojDn]._jazik.Split(';');
                     SJ_1_CB.SelectedIndex = int.Parse(jazici[0]);
                     SJ_2_CB.SelectedIndex = int.Parse(jazici[1]);
                }
                else
                {
                    SJ_1_CB.SelectedIndex = 0;
                    SJ_2_CB.SelectedIndex = 1;
                }
            }
            FillOcenki(BrojDn);
            CanWork = true;

        }

        private void PedagoskiMerkiCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;
            Ucenici[Br].UpdateUcenik(RequestParameters.pedagoshki_merki , cb.SelectedValue.ToString() , UserKlas._token);
            Ucenici[Br]._pedagoski_merki = cb.SelectedValue.ToString();
        }

        private void PovedenieCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;
            Ucenici[Br].UpdateUcenik(RequestParameters.povedenie, cb.SelectedValue.ToString(), UserKlas._token);
            Ucenici[Br]._povedenie = cb.SelectedValue.ToString();
        }

        private void NeopravdaniTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(NeopravdaniTxt.Text, out int n)) return;
            Ucenici[Br]._neopravdani = int.Parse(NeopravdaniTxt.Text);
            Ucenici[Br].UpdateUcenik(RequestParameters.neopravdani, Ucenici[Br]._neopravdani.ToString(), UserKlas._token);
        }

        private void OpravdaniTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(OpravdaniTxt.Text, out int n)) return;
            Ucenici[Br]._opravdani = int.Parse(OpravdaniTxt.Text);
            Ucenici[Br].UpdateUcenik(RequestParameters.opravdani, Ucenici[Br]._opravdani.ToString() , UserKlas._token);
        }

        private void LoadListView()
        {

            int i = 0;
            foreach (var x in Ucenici)
            {
                Menu.Items.Add(MenuDP(x._ime, x._prezime, i++));
            }
            combobox_smer.ItemsSource = smerovi_naslov.Keys;

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
            OcenkiGrid.Children.Clear();
            if (Ucenici[BrojDn]._smer == "")
            {
                MessageBox.Show("ученикот нема одберено смер");
                CanWork = true;
                return;
            }

            List<string> predmeti = UserKlas._p._smerovi[Ucenici[BrojDn]._smer]._predmeti;
            //combobox_smer.SelectedIndex = 0;
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

        int Br;
        private void FillOcenki(int brojDn)
        {
            CanWork = false;

            Ucenik SelectedUcenik = Ucenici[brojDn];

            //fill Menu
            Ucenik_Name.Content = SelectedUcenik._ime + " " + SelectedUcenik._prezime;
            Prosek_out.Content = SelectedUcenik.prosek();
            BrojDn_label.Content = (brojDn + 1).ToString();
            combobox_smer.SelectedValue = smerovi_naslov[SelectedUcenik._smer]._smer;

            //fill OcenkiView
            List<string> predmeti = UserKlas._p._smerovi[Ucenici[brojDn]._smer].GetCeliPredmeti(Ucenici[brojDn]._jazik , Ucenici[brojDn]._izborni , UserKlas._p._smerovi);
            for (int i = 0; i < predmeti.Count; i++)
            {
                if (i < SelectedUcenik._oceni.Count) Ocenkibox[i].Text = SelectedUcenik._oceni[i].ToString();//5 5 5 5 5 5 5 5
                else Ocenkibox[i].Text = "0";
                Predmetibox[i].Content = predmeti[i];
            }

            if (Ocenkibox.Count > 0)
            {
                Ocenkibox[0].SelectAll();
                Ocenkibox[0].Focus();
            }


            OpravdaniTxt.Text = SelectedUcenik._opravdani.ToString();
            NeopravdaniTxt.Text = SelectedUcenik._neopravdani.ToString();

            LoadProektnaAktivnost();
            LoadExtraPolinja(brojDn);

            CanWork = true;
        }

        private void LoadExtraPolinja(int br)
        {
            CanWork = false;    
            if(Ucenici[br]._izborni != null && Ucenici[br]._izborni != "") IzborenPredmetCB.SelectedIndex = int.Parse(Ucenici[br]._izborni);
            PovedenieCB.SelectedValue = Ucenici[br]._povedenie;
            PedagoskiMerkiCB.SelectedValue = Ucenici[br]._pedagoski_merki;
            if (PovedenieCB.SelectedIndex == -1) PovedenieCB.SelectedIndex = 0;
            if (PedagoskiMerkiCB.SelectedIndex == -1) PedagoskiMerkiCB.SelectedIndex = 0;
            if (IzborenPredmetCB.SelectedIndex == -1) IzborenPredmetCB.SelectedIndex = 0;
            CanWork = true;
        }

        int Proektnictr = 2;
        private void LoadProektnaAktivnost()
        {
            st1.Children.Clear();
            st2.Children.Clear();

            for (int i = 0; i < Proektnictr; i++)
            {
                if(i % 2 ==0)
                {
                    st1.Children.Add(ProektnaPanel(i));
                }
                else
                {
                    st2.Children.Add(ProektnaPanel(i));
                }
            }
        }

        List<CheckBox> realiziranoProektni = new List<CheckBox>();
        private StackPanel ProektnaPanel (int i)
        {
            string[] PreValue = null;
            if (Ucenici[Br]._proektni != null &&  Ucenici[Br]._proektni.Length > 0) PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',');

            StackPanel MainST = new StackPanel();

            ComboBox CB = new ComboBox();
            CB.Background = null;
            CB.Margin = new Thickness(28, 10, 28, 0);
            CB.FontSize = 24;
            CB.VerticalAlignment = VerticalAlignment.Top;
            CB.Height = 38;
            CB.Style = (Style)FindResource("ComboBoxStyle2");
            CB.Tag = i.ToString();
            if (UserKlas._p._smerovi.ContainsKey("ПА"))
            {
                CB.ItemsSource = UserKlas._p._smerovi["ПА"]._predmeti;
                if (PreValue == null)
                {
                    CB.SelectedIndex = 0;
                    string str ="";
                    for(int x=0;x<Proektnictr;x++)
                    {
                        str += CB.SelectedValue + ",Реализирал;";
                    }
                    Ucenici[Br]._proektni = str;
                    PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',');
                }
                else CB.SelectedValue = PreValue[0];
            }
            CB.SelectionChanged += CB_SelectionChanged;
            MainST.Children.Add(CB);

            StackPanel checkST = new StackPanel();
            checkST.Orientation = Orientation.Horizontal;
            checkST.HorizontalAlignment = HorizontalAlignment.Center;

            Label lb = SettingsDesign.CreateLabel("Реализирал", 22, "Segoe UI");
            lb.FontWeight = FontWeights.Normal;
            lb.HorizontalAlignment = HorizontalAlignment.Left;
            lb.VerticalAlignment = VerticalAlignment.Center;
            checkST.Children.Add(lb);

            CheckBox Check = new CheckBox();
            Check.HorizontalAlignment = HorizontalAlignment.Right;
            Check.VerticalAlignment = VerticalAlignment.Top;
            if(PreValue[1] == "Реализирал")Check.IsChecked = true;
            Check.RenderTransform = new ScaleTransform(2.5, 2.5);
            realiziranoProektni.Add(Check);
            Check.Tag = i.ToString();
            Check.Checked += Check_Checked;
            Check.Unchecked += Check_Unchecked;
            checkST.Children.Add(Check);

            MainST.Children.Add(checkST);
            return MainST;

        }

        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(((CheckBox)sender).Tag.ToString());
            string PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',')[0];
            Ucenici[Br].UpdateProektni(i, PreValue, false, UserKlas._token);
        }

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(((CheckBox)sender).Tag.ToString());
            string PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',')[0];
            Ucenici[Br].UpdateProektni(i, PreValue, true, UserKlas._token);
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = int.Parse(((ComboBox)sender).Tag.ToString());
            Ucenici[Br].UpdateProektni(i , ((ComboBox)sender).SelectedValue.ToString() , realiziranoProektni[i].IsChecked.Value , UserKlas._token);
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
            Ucenici[br - 1].UpdateUcenikOceni(UserKlas._token);

            //Menu
            Prosek_out.Content = Ucenici[br - 1].prosek();
        }

        private void OcenkiBox_GetFocus(object sender, EventArgs e)
        {
            TextBox tx = (TextBox)sender;
            int i = int.Parse(tx.Name.Substring(1, tx.Name.Length - 1));
            Ocenkibox[i].SelectAll();
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

            Br = brojDn;

            if (ClickedMenuItem != null)//hover
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;

            OcenkiGrid.Children.Clear();
            if (Ucenici[brojDn]._smer != "")
            {
                LoadOcenkiView(brojDn);
                Load_stranski_jazici(brojDn);
                FillOcenki(brojDn);
            }
            else
            {
                MessageBox.Show("ученикот нема одберено смер");
            }
        }

        private void Combobox_Smer_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            if (!CanWork) return;

            CanWork = false;
            int br = int.Parse(BrojDn_label.Content.ToString());
            br--;
            Smer NovSmer = UserKlas._p._smerovi.Values.ToArray()[combobox_smer.SelectedIndex];

            Ucenici[br].ChangeSmer(NovSmer,UserKlas._token);

            LoadOcenkiView(br);
            Load_stranski_jazici(br);
            FillOcenki(br);
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
