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
        Student student;
        public (int x, int y, string value, Font font, int width, int height)[] data { get; set; }

        Image[] images = new Image[2];

        ICustomImage[] sides = new GenericPrinter[2];

        public ReportCard(Student student, ref User homeroomTeacher, List<string> filenames)
        {
            this.student = student;
            sides[0] = new ReportCardFront(ref this.student, ref homeroomTeacher, filenames[0]);
            sides[1] = new ReportCardBack(ref this.student, ref homeroomTeacher, filenames[1]);
        }

        public dynamic generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false)
        {
            images[0] = sides[0].generateMainPage(horizontalMargin, verticalMargin);
            images[1] = sides[1].generateMainPage(horizontalMargin, verticalMargin);
            return images;
        }

        public dynamic generatePreview(int horizontalMargin, int verticalMargin)
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

        public void clearMainPage()
        {
            sides[0].clearMainPage();
            sides[1].clearMainPage();
        }

        public void clearPreview()
        {
            sides[0].clearPreview();
            sides[1].clearPreview();
        }
    }
}
