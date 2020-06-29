using Middleware;
using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Page homePage;
        private User currentUser;
        Klasen UserKlas;
        bool CanWork = false;

        Dictionary<string, Smer> smerovi;
        List<Dictionary<string, string>> result;
        private static Dictionary<string, Smer> smerovi_naslov = new Dictionary<string, Smer>();

        List<Ucenik> Ucenici;
        static int brPredmeti;
        private Student currentStudent;
        private bool haveGradesChanged;
        public Oceni(User u)
        {
            InitializeComponent();
            currentUser = u;
            currentStudent = currentUser.schoolClass.students[0];

            LoadListView();
            home_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Home);

            LoadOcenkiView(0);
            FillOcenki(0);

            haveGradesChanged = false;
            OcenkiGrid.MouseLeave += new MouseEventHandler(async (object o, MouseEventArgs e) =>
            {
                if (!haveGradesChanged) return;
                try
                {
                    await MiddlewareRevisited.Controllers.Student.updateStudent(currentStudent, currentUser);
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.Message);
                } finally
                {
                    haveGradesChanged = false;
                }
            });

            CanWork = true;

            return;

            Load_stranski_jazici(0);


            print_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Print);
            settings_img.MouseLeftButtonDown += new MouseButtonEventHandler(Back_Settings);
            hide_menu_img.MouseLeftButtonDown += new MouseButtonEventHandler(Menu_hide);

            OpravdaniTxt.TextChanged += OpravdaniTxt_TextChanged;
            NeopravdaniTxt.TextChanged += NeopravdaniTxt_TextChanged;

            PovedenieCB.ItemsSource = new string[] { "Примeрно", "Добро", "Незадоволително" };
            PedagoskiMerkiCB.ItemsSource = new string[] { "нема", "Усмена Опомена", "Писмена Опомена" };
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

        private async void izborniSeletionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanWork) return;
            CanWork = false;
            string str = IzborenPredmetCB.SelectedIndex.ToString();
            await Ucenici[Br].UpdateUcenik(RequestParameters.izborni, IzborenPredmetCB.SelectedIndex.ToString(), UserKlas._token);
            Ucenici[Br]._izborni = str;

            FillOcenki(Br);
        }

        private async void SJ_1_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanWork) return;
            CanWork = false;
            string str = SJ_1_CB.SelectedIndex.ToString() + ";" + SJ_2_CB.SelectedIndex.ToString();
            await Ucenici[Br].UpdateUcenik(RequestParameters.jazik, str, UserKlas._token);
            Ucenici[Br]._jazik = str;

            FillOcenki(Br);
        }

        private void Load_stranski_jazici(int BrojDn)
        {
            CanWork = false;
            if (UserKlas._p._smerovi.Keys.Contains("Странски Јазици"))
            {
                if (SJ_1_CB.Items.Count == 0) SJ_1_CB.ItemsSource = UserKlas._p._smerovi["Странски Јазици"]._predmeti;
                if (SJ_2_CB.Items.Count == 0) SJ_2_CB.ItemsSource = UserKlas._p._smerovi["Странски Јазици"]._predmeti;
                if (!string.IsNullOrWhiteSpace(Ucenici[BrojDn]._jazik) && Ucenici[BrojDn]._jazik.Length > 2)
                {
                    string[] jazici = Ucenici[BrojDn]._jazik.Split(new char[] { ';', ':' }); // hardcoded
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

        private async void PedagoskiMerkiCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;
            await Ucenici[Br].UpdateUcenik(RequestParameters.pedagoshki_merki, cb.SelectedValue.ToString(), UserKlas._token);
            Ucenici[Br]._pedagoski_merki = cb.SelectedValue.ToString();
        }

        private async void PovedenieCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1) return;
            await Ucenici[Br].UpdateUcenik(RequestParameters.povedenie, cb.SelectedValue.ToString(), UserKlas._token);
            Ucenici[Br]._povedenie = cb.SelectedValue.ToString();
        }

        private async void NeopravdaniTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(NeopravdaniTxt.Text, out int n)) return;
            Ucenici[Br]._neopravdani = int.Parse(NeopravdaniTxt.Text);
            await Ucenici[Br].UpdateUcenik(RequestParameters.neopravdani, Ucenici[Br]._neopravdani.ToString(), UserKlas._token);
        }

        private async void OpravdaniTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(OpravdaniTxt.Text, out _)) return;
            Ucenici[Br]._opravdani = int.Parse(OpravdaniTxt.Text);
            await Ucenici[Br].UpdateUcenik(RequestParameters.opravdani, Ucenici[Br]._opravdani.ToString(), UserKlas._token);
        }

        private void LoadListView()
        {

            int i = 0;
            foreach (var x in currentUser.schoolClass.students)
            {
                Menu.Items.Add(MenuDP(x.firstName, x.lastName, i++));
            }
            combobox_smer.ItemsSource = currentUser.schoolClass.subjectOrientations.Select(x => x.fullName);

        }

        private DockPanel MenuDP(string Name, string Prezime, int brojDn)
        {
            DockPanel st = new DockPanel();
            Label tx = new Label { Content = (brojDn + 1).ToString() + ". " + Name + " " + Prezime };
            st.Children.Add(tx);
            st.Height = 50;
            st.Width = 800;
            st.MaxWidth = 800;
            st.HorizontalAlignment = HorizontalAlignment.Left;
            st.Background = new SolidColorBrush(Color.FromRgb(165, 166, 140));
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
            if (currentUser.schoolClass.students[BrojDn].subjectOrientation == null)
            //if (Ucenici[BrojDn]._smer == "")
            {
                MessageBox.Show("ученикот нема одберено смер");
                CanWork = true;
                return;
            }
            List<string> subjects = currentUser.schoolClass.students[BrojDn].subjectOrientation.subjects;
            //List<string> predmeti = UserKlas._p._smerovi[Ucenici[BrojDn]._smer]._predmeti;
            //combobox_smer.SelectedIndex = 0;
            int Size = subjects.Count;
            brPredmeti = Size;
            int ctr = 0;
            int ImgHeight = 80;
            int TxtHeight = 50;
            OcenkiGrid.Height = 0;

            Ocenkibox.Clear();
            Predmetibox.Clear();

            // TODO: rewrite needed
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

                    Image img = new Image();
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

                    TextBox tx = new TextBox
                    {
                        //tx.Text = ocenki[j];
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 23,
                        TextAlignment = TextAlignment.Center,
                        FontFamily = new FontFamily("Crimson Text"),
                        FontWeight = FontWeights.Medium,
                        BorderThickness = new Thickness(0, 0, 0, 2),
                        BorderBrush = Brushes.White,
                        Width = 20,
                        Foreground = Brushes.White,
                        CaretBrush = Brushes.White,
                        SelectionBrush = Brushes.Coral,
                        Background = Brushes.Transparent
                    };
                    tx.TextChanged += OcenkiBox_Text_Changed;
                    tx.Name = "t" + Ocenkibox.Count.ToString();
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

                    Label tx = new Label
                    {
                        FontSize = 20,
                        FontFamily = new System.Windows.Media.FontFamily("Arial Black"),
                        Foreground = System.Windows.Media.Brushes.White
                    };
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

            //Ucenik SelectedUcenik = Ucenici[brojDn];
            //Student currentStudent = currentSchoolClass.students[brojDn];

            //fill Menu
            //Ucenik_Name.Content = SelectedUcenik._ime + " " + SelectedUcenik._prezime;
            Ucenik_Name.Content = currentStudent.firstName + " " + currentStudent.lastName;
            //Prosek_out.Content = SelectedUcenik.prosek();
            Prosek_out.Content = currentStudent.grades.DefaultIfEmpty(0).Average().ToString("F2");
            BrojDn_label.Content = (brojDn + 1).ToString();
            //combobox_smer.SelectedValue = smerovi_naslov[SelectedUcenik._smer]._smer;
            combobox_smer.SelectedValue = currentStudent.subjectOrientation.fullName;

            //fill OcenkiView
            //List<string> predmeti = UserKlas._p._smerovi[Ucenici[brojDn]._smer].GetCeliPredmeti(Ucenici[brojDn]._jazik, Ucenici[brojDn]._izborni, UserKlas._p._smerovi);
            List<string> subjects = currentStudent.subjectOrientation.subjects;
            int ctr = 0;
            for (int i = 0; i < subjects.Count; i++)
            {
                if (i < currentStudent.grades.Count) Ocenkibox[i].Text = currentStudent.grades[i].ToString();//5 5 5 5 5 5 5 5
                else Ocenkibox[i].Text = "0";
                if (!subjects[i].Contains("СЈ")) Predmetibox[i].Content = subjects[i];
                else
                {
                    // Predmetibox[i].Content = UserKlas._p._smerovi["Странски Јазици"]._predmeti[int.Parse(SelectedUcenik._jazik.Split(';')[ctr])];
                    ctr++;
                }
            }

            if (Ocenkibox.Count > 0)
            {
                Ocenkibox[0].SelectAll();
                Ocenkibox[0].Focus();
            }


            OpravdaniTxt.Text = currentStudent.justifiedAbsences.ToString();
            NeopravdaniTxt.Text = currentStudent.unjustifiedAbsences.ToString();

            //LoadProektnaAktivnost();
            //LoadExtraPolinja(brojDn);
            //LoadMaturskiPanel();

            CanWork = true;
        }

        private void LoadExtraPolinja(int br)
        {
            CanWork = false;
            if (Ucenici[br]._izborni != null && Ucenici[br]._izborni != "") IzborenPredmetCB.SelectedIndex = int.Parse(Ucenici[br]._izborni);
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
                if (i % 2 == 0)
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
        private StackPanel ProektnaPanel(int i)
        {
            string[] PreValue = null;
            if (Ucenici[Br]._proektni != null && Ucenici[Br]._proektni.Length > 0) PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',');

            StackPanel MainST = new StackPanel();

            ComboBox CB = new ComboBox();
            CB.Background = null;
            CB.Margin = new Thickness(28, 10, 28, 0);
            CB.FontSize = 24;
            CB.VerticalAlignment = VerticalAlignment.Top;
            CB.Height = 38;
            // CB.Style = (Style)FindResource("ComboBoxStyle2");
            CB.Tag = i.ToString();
            if (UserKlas._p._smerovi.ContainsKey("ПА"))
            {
                CB.ItemsSource = UserKlas._p._smerovi["ПА"]._predmeti;
                if (PreValue == null)
                {
                    CB.SelectedIndex = 0;
                    string str = "";
                    for (int x = 0; x < Proektnictr; x++)
                    {
                        str += CB.SelectedValue + ",реализирал;";
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

            Label lb = SettingsDesign.CreateLabel("реализирал", 22, "Segoe UI");
            lb.FontWeight = FontWeights.Normal;
            lb.HorizontalAlignment = HorizontalAlignment.Left;
            lb.VerticalAlignment = VerticalAlignment.Center;
            checkST.Children.Add(lb);

            CheckBox Check = new CheckBox();
            Check.HorizontalAlignment = HorizontalAlignment.Right;
            Check.VerticalAlignment = VerticalAlignment.Top;
            if (PreValue[1].ToLower() == "реализирал") Check.IsChecked = true;
            Check.RenderTransform = new ScaleTransform(2.5, 2.5);
            realiziranoProektni.Add(Check);
            Check.Tag = i.ToString();
            Check.Checked += Check_Checked;
            Check.Unchecked += Check_Unchecked;
            checkST.Children.Add(Check);

            MainST.Children.Add(checkST);
            return MainST;

        }

        private async void Check_Unchecked(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(((CheckBox)sender).Tag.ToString());
            string PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',')[0];
            await Ucenici[Br].UpdateProektniAsync(i, PreValue, false, UserKlas._token);
        }

        private async void Check_Checked(object sender, RoutedEventArgs e)
        {
            int i = int.Parse(((CheckBox)sender).Tag.ToString());
            string PreValue = Ucenici[Br]._proektni.Split(';')[i].Split(',')[0];
            await Ucenici[Br].UpdateProektniAsync(i, PreValue, true, UserKlas._token);
        }

        private async void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = int.Parse(((ComboBox)sender).Tag.ToString());
            await Ucenici[Br].UpdateProektniAsync(i, ((ComboBox)sender).SelectedValue.ToString(), realiziranoProektni[i].IsChecked.Value, UserKlas._token);
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
            haveGradesChanged = true;
            //if (TextBoxBr >= currentStudent.grades.Count)
            //{
            //    currentStudent.grades.AddRange(Enumerable.Repeat(0, TextBoxBr + 1 - currentStudent
            //        .grades.Count));    
            //}
            currentStudent.grades[TextBoxBr] = int.Parse(tx.Text); 

            //Menu
            Prosek_out.Content = currentStudent.grades.DefaultIfEmpty(0).Average();
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
            currentStudent = currentUser.schoolClass.students[Br];
            haveGradesChanged = false;

            if (ClickedMenuItem != null)//hover
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;

            OcenkiGrid.Children.Clear();
            //if (Ucenici[brojDn]._smer != "")
            if (currentUser.schoolClass.students[brojDn].subjectOrientation != null)
            {
                LoadOcenkiView(brojDn);
                //Load_stranski_jazici(brojDn);
                FillOcenki(brojDn);
            }
            else
            {
                MessageBox.Show("ученикот нема одберено смер");
            }
        }

        private async void Combobox_Smer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CanWork) return;

            CanWork = false;

            try
            {
                currentStudent.subjectOrientation = currentUser.schoolClass.subjectOrientations.Find(x => x.fullName == (string)combobox_smer.SelectedItem);
                await MiddlewareRevisited.Controllers.Student.updateStudent(currentStudent, currentUser);
            } catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }


            int br = int.Parse(BrojDn_label.Content.ToString());
            br--;

            // var x = UserKlas._p.GetSmerovi().Values.ToList();
            // Smer NovSmer = x[combobox_smer.SelectedIndex];
            // await Ucenici[br].ChangeSmerAsync(NovSmer, UserKlas._token);

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
            NavigationService.GoBack();
            // ((Frame)this.Parent).GoBack();
            // Main.GoBack();
            // Main.Content = homePage;
        }

        private void Back_Print(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PrintFrame(Main, homePage);
        }

        private void Back_Settings(object sender, MouseButtonEventArgs e)
        {
            //Main.Content = new Settings(Main, loginPage);
        }


        private void LoadMaturskiPanel()
        {
            MSt_1.Children.Clear();
            MSt_2.Children.Clear();
            proektna_zadaca.Children.Clear();

            List<Middleware.MaturskiPredmet> Predmeti = Ucenici[Br].MaturskiPredmeti;

            int PredmetCtr = 0;
            foreach (Middleware.MaturskiPredmet Predmet in Predmeti)
            {
                StackPanel st = new StackPanel();
                st.Width = double.NaN;
                st.HorizontalAlignment = HorizontalAlignment.Stretch;

                //Title
                st.Children.Add(SettingsDesign.ContentBorder(Predmet.Ime));
                //ComboBox
                ComboBox CB = SettingsDesign.CreateComboBox(PredmetCtr.ToString(), Predmet.MozniPredmeti);
                CB.SelectedValue += Ucenici[Br].MaturskiPredmeti[PredmetCtr].IzbranPredmet;
                CB.SelectionChanged += MaturskiPredmetCB_SelectionChanged;



                if (Predmet.Ime != "Проектна задача") st.Children.Add(CB);

                List<Middleware.MaturskoPole> MaturskiPolinja = Predmet.MaturskiPolinja;

                int PoleCtr = 0;
                foreach (Middleware.MaturskoPole Pole in MaturskiPolinja)
                {
                    //Ime na Pole
                    if (Predmet.Ime == "Интерен Предмет" && Pole.Ime == "Перцентилен ранг")
                    {
                        PoleCtr++;
                        continue;
                    }
                    if (Predmet.Ime == "Проектна задача" && Pole.Ime == "Перцентилен ранг")
                    {
                        PoleCtr++;
                        continue;
                    }
                    if (Pole.Ime == "Име" && Predmet.Ime != "Проектна задача")
                    {
                        PoleCtr++;
                        continue;
                    }
                    //if (Pole.Vrednost != "") 

                    Border MaturskoPoleIme = SettingsDesign.ContentBorder(Pole.Ime);
                    // MaturskoPoleIme.HorizontalAlignment = HorizontalAlignment.Left;
                    MaturskoPoleIme.Margin = new Thickness(5, 0, 5, 5);


                    //Odgovor
                    TextBox MaturskoPoleTxt = SettingsDesign.ContentTextBox(Pole.GetVrednost());
                    MaturskoPoleTxt.HorizontalAlignment = HorizontalAlignment.Stretch;
                    MaturskoPoleTxt.Margin = new Thickness(0, 0, 0, 10);
                    MaturskoPoleTxt.FontSize = 30;
                    MaturskoPoleTxt.Tag = PredmetCtr.ToString() + "|" + PoleCtr.ToString();
                    // MaturskoPoleTxt.Width = double.NaN; // isto kako Width = Auto
                    // MaturskoPoleTxt.TextChanged += MaturskoPole_TextChanged;
                    MaturskoPoleTxt.LostFocus += MaturskoPole_LostFocus;

                    //StackPanel
                    // mislam deka DockPanel e podobra opcija
                    DockPanel dp = new DockPanel();
                    dp.HorizontalAlignment = HorizontalAlignment.Stretch;
                    // dp.Width = double.NaN; // Width = Auto

                    dp.LastChildFill = true;

                    dp.Margin = new Thickness(20, 5, 25, 0);
                    // dp.Orientation = Orientation.Horizontal;

                    DockPanel.SetDock(MaturskoPoleIme, Dock.Left);
                    DockPanel.SetDock(MaturskoPoleTxt, Dock.Right);

                    dp.Children.Add(MaturskoPoleIme);
                    dp.Children.Add(MaturskoPoleTxt);

                    st.Children.Add(dp);
                    st.Children.Add(SettingsDesign.UnderTextBorder());

                    PoleCtr++;
                }

                if (Predmet.Ime != "Проектна задача")
                    if (PredmetCtr % 2 == 0) MSt_1.Children.Add(st);
                    else MSt_2.Children.Add(st);
                else proektna_zadaca.Children.Add(st);
                PredmetCtr++;
            }

        }

        private async void MaturskoPole_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)sender;
            string Tag = tx.Tag.ToString();

            int PredmetCtr = int.Parse(Tag.Split('|')[0]);
            int PoleCtr = int.Parse(Tag.Split('|')[1]);

            //PoleCtr++;
            //if (PredmetCtr == 3 && PoleCtr > 0) PoleCtr++;
            //else if (PredmetCtr == 4 && PoleCtr > 1) PoleCtr++;

            // ime na pole e hardcoded, kje treba da se napravi enum
            if (Ucenici[Br].MaturskiPredmeti[PredmetCtr].MaturskiPolinja[PoleCtr].Ime == "Перцентилен ранг")
            {
                decimal d;
                var didSucceed = decimal.TryParse(tx.Text, out d);
                if (!didSucceed)
                {
                    tx.Text = "00.00";
                }
                tx.Text = tx.Text.Replace(',', '.');
            }

            Ucenici[Br].MaturskiPredmeti[PredmetCtr].MaturskiPolinja[PoleCtr].SetVrednost(tx.Text);
            await Ucenici[Br].UpdateMaturska(UserKlas._token);
            //Console.WriteLine(Ucenici[Br].MaturskiPredmeti[PredmetCtr].GetOutParam());
        }

        private async void MaturskiPredmetCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CB = (ComboBox)sender;
            int PredmetCtr = int.Parse(CB.Tag.ToString());
            Console.WriteLine(PredmetCtr);
            Ucenici[Br].MaturskiPredmeti[PredmetCtr].IzbranPredmet = CB.SelectedValue.ToString();
            await Ucenici[Br].UpdateMaturska(UserKlas._token);
        }

        private async void MaturskoPole_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx = (TextBox)sender;
            string Tag = tx.Tag.ToString();

            int PredmetCtr = int.Parse(Tag.Split('|')[0]);
            int PoleCtr = int.Parse(Tag.Split('|')[1]);

            //PoleCtr++;
            //if (PredmetCtr == 3 && PoleCtr > 0) PoleCtr++;
            //else if (PredmetCtr == 4 && PoleCtr > 1) PoleCtr++;
            Ucenici[Br].MaturskiPredmeti[PredmetCtr].MaturskiPolinja[PoleCtr].SetVrednost(tx.Text);
            await Ucenici[Br].UpdateMaturska(UserKlas._token);
            //Console.WriteLine(Ucenici[Br].MaturskiPredmeti[PredmetCtr].GetOutParam());
        }
    }
}

//public class MaturskiPredmet
//{
//    public string Ime;
//    public string IzbranPredmet = "";
//    public string[] MozniPredmeti;
//    public List<MaturskoPole> MaturskiPolinja = new List<MaturskoPole>();

//    public MaturskiPredmet(string ime , string[] moznipredmeti)
//    {
//        Ime = ime;
//        MozniPredmeti = moznipredmeti;

//        if (MozniPredmeti.Length > 0) IzbranPredmet = moznipredmeti[0];

//        //MaturskiPolinja
//        MaturskiPolinja.Add(new MaturskoPole("Ocenka", "5"));
//        if(ime != "Interen")
//        MaturskiPolinja.Add(new MaturskoPole("Percentiran", "5,0"));
//        MaturskiPolinja.Add(new MaturskoPole("Datum", "01.01.2004"));
//        MaturskiPolinja.Add(new MaturskoPole("delovoden", "2/5"));
//    }

//    public string GetOutParam()
//    {
//        string rez = Ime;
//        string Delimetar = "|";
//        rez += Delimetar;
//        rez += IzbranPredmet;
//        rez += Delimetar;
//        foreach(MaturskoPole Pole in MaturskiPolinja)
//        {
//            rez += Pole.Ime;
//            rez += Delimetar;
//            rez += Pole.GetVrednost();
//            rez += Delimetar;
//        }

//        return rez;
//    }
//}

//    public class MaturskoPole
//    {
//        public string Ime;
//        public string Vrednost = "";
//        public string DefaultVrednost;

//        public MaturskoPole(string ime , string defaultvrednost)
//        {
//            Ime = ime;
//            DefaultVrednost = defaultvrednost;
//        }

//        public string GetVrednost()
//        {
//            if (Vrednost == "") return DefaultVrednost;
//            else return Vrednost;
//        }

//        public void SetVrednost(string vred)
//        {
//            if (vred == "") return;
//            else Vrednost = vred;
//        }
//    }
//}
