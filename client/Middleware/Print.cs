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
using System.Windows;

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
            string front = ".\\sveditelstva0.jpg";
            string back = ".\\back0.jpg";
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

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
            pd.OriginAtMargins = false;
            if (pd.PrinterSettings.CanDuplex)
            {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }

            string outparam = String.Join("?", data);

            outparam = "\"Македонски Јазик/ Математичка Анализа / Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\"" + "?" + 
                "\"makedonski jazik/ calculus IV/ Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\"";
            data = new List<string>() {
                "\"Македонски Јазик/ Математичка Анализа / Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\"",
                "\"makedonski jazik/ calculus IV/ Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\""
            };

            string pyscript = rootFolder + "\\main.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.RedirectStandardInput = true;
            py.StartInfo.Arguments = String.Join("?", data);
            py.Start();

            StreamWriter processStdin = py.StandardInput;
            processStdin.AutoFlush = true;
            while (!py.HasExited)
            {
                processStdin.WriteLine("x");
            }
            
            //py.StartInfo.CreateNoWindow = true;

            for(int i = 0; i < data.Count; i++) {
                int page = 0;

                pd.PrintPage += (sender, args) => {
                    if (page%2 == 0)
                    {
                        args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\sveditelstva{0}.jpg", i)), args.PageBounds);
                        pd.DocumentName = String.Format(".\\sveditelstva{0}.jpg", i);
                        args.HasMorePages = true;
                    } else
                    {
                        args.HasMorePages = false;
                        args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\back{0}.jpg", i)), args.PageBounds);
                        pd.DocumentName = String.Format(".\\back{0}.jpg", i);
                    }
                    page++;
                };

                pd.Print();
                
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
                sw.WriteAsync(u._tatkovo + " " + u._prezime);
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

                // XX, YY
                sw.WriteAsync(", "); // XX
                sw.WriteAsync(""); // YY

                l.Add(sw.ToString());
            }
            return l;
        }
    }
}