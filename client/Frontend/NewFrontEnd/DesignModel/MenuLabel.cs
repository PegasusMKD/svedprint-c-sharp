using System.Windows.Controls;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.DesignModel
{
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
}
