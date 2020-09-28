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
        (Image singleImage, Image[] duplex) images;
        public void print(Image image, int printerChoice)
        {
            Task.Factory.StartNew(() =>
            {
                PrintDocument printDocument = new PrintDocument();
                images.singleImage = image;
                printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];
                printDocument.PrinterSettings.Duplex = Duplex.Simplex;
                printDocument.PrintPage += new PrintPageEventHandler(onPrintPage);
                printDocument.Print();
                image.Dispose();
                printDocument.Dispose();
            });
            
        }

        public void print(Image[] _images, int printerChoice)
        {
            PrintDocument printDocument = new PrintDocument();
            images.duplex = _images;
            printDocument.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];
            if (!printDocument.PrinterSettings.CanDuplex)
            {
                printDocument.Dispose();
                throw new Exception("The selected printer can't print double sided!");
            }
            printDocument.PrinterSettings.Duplex = Duplex.Horizontal;
            printDocument.PrintPage += new PrintPageEventHandler(onPrintPage);
            printDocument.Print();
            foreach (Image image in _images)
            {
                image.Dispose();
            }
            printDocument.Dispose();

        }

        private void onPrintPage(object sender, PrintPageEventArgs e)
        {
            using(var graphics = e.Graphics)
            {
                if(images.duplex != null)
                {
                    e.HasMorePages = true;
                    foreach (Image image in images.duplex) {
                        graphics.DrawImage(image, e.PageBounds);
                        image.Dispose();
                    }

                }
                else {
                    e.HasMorePages = false;
                    graphics.DrawImage(images.singleImage, e.PageBounds);
                    images.singleImage.Dispose();
                }
                graphics.Dispose();
            }
        }
    }
}
