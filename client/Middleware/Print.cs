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
        public static void PrintSveditelstvo(List<Ucenik> ucenici, Klasen klasen, int printerChoice)
        {
            List<string> data = InitSveditelstvo(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            string tmpFolder = Path.GetTempPath()+@"pics\";
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);
            pd.OriginAtMargins = false;

            data.Insert(0, "sveditelstva"); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();
            System.Diagnostics.Debug.WriteLine(outparam);

            if (pd.PrinterSettings.CanDuplex)
            {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }

            for (int i = 0; i < data.Count-1; i++)
            {
                int page = 0;

                if (pd.PrinterSettings.CanDuplex)
                {
                    pd.PrintPage += (sender, args) =>
                    {
                        if (page % 2 == 0)
                        {
                            args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\front-{0}.jpg", i)), args.PageBounds);
                            pd.DocumentName = String.Format("{0}\\front-{1}.jpg", tmpFolder, i);
                            args.HasMorePages = true;
                        }
                        else
                        {
                            args.HasMorePages = false;
                            args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\back-{0}.jpg", i)), args.PageBounds);
                            pd.DocumentName = String.Format("{0}\\back-{1}.jpg", tmpFolder, i);
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
                        pd.DocumentName = String.Format("{2}\\{0}-{1}.jpg", (page % 2 == 0 ? "front" : "back"), i, tmpFolder);
                        page++;
                    };
                    pd.Print();
                    MessageBox.Show("Ве молиме свртете го листот.");
                    pd.Print();
                }
            }

            Directory.Delete(tmpFolder, true);
        }

        //public static string InitSveditelstvoPreview(Ucenik u, Klasen klasen)
        //{
        //    StringWriter sw = new StringWriter();
        //    List<string> l = new List<string>();
        //    string delimiter = ",";
        //    sw.GetStringBuilder().Clear();

        //        // predmeti
        //        sw.Write("\"" + String.Join("/", u._s._predmeti) + "\"");
        //        sw.Write(";");

        //        // oceni
        //        sw.Write("\"" + String.Join(" ", u._oceni) + "\"");
        //        sw.Write(";");

        //        // uchilishte, grad, broj glavna kniga, godina (klas)
        //        sw.Write("\"");
        //        sw.Write(klasen._uchilishte);
        //        sw.Write(delimiter);
        //        sw.Write(klasen._grad);
        //        sw.Write(delimiter);
        //        sw.Write(delimiter); // broj glavna kniga
        //        sw.Write(klasen._paralelka.Split('-').FirstOrDefault());
        //        sw.Write(delimiter);

        //        // ime prezime na ucenik, ime prezime na roditel, DOB, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
        //        sw.Write(u._ime + " " + u._prezime);
        //        sw.Write(delimiter);
        //        sw.Write(u._tatkovo + " " + u._prezime);
        //        sw.Write(delimiter);
        //        sw.Write(u._roden);
        //        sw.Write(delimiter);
        //        sw.Write(u._mesto);
        //        sw.Write(delimiter);
        //        sw.Write("Македонец"); // hardcoded drzavjanstvo
        //        sw.Write(delimiter);

        //        // momentalna i sledna ucebna godina, po koj pat ja uci godinata
        //        sw.Write(klasen._godina.ToString());
        //        sw.Write(delimiter);
        //        sw.Write((klasen._godina + 1).ToString());
        //        sw.Write(delimiter);
        //        sw.Write(u._pat.ToString());
        //        sw.Write(delimiter);

        //        // paralelka, povedenie, opravdani, neopravdani, tip, smer
        //        sw.Write(klasen._paralelka);
        //        sw.Write(delimiter);
        //        sw.Write(u._povedenie);
        //        sw.Write(delimiter);
        //        sw.Write(u._opravdani.ToString());
        //        sw.Write(delimiter);
        //        sw.Write(u._neopravdani.ToString());
        //        sw.Write(delimiter);
        //        sw.Write(u._tip);
        //        sw.Write(delimiter);
        //        //sw.Write(nekoja vrednost);
        //        sw.Write(delimiter);
        //        //sw.Write(nekoja vrednost);
        //        sw.Write(delimiter);
        //        sw.Write(u._smer);
        //        sw.Write(delimiter);

        //        // XX, YY
        //        sw.Write(delimiter); // XX
        //        sw.Write(""); // YY

        //        sw.Write("\"");

        //        l.Add(sw.ToString());
        //        return "l";
        //}

        public static List<string> InitSveditelstvo(List<Ucenik> ucenici, Klasen klasen)
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
                //sw.Write(nekoja vrednost);
                sw.Write(delimiter);
                //sw.Write(nekoja vrednost);
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
        public static void PrintGlavnaKniga(List<Ucenik> ucenici, Klasen klasen, int printerChoice)
        {
            List<string> data = InitGlavnaKniga(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;

            data.Insert(0, "glavna"); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            for (int i = 0; i < data.Count; i++)
            {
                pd.PrintPage += (sender, args) =>
                {
                    args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\test-{0}.jpg", i)), args.PageBounds);
                    pd.DocumentName = String.Format("{0}\\test-{1}.jpg", rootFolder, i);
                };
                pd.Print();
            }
        }
        public static List<string> InitGlavnaKniga(List<Ucenik> ucenici, Klasen klasen)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = ",";
            foreach (Ucenik u in ucenici)
            {
                sw.GetStringBuilder().Clear();
                List<string> tmparr = new List<string>();

                // predmeti
                tmparr.Clear();
                tmparr.AddRange(u._s._predmeti);
                while(tmparr.Count < 17)
                {
                    tmparr.Add("NaN");
                }
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
                //sw.Write(nekoja vrednost);
                //sw.Write(delimiter);
                //sw.Write(nekoja vrednost);
                //sw.Write(delimiter);
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
        public static void PrintDiploma(List<Ucenik> ucenici, Klasen klasen, int printerChoice)
        {
            List<string> data = InitDiploma(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Landscape = true;

            data.Insert(0, "dipl"); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);
            System.Diagnostics.Debug.WriteLine(outparam);
            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam.ToString();
            py.StartInfo.CreateNoWindow = false;
            py.Start();
            py.WaitForExit();

            for (int i = 0; i < data.Count; i++)
            {
                pd.PrintPage += (sender, args) =>
                {
                    args.Graphics.DrawImage(System.Drawing.Image.FromFile(String.Format(".\\dipl-{0}.jpg", i)), args.PageBounds);
                    pd.DocumentName = String.Format("{0}\\dipl-{1}.jpg", rootFolder, i);
                };
                pd.Print();
            }
        }
        public static List<string> InitDiploma(List<Ucenik> ucenici, Klasen klasen)
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
