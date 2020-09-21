using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited
{
    class Helpers
    {
        public static Bitmap GetEmptyImage(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            return bmp;
        }
    }

    public enum Grades
    {
        FAIL = 1,
        BAD = 2,
        GOOD = 3,
        GREAT = 4,
        EXCELLENT = 5
    }

    public static class GradeExtension
    {
        public static String getGradeLabel(this Grades grade)
        {
            switch (grade)
            {
                case Grades.FAIL:
                    return "Не доволен";

                case Grades.BAD:
                    return "Доволен";

                case Grades.GOOD:
                    return "Добар";

                case Grades.GREAT:
                    return "Многу добар";

                case Grades.EXCELLENT:
                    return "Одличен";
            }
            return null;
        }
    }

}
