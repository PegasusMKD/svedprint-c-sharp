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
        public static void print2prntr(List<Ucenik> ucenici, Klasen klasen)
        {
            List<string> data = init(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            string front = rootFolder + "\\front.jpg";
            string back = rootFolder + "\\back.jpg";
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();
            
            pd.OriginAtMargins = false;
            if(pd.PrinterSettings.CanDuplex) {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }
            //pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters

            Console.WriteLine("Choose printer:");
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, PrinterSettings.InstalledPrinters[i]));
            }
            int choice = int.Parse(Console.ReadLine());

            //int choice = 3;
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[choice];
            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);

            string outparam = String.Join("?", data);
            Process py = new Process();
            py.Start(rootFolder + "\\main.exe", outparam);
            for(int i = 0; i < data.Count; i++) {
                py.WaitForInputIdle();
                pd.PrintPage += (sender, args) => {
                    args.Graphics.DrawImage(System.Drawing.Image.FromFile(front), args.PageBounds);
                };
                pd.Print();
                if(!pd.PrinterSettings.CanDuplex) {
                    MessageBox.Show("svrti list");
                }
                pd.PrintPage += (sender, args) => {
                    args.Graphics.DrawImage(System.Drawing.Image.FromFile(back), args.PageBounds);
                };
                pd.Print();
                
                py.StandardInput.WriteLineAsync();
            }
            while(!py.HasExited) {
                py.WaitForInputIdle();
                py.StandardInput.WriteLineAsync();
            }
        }
        public static List<string> init(List<Ucenik> ucenici, Klasen klasen)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            foreach (Ucenik u in ucenici)
            {
                sw.GetStringBuilder().Clear();

                // predmeti
                sw.WriteAsync("\"" + String.Join("/", u._s._predmeti) + "\"");
                sw.WriteAsync(";");

                // oceni
                sw.WriteAsync("\"" + String.Join(" ", u._oceni) + "\"");
                sw.WriteAsync(";");

                // uchilishte, grad, broj glavna kniga, godina (klas)
                sw.WriteAsync("\"");
                sw.WriteAsync(klasen._uchilishte + ", ");
                sw.WriteAsync(klasen._grad + ", ");
                sw.WriteAsync(", "); // broj glavna kniga
                sw.WriteAsync(u._paralelka.Split('-').FirstOrDefault());
                sw.WriteAsync(", ");

                // ime prezime na ucenik, ime prezime na roditel, DOB, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
                sw.WriteAsync(u._ime + " " + u._prezime);
                sw.WriteAsync(", ");
                sw.WriteAsync(u._tatkovo + " " + i._prezime);
                sw.WriteAsync(", ");
                sw.WriteAsync(u._dob);
                sw.WriteAsync(", ");
                sw.WriteAsync(u._mesto);
                sw.WriteAsync(", ");
                sw.WriteAsync("Македонец"); // hardcoded drzavjanstvo
                sw.WriteAsync(", ");

                // momentalna i sledna ucebna godina, po koj pat ja uci godinata
                sw.WriteAsync(klasen._godina.ToString());
                sw.WriteAsync(", ");
                sw.WriteAsync((klasen._godina + 1).ToString());
                sw.WriteAsync(", ");
                sw.WriteAsync(u._pat.ToString());
                sw.WriteAsync(", ");

                // paralelka, povedenie, opravdani, neopravdani, tip, smer
                sw.WriteAsync(klasen._paralelka);
                sw.WriteAsync(", ");
                sw.WriteAsync(u._povedenie);
                sw.WriteAsync(", ");
                sw.WriteAsync(u._opravdani.ToString());
                sw.WriteAsync(", ");
                sw.WriteAsync(u._neopravdani.ToString());
                sw.WriteAsync(", ");

                // XX, mesto datum XX, YY
                sw.WriteAsync(", "); // XX
                sw.WriteAsync(", "); // XX
                sw.WriteAsync(""); // YY

                l.Add(sw.ToString());
            }
            return l;
        }
    }
}