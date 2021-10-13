using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.DesignModel
{
    class OcenkaBox : DesignModel
    {
        private const int Width = 50;
        private const int Height = 50;
        private readonly TextBox Ocenka;
        public OcenkaBox(int index, string Predmet, object model)
        {
            Border bd = new Border
            {
                CornerRadius = new CornerRadius(50),
                Height = Height,
                Width = Width,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 10, 10, 0),
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("DarkBlue")
            };
            StackPanel st = new StackPanel();
            Element = st;
            st.Children.Add(bd);

            Ocenka = new TextBox
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = (SolidColorBrush)new BrushConverter().ConvertFrom("Transparent"),
                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("Transparent"),
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("White"),
                FontSize = 30
            };
            //Ocenka.Text = (model.ToString();
            bd.Child = Ocenka;

            Binding myBind = new Binding
            {
                Path = new PropertyPath(string.Format("grades[{0}]", index)),
                Source = model,
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            Ocenka.SetBinding(TextBox.TextProperty, myBind);

            Label Predmet_lbl = new Label
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("White"),
                Content = Predmet
            };

            st.Children.Add(Predmet_lbl);
        }

        public int OcenkaValue => Convert.ToInt32(Ocenka.Text);
    }
}
