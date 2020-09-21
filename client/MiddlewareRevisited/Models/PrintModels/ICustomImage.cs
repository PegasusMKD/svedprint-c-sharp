using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public interface ICustomImage
    {
        (int x, int y, string value, Font font)[] data { get; }
        void resetData();
        Image generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false);
        Image generatePreview(int horizontalMargin, int verticalMargin);
           
    }
}
