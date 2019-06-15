using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Frontend
{
    class SettingsDesign
    {
        public static TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 0, 0);
            tx.Text = Text;
            return tx;
        }

        public static Border CreateBorder(int BorderHeight, int TopMargin, int MarginLeft_Right, int CornerRadius, String Color)
        {
            Border Border = new Border();
            Border.Height = BorderHeight;
            Border.Margin = new Thickness(MarginLeft_Right, TopMargin, MarginLeft_Right, 0);
            Border.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(Color));
            Border.VerticalAlignment = VerticalAlignment.Center;
            Border.BorderThickness = new Thickness(2);
            Border.CornerRadius = new CornerRadius(CornerRadius);
            return Border;
        }

        public static Label CreateLabel(string LabelContent, int LabelFontSize, String FontStyle)
        {
            Label lb = new Label();
            lb.Content = LabelContent;
            lb.HorizontalAlignment = HorizontalAlignment.Center;
            lb.Foreground = System.Windows.Media.Brushes.White;
            lb.FontSize = LabelFontSize;
            lb.FontFamily = new FontFamily("Arial");
            lb.FontWeight = FontWeights.Bold;
            return lb;
        }

        public static TextBox CreateTextBox(int FontSize)
        {
            TextBox tx = new TextBox();
            tx.FontSize = FontSize;
            tx.Background = System.Windows.Media.Brushes.Transparent;
            tx.Foreground = System.Windows.Media.Brushes.White;
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.SelectionBrush = System.Windows.Media.Brushes.White;
            tx.BorderBrush = System.Windows.Media.Brushes.White;
            return tx;
        }

        public static Border UnderTextBorder()//settings
        {
            return CreateBorder(10, 0, 25, 5, "#FF3D84C6");
        }

        public static ComboBox CreateComboBox(string PoleTag,string[] izbori)
        {
            ComboBox CB = new ComboBox();
            CB.OverridesDefaultStyle = false;
            CB.ItemsSource = izbori;
            CB.Tag = PoleTag;
            CB.Margin = new Thickness(30, 5, 30, 5);
            CB.FontSize = 25;
           // CB.BorderBrush = Brushes.Yellow;
           // CB.Background = Brushes.Yellow;
           // CB.Resources.Add(SystemColors.WindowBrushKey, Brushes.Yellow);
            return CB;
        }
    }
}
