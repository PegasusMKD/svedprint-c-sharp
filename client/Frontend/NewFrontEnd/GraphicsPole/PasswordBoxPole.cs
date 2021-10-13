using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.GraphicsPole
{
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
}
