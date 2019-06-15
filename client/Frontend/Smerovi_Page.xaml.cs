using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Frontend.SettingsDesign;

namespace Frontend
{
    public partial class Smerovi_Page : Page
    {

        Klasen UserKlas;
        Dictionary<string, Smer> smerovi;
        List<Ucenik> Ucenici;


        public Smerovi_Page()
        {
            InitializeComponent();

            UserKlas = Home_Page.KlasenKlasa;
            smerovi = Home_Page.smerovi;
            Ucenici = Home_Page.ucenici;

            GetData();
        }

        List<TextBox> DodajPredmeti = new List<TextBox>();
        bool ContentTextChanged = false;
        private void GetData()
        {

            DodajPredmeti.Clear();
            st1.Children.Clear();
            st2.Children.Clear();
            int SmerCtr = 0;

            foreach (string x in UserKlas._p._smerovi.Keys)
            {
                List<String> Predmeti = UserKlas._p._smerovi[x]._predmeti;

                StackPanel st = new StackPanel();
                int PredmetCtr = 0;
                foreach (string s in Predmeti)
                {

                    TextBox ctx = new TextBox();
                    ctx = ContentTextBox(s);
                    ctx.TextChanged += Ctx_TextChanged;
                    ctx.Name = "n" + SmerCtr.ToString() + "s" + PredmetCtr.ToString();
                    st.Children.Add(ctx);
                    st.Children.Add(TextBorderGrid(true, SmerCtr, PredmetCtr++));
                }

                if (SmerCtr % 2 == 0)
                {
                    st1.Children.Add(ContentBorder(x));
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(ContentBorder(x));
                    st2.Children.Add(st);
                }
                st.MouseLeave += St_MouseLeave;


                DodajPredmeti.Add(ContentTextBox("Додавај Предмет"));
                st.Children.Add(DodajPredmeti[DodajPredmeti.Count - 1]);
                st.Children.Add(TextBorderGrid(false, SmerCtr, DodajPredmeti.Count - 1));

                SmerCtr++;
            }

            StackPanel NewSmerST = new StackPanel();
            Border NewSmerCB = ContentBorder("Додај Смер");
            NewSmerST.Children.Add(NewSmerCB);

            DodajPredmeti.Add(ContentTextBox("кратенка"));
            NewSmerST.Children.Add(DodajPredmeti[DodajPredmeti.Count - 1]);
            NewSmerST.Children.Add(UnderTextBorder());

            DodajPredmeti.Add(ContentTextBox("цел смер"));
            NewSmerST.Children.Add(DodajPredmeti[DodajPredmeti.Count - 1]);
            NewSmerST.Children.Add(UnderTextBorder());

            NewSmerST.Children.Add(NewSmerSaveLabel());

            if (SmerCtr % 2 == 0)
            {
                st1.Children.Add(NewSmerST);
            }
            else
            {
                st2.Children.Add(NewSmerST);
            }
        }

        private void St_MouseLeave(object sender, MouseEventArgs e)
        {
            ContentTextChanged = false;
        }

        private void Ctx_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContentTextChanged = true;
            TextBox tx = (TextBox)sender;
            int SmerCtr = int.Parse(tx.Name.Substring(1).Split('s')[0]);
            int PredmetCtr = int.Parse(tx.Name.Substring(1).Split('s')[1]);
            UserKlas._p._smerovi[UserKlas._p._smerovi.Keys.ElementAt(SmerCtr)].UpdatePredmet(PredmetCtr, tx.Text, UserKlas._token);
            UpdateVar();
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(50, 5, 20, 10, "#FFED6A3E");
            DockPanel DP = new DockPanel();
            DP.HorizontalAlignment = HorizontalAlignment.Center;
            DP.VerticalAlignment = VerticalAlignment.Center;
            DP.Children.Add(CreateLabel(LabelContent, 30, "Arial"));
            if (LabelContent != "Додај Смер" && LabelContent != "зачувај") DP.Children.Add(CreateTrashIcon(LabelContent));
            bd.Child = DP;
            return bd;
        }

        private Grid TextBorderGrid(bool IsX, int SmerCtr, int PredmetCtr)
        {
            Grid gd = new Grid();
            gd.Margin = new Thickness(0, 0, 0, 10);
            gd.Children.Add(UnderTextBorder());
            Border border = CreateBorder(36, 0, 0, 6, "#FF3D84C6");
            border.Margin = new Thickness(0, -42, 35, 0);
            border.Width = 35;
            border.HorizontalAlignment = HorizontalAlignment.Right;

            Image img = new Image();
            img.Source = new BitmapImage(new Uri("check_icon.png", UriKind.Relative));
            border.Child = img;

            gd.Children.Add(border);
            if (IsX == false)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => NewPredmetImgClicked(sender, e, SmerCtr, PredmetCtr));
            }
            if (IsX == true)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => RemovePredmetImgClicked(sender, e, SmerCtr, PredmetCtr));
                img.MouseEnter += RemovePredmedimgMouseEnter;
                img.MouseLeave += RemovePredmedimgMouseLeave;
            }
            return gd;
        }

        private Border NewSmerSaveLabel()
        {
            Border bd = ContentBorder("зачувај");
            bd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3D84C6"));
            bd.MouseLeftButtonDown += NewSmerSaveClicked;
            return bd;
        }

        private void NewSmerSaveClicked(object sender, MouseButtonEventArgs e)
        {
            // TODO:
            // validator za kratenica na smer, max 7 karakteri
            UserKlas._p.AddSmer(new Smer(DodajPredmeti[DodajPredmeti.Count() - 2].Text, DodajPredmeti[DodajPredmeti.Count - 1].Text), UserKlas._token);
            UserKlas.SetSmeroviPredmeti();
            UpdateVar();
            GetData();
        }

        private void NewPredmetImgClicked(object sender, MouseButtonEventArgs e, int i, int j)
        {
            string toBeChanged = UserKlas._p._smerovi.Keys.ElementAt(i);
            int ctr = 0;
            foreach (Ucenik Ucenik in Ucenici)
            {
                if (Ucenik._smer == toBeChanged)
                {
                    Ucenik._oceni.Add(1);
                    Ucenik.UpdateUcenikOceni(UserKlas._token);
                }
                ctr++;
            }
            UserKlas._p._smerovi[toBeChanged].AddPredmet(DodajPredmeti[j].Text, UserKlas._token);
            UpdateVar();
            GetData();
        }

        private void RemovePredmetImgClicked(object sender, MouseButtonEventArgs e, int i, int j)
        {
            if (ContentTextChanged)
            {
                return;
            }

            string toBeChanged = UserKlas._p._smerovi.Keys.ElementAt(i);
            if (i == 0) i = 1;

            int ctr = 0;
            foreach (Ucenik Ucenik in Ucenici)
            {
                if (Ucenik._smer == toBeChanged)
                {
                    if (Ucenik._oceni.Count > j) Ucenik._oceni.RemoveAt(j);
                    Ucenik.UpdateUcenikOceni(UserKlas._token);
                }
                ctr++;
            }
            UserKlas._p._smerovi[toBeChanged].RemovePredmet(j, UserKlas._token);
            UpdateVar();
            GetData();
        }

        private void RemovePredmedimgMouseEnter(object sender, MouseEventArgs e)
        {
            if (ContentTextChanged == true) return;
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri("x_2.png", UriKind.Relative));
        }

        private void RemovePredmedimgMouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri("check_icon.png", UriKind.Relative));
        }

        private TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 70, 0);
            tx.Text = Text;
            return tx;
        }

        private Image CreateTrashIcon(string smer)
        {
            Image img = new Image();
            img.Tag = smer;
            img.Source = new BitmapImage(new Uri("trash_icon.png", UriKind.Relative));
            img.HorizontalAlignment = HorizontalAlignment.Right;
            img.Margin = new Thickness(10, 5, 10, 5);
            img.MouseLeftButtonDown += Img_MouseLeftButtonDown;
            return img;
        }

        private void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (UserKlas._p._smerovi.Count < 2)
            {
                MessageBox.Show("неможе сите смерови да се избришат");
                return;
            }
            Image img = (Image)sender;
            String smer = img.Tag.ToString();
            Smer NovSmer;
            if (UserKlas._p._smerovi.Keys.ElementAt(0) != smer)
            {
                NovSmer = UserKlas._p._smerovi.Values.ElementAt(0);
            }
            else NovSmer = UserKlas._p._smerovi.Values.ElementAt(1);
            int ctr = 0;
            foreach (Ucenik ucenik in Ucenici)
            {
                if (ucenik._smer == smer)
                {
                    ucenik.ChangeSmer(NovSmer, UserKlas._token);
                }
                ctr++;
            }
            UserKlas._p._smerovi[smer].RemoveSmer(UserKlas._token);
            UserKlas._p._smerovi.Remove(smer);
            UpdateVar();
            GetData();
        }

        private void UpdateVar()
        {
            Home_Page.KlasenKlasa = UserKlas;
            Home_Page.ucenici = Ucenici;
        }

    }
}
