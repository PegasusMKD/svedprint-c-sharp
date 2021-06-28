using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels.Printer
{
    public class Printer
    {
        Image[] images;
        int page = 0;

        public void Print(Image image, int printerChoice)
        {
            images = new Image[] { (Image)image.Clone() };

            Task.Factory.StartNew(() =>
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];
                printDocument.PrinterSettings.Duplex = Duplex.Simplex;
                printDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
                printDocument.Print();
                image.Dispose();
                printDocument.Dispose();
            });
        }

        public void Print(Image[] _images, int printerChoice)
        {
            images = _images.Select(img => (Image)img.Clone()).ToArray();

            Task.Factory.StartNew(() =>
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];
                //if (!printDocument.PrinterSettings.CanDuplex)
                //{
                //    printDocument.Dispose();
                //    throw new Exception("The selected printer can't print double sided!");
                //}
                printDocument.PrinterSettings.Duplex = Duplex.Horizontal;
                printDocument.PrintPage += new PrintPageEventHandler(OnPrintPage);
                printDocument.Print();
                foreach (Image image in _images)
                {
                    image.Dispose();
                }
                printDocument.Dispose();
            });
        }


        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            using(var graphics = e.Graphics)
            {
                e.HasMorePages = false;
                graphics.DrawImage(images[page], e.PageBounds);
                images[page++].Dispose();

                if (page < images.Length)
                {
                    e.HasMorePages = true;
                    return;
                }

                graphics.Dispose();
            }
        }
    }
}
