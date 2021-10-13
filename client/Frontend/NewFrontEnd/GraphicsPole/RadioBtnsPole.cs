using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.GraphicsPole
{
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
            return design.CreateStackPanelDesign();
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
            design.Answer = radioBtn.Content.ToString();
            Model_object.GetType().GetProperty(Binding).SetValue(Model_object, design.Answer);
            Console.WriteLine(design.Answer);
            //Model_object.GetType().GetProperty(Parametar).SetValue(Model_object, Answer);
        }

        private void AnswerBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = (TextBox)sender;
            Console.WriteLine("Box {0} updated", txt.Tag);
        }


    }
}
