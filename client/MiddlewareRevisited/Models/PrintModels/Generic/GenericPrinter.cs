using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public class GenericPrinter : ICustomImage
    {

        public Image preview { get; set; }
        public Image mainPage { get; set; }
        public Image background { get; set; }
        public string backgroundPath;

        public Student student;
        public User homeroomTeacher;

        public string saveAs; // 
        public (int x, int y, string value, Font font, int width, int height)[] data { get; set; }

        public GenericPrinter(ref Student student, ref User homeroomTeacher, string filename, string saveAs)
        {
            this.student = student;
            this.backgroundPath = filename;
            this.homeroomTeacher = homeroomTeacher;
            this.saveAs = saveAs;
        }

        public void clearBackground()
        {
            this.background.Dispose();
        }

        public void clearPreview()
        {
            clearMainPage();
            this.preview.Dispose();
        }

        public void clearMainPage()
        {
            clearBackground();
            this.mainPage.Dispose();
        }

        public dynamic generateMainPage(int horizontalMargin, int verticalMargin, bool appending = false)
        {
            this.background = Image.FromFile(backgroundPath);
            this.mainPage = Helpers.GetEmptyImage(this.background.Width, this.background.Height);
            using (var graphics = Graphics.FromImage(this.mainPage))
            {
                if (appending) graphics.Clear(Color.Transparent);

                foreach ((int x, int y, string value, Font font, int width, int height) val in this.data)
                {
                    if (val.width == 0 || val.height == 0)
                    {
                        graphics.DrawString(val.value, val.font, Brushes.Black, val.x + horizontalMargin, val.y + verticalMargin);
                    } else
                    {
                        RectangleF rectF1 = new RectangleF(val.x + horizontalMargin, val.y + verticalMargin, val.width, val.height);
                        graphics.DrawString(val.value, val.font, Brushes.Black, rectF1);
                    }
                }
            }
            return this.mainPage;
        }

        public dynamic generatePreview(int horizontalMargin, int verticalMargin)
        {
            generateMainPage(horizontalMargin, verticalMargin, true);
            this.preview = Helpers.GetEmptyImage(2481, 3508);
            using (var canvas = Graphics.FromImage(this.preview))
            {
                canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(this.background, new Rectangle(0, 0, this.preview.Width, this.preview.Height),
                    new Rectangle(0, 0, this.background.Width, this.background.Height), GraphicsUnit.Pixel);
                canvas.DrawImage(this.mainPage, new Rectangle(0, 0, this.preview.Width, this.preview.Height),
                    new Rectangle(0, 0, this.mainPage.Width, this.mainPage.Height), GraphicsUnit.Pixel);
                canvas.Save();
            }
            return this.preview;
        }

        public virtual void resetData()
        {
            throw new NotImplementedException();
        }
    }
}
