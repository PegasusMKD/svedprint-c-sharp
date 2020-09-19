using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        Frame Source_Frame;
        public DesignMenu(List<KeyValuePair<string, Page>> elements,ref Frame Source_Frame)
        {
            Element = new ListView();
            ScrollViewer.SetHorizontalScrollBarVisibility(Element, ScrollBarVisibility.Hidden);
            this.Source_Frame = Source_Frame;


            int i = 0;
            foreach (KeyValuePair<string,Page> element in elements)
            {
                Label lbl = ((Label)new MenuLabel(element.Key).GetModel());
                ((ListView)Element).Items.Add(lbl);
                lbl.MouseLeftButtonDown += ((sender,e) =>  Label_Clicked(sender,e,element.Value));     
                if(i++==0)Source_Frame.Navigate(element.Value);
            }
        }

        private void Label_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e,Page Target)
        {
            Label lbl = ((Label)sender);
            //MessageBox.Show("clicked");
            //Source_Frame.Content = Target;
            Source_Frame.Navigate(Target);
        }
    }

    class MenuLabel : DesignModel
    {
        static int Width = 800;
        static int Fontsize = 40;
        static SolidColorBrush BackgroundColor = new SolidColorBrush(Color.FromRgb(255, 183, 94));

        public MenuLabel(string text)
        {
            Label lbl = new Label();
            lbl.Content = text;
            lbl.Width = Width;
            lbl.FontSize = Fontsize;
            lbl.Background = BackgroundColor;
            Element = lbl;
        }
    }

    class OcenkaBox : DesignModel
    {
        static int Width = 50;
        static int Height = 50;
        static SolidColorBrush Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("DarkBlue"));
        TextBox Ocenka = new TextBox();
        public OcenkaBox(int ocenka,string  Predmet)
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
            Ocenka.Text = ocenka.ToString();
            bd.Child = Ocenka ;

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
        /*
             <StackPanel>
                        <Border Background = "" Width= "50" Height= "50" CornerRadius= "50" HorizontalAlignment= "Center" VerticalAlignment= "Center" Margin= "10,10,10,0" >
                            < TextBox HorizontalAlignment= "Center" Background= "{x:Null}" BorderBrush= "{x:Null}"  FontSize= "30" Foreground= "White" VerticalAlignment= "Center" SelectionBrush= "{x:Null}" OpacityMask= "White" > 5 </ TextBox >
                        </ Border >
                        < Label HorizontalAlignment= "Center" Foreground= "White" VerticalAlignment= "Center" > Предмет </ Label >
                    </ StackPanel >*/
    }
}
