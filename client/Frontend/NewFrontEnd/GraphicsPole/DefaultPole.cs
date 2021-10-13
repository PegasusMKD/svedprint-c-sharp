using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.GraphicsPole
{
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
            tx.Foreground = (Brush)new BrushConverter().ConvertFromString("#ffffff");//(Brush)Application.Current.FindResource("TextColor");
            tx.HorizontalAlignment = HorizontalAlignment.Stretch;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.Margin = new Thickness(5);

            if (Binding != null)
            {
                Binding myBind = new Binding();
                myBind.Path = new PropertyPath(Binding);
                if (Model_object != null) myBind.Source = Model_object;
                myBind.Mode = BindingMode.TwoWay;
                myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                tx.SetBinding(TextBox.TextProperty, myBind);
            }

            //Binding binding = new Binding((string)Model_object) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Mode = BindingMode.TwoWay };
            //tx.SetBinding(TextBox.TextProperty, binding);
            if (tx.Text == "") tx.Text = Text;
            tx.LostFocus += AnswerBox_LostFocus;
            //Answer = tx.Text;
            tx.Tag = i;
            //AnswerPoleObject = tx;
            return tx;
        }

        private void AnswerBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var txt = (TextBox)sender;
            Console.WriteLine("Box {0} updated", txt.Tag);
        }

        public UIElement GetDesign()
        {
            return DesignPole.GetDesign();
        }
    }
}
