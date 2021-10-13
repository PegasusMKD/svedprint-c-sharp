using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Frontend.NewFrontEnd.GraphicsPole
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

        public void SetAnswerBox(object AnswerBox)
        {
            AnswerGrid = (UIElement)AnswerBox;
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
            bd.Background = (Brush)new BrushConverter().ConvertFromString("#FFED6A3E");//(Brush)Application.Current.FindResource("MenuItemColor");
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
            lbl.Foreground = (Brush)new BrushConverter().ConvertFromString("#fff");//(Brush)Application.Current.FindResource("TextColor");
            return lbl;
        }
        public static Border CreateDefaultUnderBorder()
        {
            Border bd = new Border();
            bd.Height = 10;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Background = (Brush)new BrushConverter().ConvertFromString("#fff");//(Brush)Application.Current.FindResource("UnderBorderColor");
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
}
