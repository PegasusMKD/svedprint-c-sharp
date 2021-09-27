using MiddlewareRevisited.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Frontend.NewFrontEnd
{
    class DesignModel
    {
        protected UIElement Element;
        public UIElement GetModel()
        {
            return Element;
        }
    }

    class DesignMenu : DesignModel
    {
        private NewOceniFrame childFrame;
        private Frame parentFrame;
        private User currentUser;
        public DesignMenu(List<Student> elements, User User, ref Frame ParentFrame)
        {
            Element = new ListView();
            ScrollViewer.SetHorizontalScrollBarVisibility(Element, ScrollBarVisibility.Hidden);
            this.parentFrame = ParentFrame;
            currentUser = User;

            int i = 0;
            foreach (var element in elements)
            {
                Label lbl = (Label)new MenuLabel($"{element.firstName} {element.lastName}").GetModel();
                ((ListView)Element).Items.Add(lbl);

                lbl.MouseLeftButtonDown += (sender, e) => Label_Clicked(sender, e, element);
            }

            if (elements.Count == 0) return;
            else childFrame = new NewOceniFrame(elements[0], User);
            parentFrame.Navigate(childFrame);
        }

        private void Label_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e, Student student)
        {
            Label lbl = ((Label)sender);
            //MessageBox.Show("clicked");
            //Source_Frame.Content = Target;
            if (childFrame == null) childFrame = new NewOceniFrame(student, currentUser);
            else childFrame.init(student, currentUser);
            parentFrame.Navigate(childFrame);
        }
    }

    class MenuLabel : DesignModel
    {
        static int Width = 800;
        static int Fontsize = 36;
        static SolidColorBrush BackgroundColor = new SolidColorBrush(Color.FromRgb(255, 183, 94));

        public MenuLabel(string text)
        {
            Label lbl = new Label();
            lbl.Content = text;
            lbl.Width = Width;
            lbl.FontSize = Fontsize;
            lbl.Background = BackgroundColor;
            lbl.ToolTip = text;
            Element = lbl;
        }
    }

    class OcenkaBox : DesignModel
    {
        static int Width = 50;
        static int Height = 50;
        static SolidColorBrush Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("DarkBlue"));
        TextBox Ocenka = new TextBox();
        public OcenkaBox(int index, string Predmet, object model)
        {
            StackPanel st = new StackPanel();

            Element = st;
            Border bd = new Border();
            bd.CornerRadius = new CornerRadius(50);
            bd.Height = Height;
            bd.Width = Width;
            bd.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            bd.VerticalAlignment = VerticalAlignment.Center;
            bd.Margin = new Thickness(10, 10, 10, 0);
            bd.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("DarkBlue"));
            st.Children.Add(bd);



            Ocenka.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            Ocenka.VerticalAlignment = VerticalAlignment.Center;
            Ocenka.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("Transparent"));
            Ocenka.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("Transparent"));
            Ocenka.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("White"));
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
            Predmet_lbl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("White"));
            Predmet_lbl.Content = Predmet;

            st.Children.Add(Predmet_lbl);

        }

        public int GetOcenka()
        {
            return short.Parse(Ocenka.Text);//try catch
        }
    }
}
