using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdminPanel
{
    public class Pole
    {
        public string Name { get; set; }

        public string[] Question = new string[] { };

        public string Answer;
        public string Type { get; set; }

        public string Parametar;

        object AnswerPoleObject;

        public Pole()//Default Construktor
        {
            
        }
        public Pole(int a)//test
        {
            Design DefaultPole = new Design(Title(), (UIElement)GetAnswerPole(), UnderBorder());
            DefaultPole.GetPanel();
        }
        public Pole(string Name, string[] Question, string Parametar, string Type = null , string Answer = null)//Site Polinja
        {
            this.Name = Name;
            this.Question = Question;
            this.Answer = Answer;
            this.Parametar = Parametar;
            this.Type = Type;
        }

        Border Title()
        {
            Border bd = new Border();
            bd.Height = 50.0;
            bd.VerticalAlignment = VerticalAlignment.Top;
            bd.Background = (Brush)Application.Current.FindResource("MenuItemColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(10);
            bd.Tag = Type;
            bd.Child = GetLabel(Name);
            return bd;
        }
        Label GetLabel(string Content)
        {
            Label lbl = new Label();
            lbl.Content = Content;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            lbl.FontFamily = new FontFamily("Arial");
            lbl.FontWeight = FontWeights.Bold;
            if (Content.Length < 15) lbl.FontSize = 30.0;
            else lbl.FontSize = 30.0 / (Content.Length / 15);
            lbl.Foreground = (Brush)Application.Current.FindResource("TextColor");
            return lbl;
        }
        Object GetAnswerPole()
        {
            if (Type != null)
            {
                if (Type == "PW") return GetPasswordBox();
                if (Type == "Predmeti") return GetPredmeti();
            }
            if (Question.Length == 1) return AnswerBox(Question[0], new Thickness(5, 0, 5, 0));
            if (Enumerable.SequenceEqual(Question, new string[] { "True", "False" })) return GetCheckBox();
            if (Question.Length < 5) return GetRadioButtons();
            if (Question.Length >= 5) return GetComboBox();
            return null;
        }
        StackPanel GetPredmeti()
        {
            StackPanel st = new StackPanel();
            st.VerticalAlignment = VerticalAlignment.Top;
            int i = 0;
            foreach (string answer in Question)
            {
                st.Children.Add(AnswerBox_Predmeti(answer , i));
                if (i++ < Question.Length - 1) st.Children.Add(UnderBorder());
            }
            return st;
        }
        Grid AnswerBox_Predmeti(String Text , int broj_na_predmet)
        {
            Grid gd = new Grid();
            gd.Children.Add(AnswerBox(Text, new Thickness(5, 0, 55, 0) , broj_na_predmet));
            gd.Children.Add(ImageBox("Resources/Icons/x.png" , broj_na_predmet));
            return gd;
        }
        TextBox AnswerBox(string Text, Thickness margin , int i = -1)
        {
            TextBox tx = new TextBox();
            if (Text.Length < 15) tx.FontSize = 35.0;
            else tx.FontSize = 35.0 / (Text.Length/10); 
            tx.Background = Brushes.Transparent;
            tx.Foreground = (Brush)Application.Current.FindResource("TextColor");
            tx.HorizontalAlignment = HorizontalAlignment.Stretch;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.Margin = margin;
            tx.Text = Text;
            tx.LostFocus += AnswerBox_LostFocus;
            Answer = tx.Text;
            tx.Tag = i;
            AnswerPoleObject = tx;
            if (Type == "PW") tx.Text = TurnIntoPW(Text.Length);
            return tx;
        }
        Image ImageBox(string ImagePath , int broj_na_predmet = 0)
        {
            Image img = new Image();
            img.Height = 40.0;
            img.HorizontalAlignment = HorizontalAlignment.Right;
            BitmapImage ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
            img.Source = ImageSource;
            img.Tag = broj_na_predmet;
            if (Type == "PW") img.MouseLeftButtonDown += PW_AnswerIcon_Clicked;
            if (Type == "Predmeti")
            {
                img.MouseLeftButtonDown += Predmeti_Del_Icon_Clicked;
            }
            return img;
        }
        Viewbox GetRadioButtons()
        {
            Viewbox vp = new Viewbox();
            UniformGrid dp = new UniformGrid();
            dp.Columns = 2;
            int i = 0;
            foreach (string answer in Question)
            {
                RadioButton btn = new RadioButton();
                btn.Foreground = (Brush)Application.Current.FindResource("TextColor");
                btn.Margin = new Thickness(20, 5, 20, 5);
                btn.Content = answer;
                btn.Tag = i++;
                btn.Checked += RadioBtn_Checked;
                dp.Children.Add(btn);
            }
            vp.Child = dp;
            return vp;
        }
        public ComboBox GetComboBox()
        {
            ComboBox cb = new ComboBox();
            cb.ItemsSource = Question;
            cb.SelectedItem = GetAnswer();
            cb.FontSize = 35.0;
            cb.VerticalAlignment = VerticalAlignment.Top;
            cb.SelectionChanged += CB_SelectionChanged;
            return cb;
        }
        Viewbox GetCheckBox()
        {
            CheckBox Checkb = new CheckBox();
            Checkb.Checked += Checkb_CheckedChange;
            Checkb.Unchecked += Checkb_CheckedChange;
            if (Answer == "True") Checkb.IsChecked = true;
            else Checkb.IsChecked = false;

            Viewbox vb = new Viewbox();
            vb.HorizontalAlignment = HorizontalAlignment.Center;
            vb.VerticalAlignment = VerticalAlignment.Center;
            vb.Height = 60.0;
            vb.Child = Checkb;
            return vb;
        }
        Grid GetPasswordBox()
        {
            Grid gd = new Grid();
            gd.Children.Add(AnswerBox(Question[0], new Thickness(5, 5, 50, 5)));
            gd.Children.Add(ImageBox("Resources/Icons/eye.png"));
            return gd;
        }
        Border UnderBorder()
        {
            Border bd = new Border();
            bd.Height = 10;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Background = (Brush)Application.Current.FindResource("UnderBorderColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(5);
            return bd;
        }
        public StackPanel GetPole() //Glavno
        {
            StackPanel Main = new StackPanel();
            Main.Margin = new Thickness(10);
            Main.Children.Add(Title());

            StackPanel content = new StackPanel();
            content.Children.Add((UIElement)GetAnswerPole());
            content.Children.Add(UnderBorder());
            content.Margin = new Thickness(5);

            Main.Children.Add(content);
            return Main;
        }

        //Actions
        private void AnswerBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)sender;
            if (Check_if_String_Ok(tx.Text) != '1') return;
            Answer = tx.Text;
            Update();
        }
        private void RadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioBtn = (RadioButton)sender;
            Answer = radioBtn.Content.ToString();
            Update();
        }
        private void Predmeti_Del_Icon_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var img = (Image)(sender);
            Console.WriteLine("Delete {0} - {1}" , Question[(int)img.Tag] , img.Tag.ToString());
        }
        private void PW_AnswerIcon_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var img = (Image)(sender);
            if(((TextBox)(AnswerPoleObject)).Text[0] !='*') Answer = ((TextBox)(AnswerPoleObject)).Text;
            Console.WriteLine(img.Tag);
            if ((int)img.Tag == 0)
            {
                ((TextBox)AnswerPoleObject).Text = GetAnswer();//Bug
            }
            else ((TextBox)AnswerPoleObject).Text = TurnIntoPW(GetAnswer().Length);

            img.Tag = ((int)img.Tag + 1) % 2;
            Console.WriteLine("Clicked");
        }
        private void Checkb_CheckedChange(object sender, RoutedEventArgs e)
        {
            var Btn = (CheckBox)(sender);
            Answer = Btn.IsChecked.ToString();
            Update();
        }
        private void CB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var cb = (ComboBox)(sender);
            Answer = cb.SelectedItem.ToString();
            Update();
        }

        //Calc
        char Check_if_String_Ok(string Text)
        {
            char[] illegalCharacters = { '*' };
            foreach(char i in Text)
            {
                foreach(char j in illegalCharacters)
                {
                   if( i==j ) return j;
                }
            }
            return '1';
        }
        string TurnIntoPW(int length)
        {
            return new string('*', length);
        }
        string GetAnswer()
        {
            if(Answer == null)
            {
                return Question[0];
            }
            return Answer;
        }
        private void Update()
        {
            Console.WriteLine("Update {0} - {1} - {2}" , Name ,Answer , Parametar);
            //UpdateData(Name, Answer, Parametar);
        }


        /* old code
         * 
         *  FrameworkElementFactory FE_Title()
        {
            var bd = new FrameworkElementFactory(typeof(Border));
            bd.SetValue(Border.HeightProperty, 50.0);
            bd.SetValue(Border.VerticalAlignmentProperty, VerticalAlignment.Top);
            //bd.SetValue(Border.MarginProperty, new Thickness(20, 20, 20, 0));
            bd.SetValue(Border.BackgroundProperty, Application.Current.FindResource("MenuItemColor"));
            bd.SetValue(Border.BorderThicknessProperty, new Thickness(2));
            bd.SetValue(Border.CornerRadiusProperty, new CornerRadius(10.0));
            bd.SetValue(Border.TagProperty, new Binding("Type"));
            //var lbl = LabelCtr();
            //lbl.SetValue(Label.ContentProperty, new Binding("Name"));

            bd.AppendChild(lbl);

            return bd;
        }
     FrameworkElementFactory LabelCtr()
     {
         var lbl = new FrameworkElementFactory(typeof(Label));
         lbl.SetValue(Label.HorizontalAlignmentProperty, HorizontalAlignment.Center);
         lbl.SetValue(Label.VerticalAlignmentProperty, VerticalAlignment.Center);
         lbl.SetValue(Label.ForegroundProperty, Application.Current.FindResource("TextColor"));
         lbl.SetValue(Label.FontSizeProperty, 30.0);
         lbl.SetValue(Label.FontFamilyProperty, new FontFamily("Arial"));
         lbl.SetValue(Label.FontWeightProperty, FontWeights.Bold);

         return lbl;
     }

     FrameworkElementFactory TextBox()
     {
         //<TextBox Text ="одговор" FontSize="35" Background="{x:Null}" Foreground="White" HorizontalAlignment="Center" VerticalAlignment="Center" BorderThickness="0"/>
         var tb = new FrameworkElementFactory(typeof(TextBox));
         tb.SetValue(TextBlock.FontSizeProperty , 35.0);
         tb.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
         tb.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
         tb.SetValue(Control.ForegroundProperty, Brushes.White);
         tb.SetValue(Control.BackgroundProperty, Brushes.Transparent);
         tb.SetValue(Control.BorderThicknessProperty, new Thickness(0));
         tb.SetValue(Control.MarginProperty, new Thickness(5, 0 , 5 , 0));
         tb.SetValue(TextBlock.TextProperty, new Binding("Answer[0]"));

         return tb;
     }

     FrameworkElementFactory BottomBorder()
     {
         //  <Border Height="10" VerticalAlignment="Center" Background="White" BorderThickness="2" CornerRadius="5"/>
         var bd = new FrameworkElementFactory(typeof(Border));
         bd.SetValue(Border.HeightProperty, 10.0);
         bd.SetValue(Border.VerticalAlignmentProperty, VerticalAlignment.Center);
         bd.SetValue(Border.BackgroundProperty, Brushes.White);
         bd.SetValue(Border.BorderThicknessProperty, new Thickness(2));
         bd.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));

         return bd;
     }



     public DataTemplate DT()
    {
         FrameworkElementFactory st = new FrameworkElementFactory(typeof(StackPanel));
         st.SetValue(StackPanel.MarginProperty, new Thickness(20));
         st.AppendChild(Title().);
         var t = );
         st.AppendChild(TextBox());
         st.AppendChild(BottomBorder());

         var template = new DataTemplate();
         template.VisualTree = st;

         return template;
    }

        void SetType()
        {
            /*
            if (Answer.Length = 0)//""se ceka odgovor od user"
            {
                Type = 0;
            }
            if(Answer.Length = 1)//"ima eden odgovor"
            {
                Type = 1;
            }
            if(Answer[0] == "*")//"password"
            {
                Type = 2;
            }
            if(Answer.Length == 2)//RadioBox
            {
                Type = 3;
            }
            if(Answer.Length > 4)//Combobox
            {
                Type = 4;
            }
            if (Answer == new string{ "0" , "1" })//CheckBox
            {
                Type = 5;
            }
        } /wpf binding 
        //old code */
    }

    class Design
    {
        public Border Title;
        public UIElement AnswerGrid;
        public Border UnderBorder;        

        public Design(Border title, UIElement answerGrid, Border underBorder)
        {
            Title = title;
            AnswerGrid = answerGrid;
            UnderBorder = underBorder;
        }

        public Border CreateDefaultTitle()
        {
            Border bd = new Border();
            bd.Height = 50.0;
            bd.VerticalAlignment = VerticalAlignment.Top;
            bd.Background = (Brush)Application.Current.FindResource("MenuItemColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(10);
            bd.Child = CreateDefaultLabel("Name");
            return bd;
        }
        public Label CreateDefaultLabel(string Content)
        {
            Label lbl = new Label();
            lbl.Content = Content;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
            lbl.FontFamily = new FontFamily("Arial");
            lbl.FontWeight = FontWeights.Bold;
            if (Content.Length < 15) lbl.FontSize = 30.0;
            else lbl.FontSize = 30.0 / (Content.Length / 15);
            lbl.Foreground = (Brush)Application.Current.FindResource("TextColor");
            return lbl;
        }

        public Border CreateDefaultUnderBorder()
        {
            Border bd = new Border();
            bd.Height = 10;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Background = (Brush)Application.Current.FindResource("UnderBorderColor");
            bd.BorderThickness = new Thickness(2);
            bd.CornerRadius = new CornerRadius(5);
            return bd;
        }

        public StackPanel GetPanel()
        {
            StackPanel Main = new StackPanel();
            Main.Children.Add(Title);
            Main.Children.Add(AnswerGrid);
            Main.Children.Add(UnderBorder);
            return Main;
        }

    }
}
