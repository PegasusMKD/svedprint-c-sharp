using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public interface ICustomImageCollection : ICustomImage
    {
        new Image[] generateMainPage(int horizontalMargin, int verticalMargin, bool appending=false);
        new Image[] generatePreview(int horizontalMargin, int verticalMargin);
    }
}
