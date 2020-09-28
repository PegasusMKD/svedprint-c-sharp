using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public interface ICustomImage // TODO: Replace this with an abstract class that resembles ReportCardFront
    {
        (int x, int y, string value, Font font, int width, int height)[] data { get; set; }
        void resetData();
        dynamic generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false);
        dynamic generatePreview(int horizontalMargin, int verticalMargin);
        void clearMainPage();
        void clearPreview();
    }
}
