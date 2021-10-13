using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.DesignModel
{
    class OcenkaBox : DesignModel
    {
        static int Width = 50;
        static int Height = 50;
        static SolidColorBrush Background = (SolidColorBrush)new BrushConverter().ConvertFrom("DarkBlue");
        TextBox Ocenka = new TextBox();
        public OcenkaBox(int index, string Predmet, object model)
        {
            StackPanel st = new StackPanel();

            Element = st;
            Border bd = new Border();
            bd.CornerRadius = new CornerRadius(50);
            bd.Height = Height;
            bd.Width = Width;
            bd.HorizontalAlignment = HorizontalAlignment.Center;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Margin = new Thickness(10, 10, 10, 0);
            bd.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("DarkBlue");
            st.Children.Add(bd);



            Ocenka.HorizontalAlignment = HorizontalAlignment.Center;
            Ocenka.VerticalAlignment = VerticalAlignment.Center;
            Ocenka.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("Transparent");
            Ocenka.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("Transparent");
            Ocenka.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("White");
            Ocenka.FontSize = 30;
            //Ocenka.Text = (model.ToString();
            bd.Child = Ocenka;


            if ("ocenka" != null)
            {
                Binding myBind = new Binding();
                myBind.Path = new PropertyPath(string.Format("grades[{0}]", index));
                myBind.Source = model;
                myBind.Mode = BindingMode.TwoWay;
                myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                Ocenka.SetBinding(TextBox.TextProperty, myBind);
            }

            Label Predmet_lbl = new Label();
            Predmet_lbl.HorizontalAlignment = HorizontalAlignment.Center;
            Predmet_lbl.VerticalAlignment = VerticalAlignment.Center;
            Predmet_lbl.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("White");
            Predmet_lbl.Content = Predmet;

            st.Children.Add(Predmet_lbl);

        }

        public int GetOcenka()
        {
            return short.Parse(Ocenka.Text);//try catch
        }
    }
}
