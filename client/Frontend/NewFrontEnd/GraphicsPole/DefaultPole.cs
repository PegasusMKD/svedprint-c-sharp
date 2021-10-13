using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.GraphicsPole
{
    class DefaultPole
    {
        private readonly Design DesignPole;
        private readonly object modelObject;
        private readonly string Binding;
        public DefaultPole(string Question, string Binding, string Answer = "", object modelObject = null)
        {
            this.modelObject = modelObject;
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
                if (modelObject != null) myBind.Source = modelObject;
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
            return DesignPole.CreateStackPanelDesign();
        }
    }
}
