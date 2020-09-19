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
}
