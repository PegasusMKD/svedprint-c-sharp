using System.Windows.Controls;
using System.Windows.Media;

namespace Frontend.NewFrontEnd.DesignModel
{
    class MenuLabel : DesignModel
    {
        private const int width = 800;
        private const int fontSize = 36;
        private readonly SolidColorBrush BackgroundColor = new SolidColorBrush(Color.FromRgb(255, 183, 94));

        public MenuLabel(string text)
        {
            Element = new Label
            {
                Content = text,
                Width = width,
                FontSize = fontSize,
                Background = BackgroundColor,
                ToolTip = text
            };
        }
    }
}
