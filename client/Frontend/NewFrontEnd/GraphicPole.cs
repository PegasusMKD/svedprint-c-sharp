using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Frontend.NewFrontEnd
{
     class Design
    {
        public Border Title;
        public UIElement AnswerGrid;
        public Border UnderBorder;
        string Answer = "";

        public Design(Border title, UIElement answerGrid, Border underBorder)
        {
            Title = title;
            AnswerGrid = answerGrid;
            UnderBorder = underBorder;
        }

        public Design(Border title, UIElement answerGrid)
        {
            Title = title;
            AnswerGrid = answerGrid;
            UnderBorder = CreateDefaultUnderBorder();
        }

        public Design(string Question, UIElement answerGrid)
        {
            Title = CreateDefaultTitle(Question);
            AnswerGrid = answerGrid;
            UnderBorder = CreateDefaultUnderBorder();
        }

        public Design()
        {

        }

        public StackPanel GetDesign()
        {
            StackPanel st = new StackPanel();
            st.Margin = new Thickness(10);
            st.Children.Add(Title);
            st.Children.Add(AnswerGrid);
            st.Children.Add(UnderBorder);
            return st;
        }

        public void SetTitle(Border title)
        {
            Title = title;
        }

        public void SetAnswerBox(Object AnswerBox)
        {
            this.AnswerGrid = (UIElement)AnswerBox;
        }

        public void SetUnderBorder(Border UnderBorder)
        {
            this.UnderBorder = UnderBorder;
        }

        public static Border CreateDefaultTitle(string Content)
        {
            Border bd = new Border();
            bd.Height = 50.0;
            bd.VerticalAlignment = VerticalAlignment.Top;
            bd.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFED6A3E");//(Brush)Application.Current.FindResource("MenuItemColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(10);
            bd.Child = CreateDefaultLabel(Content);
            return bd;
        }
        public static Label CreateDefaultLabel(string Content)
        {
            Label lbl = new Label();
            lbl.Content = Content;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            lbl.FontFamily = new FontFamily("Arial");
            lbl.FontWeight = FontWeights.Bold;
            if (Content.Length < 15) lbl.FontSize = 30.0;
            else lbl.FontSize = 30.0 / (Content.Length / 15);
            lbl.Foreground = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#fff");//(Brush)Application.Current.FindResource("TextColor");
            return lbl;
        }
        public static Border CreateDefaultUnderBorder()
        {
            Border bd = new Border();
            bd.Height = 10;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#fff");//(Brush)Application.Current.FindResource("UnderBorderColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(5);
            return bd;
        }
        public static Image ImageBox(string ImagePath, int broj_na_predmet = 0)
        {
            Image img = new Image();
            img.Height = 40.0;
            img.HorizontalAlignment = HorizontalAlignment.Right;
            BitmapImage ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            img.Source = ImageSource;
            img.Tag = broj_na_predmet;
            img.Margin = new Thickness(0, 0, 3, 0);
            return img;
        }

        public string GetAnswer()
        {
            return Answer;
        }

        public void SetAnswer(string Answer)
        {
            this.Answer = Answer;
        }

    }

    class DefaultPole
    {
        Design DesignPole = null;
        object Model_object = null;
        string Binding = null;
        public DefaultPole(string Question, string Answer, object Model_object, string Binding)
        {
            this.Model_object = Model_object;
            this.Binding = Binding;

            DesignPole = new Design(Question, GetAnswerBox(Answer));
        }

        public TextBox GetAnswerBox(string Text, int i = -1)
        {
            TextBox tx = new TextBox();
            if (Text.Length < 15) tx.FontSize = 35.0;
            else tx.FontSize = 35.0 / (Text.Length / 10);
            tx.Background = Brushes.Transparent;
            tx.Foreground = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#ffffff");//(Brush)Application.Current.FindResource("TextColor");
            tx.HorizontalAlignment = HorizontalAlignment.Stretch;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.Margin = new Thickness(5);
            if (Binding != null)
            {
                Binding myBind = new Binding();
                myBind.Path = new PropertyPath(Binding);
                myBind.Source = Model_object;
                myBind.Mode = BindingMode.TwoWay;
                myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                tx.SetBinding(TextBox.TextProperty, myBind);
            }
            if (tx.Text == "") tx.Text = Text;
            tx.LostFocus += AnswerBox_LostFocus;
            //Answer = tx.Text;
            tx.Tag = i;
            //AnswerPoleObject = tx;
            return tx;
        }

        private void AnswerBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var txt = (TextBox)(sender);
            Console.WriteLine("Box {0} updated", txt.Tag);
        }

        public UIElement GetDesign()
        {
            return DesignPole.GetDesign();
        }
    }

   /* class PredmetiPole
    {
        Design DesignPole;

        public PredmetiPole(List<Predmeti> Predmeti, string Smer)
        {
            DesignPole = new Design(Design.CreateDefaultTitle(Smer), GetPredmeti(Predmeti), Design.CreateDefaultUnderBorder());
        }

        public StackPanel GetDesign()
        {
            return DesignPole.GetDesign();
        }

        StackPanel GetPredmeti(List<Predmeti> Predmeti)
        {
            StackPanel st = new StackPanel();
            st.VerticalAlignment = VerticalAlignment.Top;
            int i = 0;
            foreach (Predmeti answer in Predmeti)
            {
                st.Children.Add(AnswerBox_Predmeti(answer.Name, Predmeti, i));
                if (i++ < Predmeti.Count - 1) st.Children.Add(Design.CreateDefaultUnderBorder());
            }
            return st;
        }

        Grid AnswerBox_Predmeti(String Text, List<Predmeti> Predmeti, int broj_na_predmet)
        {
            Grid gd = new Grid();
            gd.Children.Add(GetAnswerBox(Text, Predmeti, broj_na_predmet));
            gd.Children.Add(Design.ImageBox("Resources/Icons/x.png", broj_na_predmet));
            return gd;
        }

        public TextBox GetAnswerBox(string Text, List<Predmeti> Predmeti, int i)
        {
            TextBox tx = new TextBox();
            if (Text.Length < 15) tx.FontSize = 35.0;
            else tx.FontSize = 35.0 / (Text.Length / 10);
            tx.Background = Brushes.Transparent;
            tx.Foreground = (Brush)Application.Current.FindResource("TextColor");
            tx.HorizontalAlignment = HorizontalAlignment.Stretch;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.Margin = new Thickness(5);

            Binding myBind = new Binding();
            myBind.Path = new PropertyPath("Name");
            myBind.Source = Predmeti[i];
            myBind.Mode = BindingMode.TwoWay;
            myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            tx.SetBinding(TextBox.TextProperty, myBind);

            if (tx.Text == "") tx.Text = Text;
            //tx.LostFocus += AnswerBox_LostFocus;
            //Answer = tx.Text;
            tx.Tag = i;
            //AnswerPoleObject = tx;
            return tx;


        }
    }*/

    class RadioBtnsPole
        {
            Design design = null;
            object Model_object = null;
            string Binding = null;
            public RadioBtnsPole(string Question, string[] Answer, object Model_object, string Binding)
            {
                this.Model_object = Model_object;
                this.Binding = Binding;

                design = new Design(Question, GetRadioButtons(Answer));
            }

            public UIElement GetDesign()
            {
                return design.GetDesign();
            }

            Viewbox GetRadioButtons(string[] Questions)
            {
                Viewbox vp = new Viewbox();
                UniformGrid dp = new UniformGrid();
                dp.Columns = 2;
                int i = 0;
                foreach (string answer in Questions)
                {
                    RadioButton btn = new RadioButton();
                    btn.Foreground = (Brush)Application.Current.FindResource("TextColor");
                    btn.Margin = new Thickness(20, 5, 20, 5);
                    btn.Content = answer;
                    btn.Tag = i++;
                    btn.Name = Binding;
                    if (answer == Model_object.GetType().GetProperty(Binding).GetValue(Model_object, null).ToString()) btn.IsChecked = true;
                    btn.Checked += RadioBtn_Checked;
                    dp.Children.Add(btn);
                }

                vp.Child = dp;
                return vp;
            }

            private void RadioBtn_Checked(object sender, RoutedEventArgs e)
            {
                RadioButton radioBtn = (RadioButton)sender;
                design.SetAnswer(radioBtn.Content.ToString());
                Model_object.GetType().GetProperty(Binding).SetValue(Model_object, design.GetAnswer());
                Console.WriteLine(design.GetAnswer());
                //Model_object.GetType().GetProperty(Parametar).SetValue(Model_object, Answer);
            }

            private void AnswerBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                var txt = (TextBox)(sender);
                Console.WriteLine("Box {0} updated", txt.Tag);
            }


        }
     
    class ComboBoxPole
        {
            Design design = null;
            object Model_object = null;
            string Binding = null;
            public ComboBoxPole(string Question, string[] Answer, object Model_object, string Binding)
            {
                this.Model_object = Model_object;
                this.Binding = Binding;

                design = new Design(Question, GetComboBox(Answer));
            }

            public UIElement GetDesign()
            {
                return design.GetDesign();
            }

            public ComboBox GetComboBox(string[] Questions)
            {
                ComboBox cb = new ComboBox();
                cb.ItemsSource = Questions;

                if (Binding != null)
                {
                    Binding myBind = new Binding();
                    myBind.Path = new PropertyPath(Binding);
                    myBind.Source = Model_object;
                    myBind.Mode = BindingMode.TwoWay;
                    myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    cb.SetBinding(ComboBox.SelectedValueProperty, myBind);
                }
                if (cb.SelectedValue == null) cb.SelectedIndex = 0;

                cb.FontSize = 35.0;
                cb.VerticalAlignment = VerticalAlignment.Top;
                cb.SelectionChanged += CB_SelectionChanged;
                return cb;
            }

            private void CB_SelectionChanged(object sender, RoutedEventArgs e)
            {
                var cb = (ComboBox)(sender);
                design.SetAnswer(cb.SelectedItem.ToString());
            }
        }

    class PasswordBoxPole
        {
            Design design = null;
            object Model_object = null;
            PasswordBox PassBox = null;
            public PasswordBoxPole(string Question, object Model_object)
            {
                this.Model_object = Model_object;
                design = new Design(Question, GetPasswordGrid());
            }

            public UIElement GetDesign()
            {
                return design.GetDesign();
            }

            private PasswordBox GetPasswordBox()
            {
                PassBox = new PasswordBox();
                PassBox.FontSize = 35.0;
                PassBox.Background = Brushes.Transparent;
                PassBox.Foreground = (Brush)Application.Current.FindResource("TextColor");
                //PassBox.SelectionBrush = Brushes.Transparent;
                PassBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                PassBox.VerticalAlignment = VerticalAlignment.Center;
                PassBox.Margin = new Thickness(5, 0, 45, 0);
                PassBox.BorderThickness = new Thickness(0);
                return PassBox;
            }

            private Grid GetPasswordGrid()
            {
                Grid grd = new Grid();
                grd.Children.Add(GetPasswordBox());

                Image img = Design.ImageBox("Resources/Icons/stikla.png");
                img.MouseLeftButtonDown += ConfirmBtn_Clicked;
                grd.Children.Add(img);
                return grd;
            }

            private void ConfirmBtn_Clicked(object sender, MouseButtonEventArgs e)
            {
                //Update
                Console.WriteLine(PassBox.Password);
            }
        }

    class CheckBoxPole
        {
            Design DesignPole = null;
            object Model_object = null;
            string Binding = null;
            public CheckBoxPole(string Question, string DefaultAnswer, object Model_object, string Binding)
            {
                this.Model_object = Model_object;
                this.Binding = Binding;

                DesignPole = new Design(Question, GetCheckBox(DefaultAnswer));
                DesignPole.SetAnswer(DefaultAnswer);
            }
            public UIElement GetDesign()
            {
                return DesignPole.GetDesign();
            }

            Viewbox GetCheckBox(string DefaultAnswer)
            {
                CheckBox Checkb = new CheckBox();

                if (Binding != "")
                {
                    Binding myBind = new Binding();
                    myBind.Path = new PropertyPath(Binding);
                    myBind.Source = Model_object;
                    myBind.Mode = BindingMode.TwoWay;
                    Checkb.SetBinding(CheckBox.IsCheckedProperty, myBind);
                }

                if (DefaultAnswer == "True") Checkb.IsChecked = true;
                else Checkb.IsChecked = false;

                Checkb.Checked += Checkb_CheckedChange;
                Checkb.Unchecked += Checkb_CheckedChange;

                Viewbox vb = new Viewbox();
                vb.HorizontalAlignment = HorizontalAlignment.Center;
                vb.VerticalAlignment = VerticalAlignment.Center;
                vb.Height = 60.0;
                vb.Child = Checkb;
                return vb;
            }

            private void Checkb_CheckedChange(object sender, RoutedEventArgs e)
            {
                var Btn = (CheckBox)(sender);
                DesignPole.SetAnswer(Btn.IsChecked.ToString());
            }
        }

    class Template
        {
            Design DesignPole = null;
            object Model_object = null;
            string Binding = null;
            public Template(string Question, string Answer, object Model_object, string Binding)
            {
                this.Model_object = Model_object;
                this.Binding = Binding;

                //DesignPole = new Design(Question, answer);
            }
            public UIElement GetDesign()
            {
                return DesignPole.GetDesign();
            }
        }
}
