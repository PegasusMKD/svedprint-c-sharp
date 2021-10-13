using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.SettingsDesign
{
    class SettingsDesign
    {
        public static TextBox ContentTextBox(string Text)
        {
            TextBox tx = CreateTextBox(24);
            tx.HorizontalAlignment = HorizontalAlignment.Left;
            tx.Margin = new Thickness(30, 0, 0, 0);
            tx.Text = Text;
            tx.CaretBrush = Brushes.White; // bel cursor
            return tx;
        }

        public static Border CreateBorder(int BorderHeight, int TopMargin, int MarginLeft_Right, int CornerRadius, string Color)
        {
            Border Border = new Border();
            Border.Height = BorderHeight;
            Border.Margin = new Thickness(MarginLeft_Right, TopMargin, MarginLeft_Right, 0);
            Border.Background = (SolidColorBrush)new BrushConverter().ConvertFrom(Color);
            Border.VerticalAlignment = VerticalAlignment.Center;
            Border.BorderThickness = new Thickness(2);
            Border.CornerRadius = new CornerRadius(CornerRadius);
            return Border;
        }

        public static Label CreateLabel(string LabelContent, int LabelFontSize, string FontStyle)
        {
            Label lb = new Label();
            lb.Content = LabelContent;
            lb.HorizontalAlignment = HorizontalAlignment.Center;
            lb.Foreground = Brushes.White;
            lb.FontSize = LabelFontSize;
            lb.FontFamily = new FontFamily("Arial");
            lb.FontWeight = FontWeights.Bold;
            return lb;
        }

        public static TextBox CreateTextBox(int FontSize)
        {
            TextBox tx = new TextBox();
            tx.FontSize = FontSize;
            tx.Background = Brushes.Transparent;
            tx.Foreground = Brushes.White;
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            tx.BorderThickness = new Thickness(0);
            tx.SelectionBrush = Brushes.White;
            tx.BorderBrush = Brushes.White;
            return tx;
        }

        public static Border UnderTextBorder()//settings
        {
            return CreateBorder(10, 0, 25, 5, "#FF3D84C6");
        }

        public static ComboBox CreateComboBox(string PoleTag, string[] izbori)
        {
            ComboBox CB = new ComboBox();
            CB.OverridesDefaultStyle = false;
            CB.ItemsSource = izbori;
            CB.Tag = PoleTag;
            CB.Margin = new Thickness(30, 5, 30, 5);
            CB.FontSize = 24;
            // CB.BorderBrush = Brushes.Yellow;
            // CB.Background = Brushes.Yellow;
            // CB.Resources.Add(SystemColors.WindowBrushKey, Brushes.Yellow);
            return CB;
        }

        public static Border ContentBorder(string LabelContent) //TitlePole
        {
            Border bd = CreateBorder(50, 20, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 28, "Arial");
            return bd;
        }
    }
}
