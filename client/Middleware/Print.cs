using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Middleware
{
    public class Print
    {
        public static void print2prntr(List<Ucenik> ucenici, Klasen klasen)
        {
            List<string> data = init(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            Console.WriteLine("Choose printer:");
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine(String.Format("{0}: {1}", i, PrinterSettings.InstalledPrinters[i]));
            }
            int choice = int.Parse(Console.ReadLine());

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[choice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);
            pd.OriginAtMargins = false;

            /*List<string> data = new List<string>() {
            //    "\"Македонски Јазик/ Математичка Анализа / Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\"",
            //    "\"makedonski jazik/ calculus IV/ Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\"",
            //     "\"SOCIOLOGIJAAA/ Математичка Анализа / Бизнис и Претприемништво/ ФЗО / Германски Јазик / Програмски Јазици/ Predmet123/ Predmet321/ Predmet221/ Predmet 231\";\"1 2 3 4 5 2 2 3 5 4\";\"Коце Металец, Скопје,,IV, Лука Јовановски,Славка Јовановска,22.09.2000,Скопје,Скопје,Северна Македонија, северно-македонец,19,20,прв,IV 1,Добро,0,0,Гимназиско образование,ПМА,,\""
            //};
            */
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + "\\main.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.RedirectStandardInput = true;
            py.StartInfo.Arguments = outparam;
            py.Start();
            py.WaitForExit();

            if (pd.PrinterSettings.CanDuplex)
            {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }

            //py.StartInfo.CreateNoWindow = true;

            for (int i = 0; i < data.Count; i++)
            {
                int page = 0;

                if (pd.PrinterSettings.CanDuplex)
                {
                    pd.PrintPage += (sender, args) =>
                    {
                        if (page % 2 == 0)
                        {
                            args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\front-{0}.jpg", i)), args.PageBounds);
                            pd.DocumentName = String.Format("{0}\\front-{1}.jpg", rootFolder, i);
                            args.HasMorePages = true;
                        }
                        else
                        {
                            args.HasMorePages = false;
                            args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\back-{0}.jpg", i)), args.PageBounds);
                            pd.DocumentName = String.Format("{0}\\back-{1}.jpg", rootFolder, i);
                        }
                        page++;
                    };
                    pd.Print();
                }
                else
                {
                    pd.PrintPage += (sender, args) =>
                    {
                        args.Graphics.DrawImage(System.Drawing.Image.FromFile(
                            String.Format(".\\{0}-{1}.jpg", (page % 2 == 0 ? "front" : "back"), i)),
                            args.PageBounds);
                        pd.DocumentName = String.Format("{2}\\{0}-{1}.jpg", (page % 2 == 0 ? "front" : "back"), i, rootFolder);
                        page++;
                    };
                    pd.Print();
                    MessageBox.Show("Ве молиме свртете го листот.");
                    pd.Print();
                }
            }
        }
        public static List<string> init(List<Ucenik> ucenici, Klasen klasen)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = ",";
            foreach (Ucenik u in ucenici)
            {
                sw.GetStringBuilder().Clear();

                // predmeti
                sw.Write("\"" + String.Join("/", u._s._predmeti) + "\"");
                sw.Write(";");

                // oceni
                sw.Write("\"" + String.Join(" ", u._oceni) + "\"");
                sw.Write(";");

                // uchilishte, grad, broj glavna kniga, godina (klas)
                sw.Write("\"");
                sw.Write(klasen._uchilishte);
                sw.Write(delimiter);
                sw.Write(klasen._grad);
                sw.Write(delimiter);
                sw.Write(delimiter); // broj glavna kniga
                sw.Write(klasen._paralelka.Split('-').FirstOrDefault());
                sw.Write(delimiter);

                // ime prezime na ucenik, ime prezime na roditel, DOB, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
                sw.Write(u._ime + " " + u._prezime);
                sw.Write(delimiter);
                sw.Write(u._tatkovo + " " + u._prezime);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto);
                sw.Write(delimiter);
                sw.Write("Македонец"); // hardcoded drzavjanstvo
                sw.Write(delimiter);

                // momentalna i sledna ucebna godina, po koj pat ja uci godinata
                sw.Write(klasen._godina.ToString());
                sw.Write(delimiter);
                sw.Write((klasen._godina + 1).ToString());
                sw.Write(delimiter);
                sw.Write(u._pat.ToString());
                sw.Write(delimiter);

                // paralelka, povedenie, opravdani, neopravdani, tip, smer
                sw.Write(klasen._paralelka);
                sw.Write(delimiter);
                sw.Write(u._povedenie);
                sw.Write(delimiter);
                sw.Write(u._opravdani.ToString());
                sw.Write(delimiter);
                sw.Write(u._neopravdani.ToString());
                sw.Write(delimiter);
                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(u._smer);
                sw.Write(delimiter);

                // XX, YY
                sw.Write(delimiter); // XX
                sw.Write(""); // YY

                sw.Write("\"");

                l.Add(sw.ToString());
            }
            return l;
        }
    }
}

/*
for(int i = 0; i < data.Count; i++) {
                int page = 0;

                pd.PrintPage += (sender, args) => {
                    if (page%2 == 0)
                    {
                        args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\front-{0}.jpg", i)), args.PageBounds);
                        pd.DocumentName = String.Format(".\\sveditelstva{0}.jpg", i);
                        args.HasMorePages = false;
                    } else
                    {
                        args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\back-{0}.jpg", i)), args.PageBounds);
                        pd.DocumentName = String.Format(".\\back{0}.jpg", i);
                        args.HasMorePages = false;
                    }
                    page++;
                };

                pd.Print();
                MessageBox.Show("svrti list");
                pd.Print();
            }
*/
