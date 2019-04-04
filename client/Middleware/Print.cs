using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Controls;
using iText;
using iText.Layout;
using System.IO;
using iText.Kernel.Pdf;
using System.Diagnostics;

namespace Middleware
{
    public class Print
    {
        public static void print2pdf()
        {
            string input = @"C:\Users\darij\Desktop\dio.jpg";
            string output = @"C:\Users\darij\Desktop\print.pdf";

            Document doc = new Document(new PdfDocument(new PdfWriter(output)));
            doc.Add(new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(input)));

            doc.Close();
            return;

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, args) =>
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(@"C:\Users\darij\Desktop\dio.jpg");
                args.Graphics.DrawImage(img, args.PageBounds);
            };
            PrintDialog d = new PrintDialog();
            
            pd.PrinterSettings.PrintFileName = @"C:\Users\darij\Desktop\print123.pdf";
            pd.Print();
        }
        public static void print2prntr()
        {
            string input = @"C:\Users\darij\Desktop\sveditelstvo.jpg";
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, args) => {
                args.Graphics.DrawImage(System.Drawing.Image.FromFile(input), args.PageBounds);
            };
            pd.OriginAtMargins = false;
            //pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters
            
            Console.WriteLine("Choose printer:");
            for(int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, PrinterSettings.InstalledPrinters[i]));
            }
            int choice = int.Parse(Console.ReadLine());
            
            //int choice = 3;
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[choice];
            
            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);

            pd.Print();
        }
    }
}