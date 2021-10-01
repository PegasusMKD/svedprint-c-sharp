using MiddlewareRevisited.Models;
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

        List<SubjectOrientation> subjectOrientations;
        User currentUser;
        List<Student> students;

        public Smerovi_Page(ref List<SubjectOrientation> subjectOrientations, ref User currentUser, ref List<Student> students)
        {
            InitializeComponent();

            this.subjectOrientations = subjectOrientations;
            this.currentUser = currentUser;
            this.students = students;

            UpdatePanels();
        }

        List<TextBox> DodajPredmeti = new List<TextBox>();
        private bool ContentTextChanged;

        private void UpdatePanels()
        {
            DodajPredmeti.Clear();
            st1.Children.Clear();
            st2.Children.Clear();
            int SmerCtr = 0;

            foreach (SubjectOrientation subjectOrientation in subjectOrientations)
            {
                List<string> Predmeti = subjectOrientation.subjects;

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
                    st1.Children.Add(ContentBorder(subjectOrientation.shortName));
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(ContentBorder(subjectOrientation.shortName));
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
                st1.Children.Add(NewSmerST);
            else
                st2.Children.Add(NewSmerST);
        }

        private void St_MouseLeave(object sender, MouseEventArgs e) => ContentTextChanged = false;

        private void Ctx_TextChanged(object sender, TextChangedEventArgs e) => ContentTextChanged = true;

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(50, 5, 20, 10, "#FFED6A3E");
            DockPanel DP = new DockPanel();
            DP.HorizontalAlignment = HorizontalAlignment.Center;
            DP.VerticalAlignment = VerticalAlignment.Center;
            DP.Children.Add(CreateLabel(LabelContent, 30, "Arial"));
            if (LabelContent != "Додај Смер" && LabelContent != "зачувај")
                DP.Children.Add(CreateTrashIcon(LabelContent));
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
            Image img = new Image
            {
                Source = new BitmapImage(new Uri(@"/Images/check_icon.png", UriKind.Relative))
            };

            if (IsX)
            {
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => RemovePredmetImgClicked(sender, e, SmerCtr, PredmetCtr));
                img.MouseEnter += RemovePredmedimgMouseEnter;
                img.MouseLeave += RemovePredmedimgMouseLeave;
            }
            else
                img.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => NewPredmetImgClicked(sender, e, SmerCtr, PredmetCtr));

            border.Child = img;
            gd.Children.Add(border);
            return gd;
        }

        private Border NewSmerSaveLabel()
        {
            Border bd = ContentBorder("зачувај");
            bd.Background = new BrushConverter().ConvertFrom("#FF3D84C6") as SolidColorBrush;
            bd.MouseLeftButtonDown += NewSmerSaveClicked;
            return bd;
        }

        private async void NewSmerSaveClicked(object sender, MouseButtonEventArgs e)
        {
            SubjectOrientation subjectOrientation = new SubjectOrientation();
            subjectOrientation.shortName = DodajPredmeti[DodajPredmeti.Count() - 2].Text;
            subjectOrientation.fullName = DodajPredmeti[DodajPredmeti.Count - 1].Text;

            subjectOrientation = await MiddlewareRevisited.Controllers.SubjectOrientation.AddSubjectOrientation(subjectOrientation);

            currentUser.schoolClass.subjectOrientations.Add(subjectOrientation);

            UpdatePanels();
        }

        private async void NewPredmetImgClicked(object sender, MouseButtonEventArgs e, int i, int j)
        {
            SubjectOrientation subjectOrientation = subjectOrientations[i];
            subjectOrientation.subjects.Add(DodajPredmeti[j].Text);

            await MiddlewareRevisited.Controllers.SubjectOrientation.UpdateSubjectOrientation(subjectOrientation);
            UpdatePanels();
        }

        private async void RemovePredmetImgClicked(object sender, MouseButtonEventArgs e, int i, int j)
        {
            SubjectOrientation subjectOrientation = subjectOrientations[i];
            subjectOrientation.subjects.RemoveAt(j);

            await MiddlewareRevisited.Controllers.SubjectOrientation.UpdateSubjectOrientation(subjectOrientation);
            UpdatePanels();
        }

        private void RemovePredmedimgMouseEnter(object sender, MouseEventArgs e)
        {
            if (ContentTextChanged == true) return;
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri(@"/Images/x_2.png", UriKind.Relative));
        }

        private void RemovePredmedimgMouseLeave(object sender, MouseEventArgs e)
        {
            Image img = (Image)sender;
            img.Source = new BitmapImage(new Uri(@"/Images/check_icon.png", UriKind.Relative));
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
            img.Source = new BitmapImage(new Uri(@"/Images/trash_icon.png", UriKind.Relative));
            img.HorizontalAlignment = HorizontalAlignment.Right;
            img.Margin = new Thickness(10, 5, 10, 5);
            img.MouseLeftButtonDown += Img_MouseLeftButtonDown;
            return img;
        }

        private async void Img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            /*
             * 
             * Basically delete
             * 
             * TODO: Add remove handling for students in backend
             * */

            //if (UserKlas._p._smerovi.Count <= 4) //  && new List<string>() { "I", "II" }.Contains(UserKlas._paralelka.Split('-')[0])) ||
            //   // (UserKlas._p._smerovi.Count <= 3 && new List<string>() { "III", "IV" }.Contains(UserKlas._paralelka.Split('-')[0]))
            //{
            //    MessageBox.Show("неможе сите смерови да се избришат");
            //    return;
            //}
            //Image img = (Image)sender;
            //String smer = img.Tag.ToString();
            //Smer NovSmer;
            //if (UserKlas._p._smerovi.Keys.ElementAt(0) != smer)
            //{
            //    NovSmer = UserKlas._p._smerovi.Values.ElementAt(0);
            //}
            //else NovSmer = UserKlas._p._smerovi.Values.ElementAt(1);
            //int ctr = 0;
            //foreach (Ucenik ucenik in Ucenici)
            //{
            //    if (ucenik._smer == smer)
            //    {
            //        ucenik.ChangeSmerAsync(NovSmer, UserKlas._token);
            //    }
            //    ctr++;
            //}
            //UserKlas._p._smerovi[smer].RemoveSmer(UserKlas._token);
            //UserKlas._p._smerovi.Remove(smer);
            Image img = (Image)sender;
            string shortName = img.Tag.ToString();
            SubjectOrientation subjectOrientation = (from orientation in subjectOrientations where orientation.shortName.Equals(shortName) select orientation).FirstOrDefault();
            await MiddlewareRevisited.Controllers.SubjectOrientation.RemoveSubjectOrientation(subjectOrientation);
            subjectOrientations.RemoveAll(orientation => orientation.id.Equals(subjectOrientation.id));
            UpdatePanels();
        }

    }
}
