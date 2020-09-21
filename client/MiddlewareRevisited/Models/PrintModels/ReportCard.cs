using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public class ReportCard : ICustomImageCollection
    {
        public (int x, int y, string value, Font font)[] data => throw new NotImplementedException();

        Image[] images = new Image[2];

        ICustomImage[] sides = new ICustomImage[2];

        public ReportCard(ref Student student, ref User homeroomTeacher, string frontFileName, string backFileName,
            string saveAsFront, string saveAsBack)
        {
            sides[0] = new ReportCardFront(ref student, ref homeroomTeacher, frontFileName,saveAsFront);
            sides[1] = new ReportCardBack(ref student, ref homeroomTeacher, backFileName, saveAsBack);
        }

        public Image[] generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false)
        {
            images[0] = sides[0].generateMainPage(horizontalMargin, verticalMargin);
            images[1] = sides[1].generateMainPage(horizontalMargin, verticalMargin);
            return images;
        }

        public Image[] generatePreview(int horizontalMargin, int verticalMargin)
        {
            images[0] = sides[0].generatePreview(horizontalMargin, verticalMargin);
            images[1] = sides[1].generatePreview(horizontalMargin, verticalMargin);
            return images;
        }

        public void resetData()
        {
            sides[0].resetData();
            sides[1].resetData();
        }

        Image ICustomImage.generateMainPage(int horizontalMargin, int verticalMargin, bool appending)
        {
            throw new NotImplementedException();
        }

        Image ICustomImage.generatePreview(int horizontalMargin, int verticalMargin)
        {
            throw new NotImplementedException();
        }
    }
}
