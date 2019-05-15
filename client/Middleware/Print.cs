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
        static string tmpFolder = Path.GetTempPath() + @"pics\";
        public static void PrintSveditelstva(List<Ucenik> ucenici, Klasen klasen, int printerChoice)
        {
            List<string> data = InitSveditelstvo(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);
            pd.OriginAtMargins = false;

            data.Insert(0, "sveditelstva"); // mozno e da e "sveditelstva", "sveditelstvo"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + @"\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            if (pd.PrinterSettings.CanDuplex)
            {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }

            int page = 0;

            for (int i = 0; i < data.Count - 1; i++)
            {
                if (pd.PrinterSettings.CanDuplex)
                {
                    pd.PrintPage += (sender, args) =>
                    {
                        if (page % 2 == 0)
                        {
                            args.Graphics.DrawImage(
                                System.Drawing.Image.FromFile($"{tmpFolder}front-{i}.jpg"),
                                args.PageBounds);
                            pd.DocumentName = $"{tmpFolder}front-{i}.jpg";
                            System.Diagnostics.Debug.WriteLine(pd.DocumentName);
                            args.HasMorePages = true;
                        }
                        else
                        {
                            args.HasMorePages = false;
                            args.Graphics.DrawImage(
                                System.Drawing.Image.FromFile($"{tmpFolder}back-{i}.jpg"),
                                args.PageBounds);
                            pd.DocumentName = $"{tmpFolder}back-{i}.jpg";
                            System.Diagnostics.Debug.WriteLine(pd.DocumentName);
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
                            $"{tmpFolder}{(page % 2 == 0 ? "front" : "back")}-{i}.jpg"),
                            args.PageBounds);

                        pd.DocumentName = $"{tmpFolder}{(page % 2 == 0 ? "front" : "back")}-{i}.jpg";
                        page++;
                    };
                    pd.Print();
                    MessageBox.Show("Ве молиме свртете го листот.");
                    pd.Print();
                }
            }

            //ClearTmpFolder();
        }

        public static void PreviewSveditelstvo(Ucenik u, Klasen k)
        {
            List<string> data = InitSveditelstvo(new List<Ucenik>() { u }, k);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"sveditelstvo\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            File.Move($"{tmpFolder}front-0.jpg", $"{tmpFolder}front-prev.jpg");
            File.Move($"{tmpFolder}back-0.jpg", $"{tmpFolder}back-prev.jpg");
        }

        public static void ClearTmpFolder()
        {
            Directory.Delete(tmpFolder, true);
        }

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
                sw.Write(u._tatko + " " + u._prezime);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje);
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo); // hardcoded drzavjanstvo
                sw.Write(delimiter);

                // momentalna i sledna ucebna godina, po koj pat ja uci godinata
                sw.Write(klasen._godina.ToString());
                sw.Write(delimiter);
                sw.Write((klasen._godina + 1).ToString());
                sw.Write(delimiter);
                sw.Write(u._pat_polaga.ToString());
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

            data.Insert(0, "\"glavna\""); // mozno e da e "sveditelstva"
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
                while (tmparr.Count < 17)
                {
                    tmparr.Add("NaN");
                }
                tmparr.AddRange(u._proektni.Split(' '));
                while (tmparr.Count < 22)
                {
                    tmparr.Add("NaN");
                }
                sw.Write("\"" + String.Join("/", tmparr) + "\"");
                sw.Write(";");

                // oceni
                tmparr.Clear();
                tmparr.AddRange(u._oceni.ConvertAll(x => x.ToString()));
                while (tmparr.Count < 17)
                {
                    tmparr.Add("NaN");
                }
                tmparr.AddRange(u._polozhil.Split(' '));
                while (tmparr.Count < 22)
                {
                    tmparr.Add("NaN");
                }
                sw.Write("\"" + String.Join(" ", tmparr) + "\"");
                sw.Write(";");

                // delovoden broj, godina (klas), paralelka, broj vo dnevnik
                sw.Write("\"");
                sw.Write(klasen._paralelka.Replace('-', delimiter[0]));
                sw.Write(delimiter);
                sw.Write(u._broj);
                sw.Write(delimiter);

                // ime prezime na ucenik, ime na tatko, ime na majka, DOB, mesto na raganje, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
                sw.Write(u._ime + " " + u._prezime);
                sw.Write(delimiter);
                sw.Write(u._tatko);
                sw.Write(delimiter);
                sw.Write(u._majka);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje); // mesto na ragjanje
                sw.Write(delimiter);
                sw.Write(u._mesto_na_zhiveenje);
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo); // hardcoded drzavjanstvo
                sw.Write(delimiter);

                // prethodna godina (oddelenie), uspeh od prethodna godina, prethodna ucebna godina, prethodno uciliste, pat
                sw.Write(u._prethodna_godina);
                sw.Write(delimiter);
                sw.Write(u._prethoden_uspeh);
                sw.Write(delimiter);
                sw.Write(u._prethodna_uchebna);
                sw.Write(delimiter);
                sw.Write(u._prethodno_uchilishte);
                sw.Write(delimiter);
                sw.Write(u._pat_polaga);
                sw.Write(delimiter);

                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(u._smer);
                sw.Write(delimiter);
                sw.Write(u._povedenie);
                sw.Write(delimiter);
                sw.Write(u._opravdani);
                sw.Write(delimiter);
                sw.Write(u._neopravdani);
                sw.Write(delimiter);

                //Koja e celta na ovaa godina? ne bi bilo isto so Split-ot odma pod nego?
                sw.Write(klasen._godina);
                sw.Write(delimiter);
                sw.Write(klasen._paralelka.Split('-')[0]);
                sw.Write(delimiter);
                if (klasen._srednoIme != "")
                {
                    sw.Write($"{klasen._ime} {klasen._srednoIme}-{klasen._prezime}");
                }
                else
                {
                    sw.Write($"{klasen._ime} {klasen._prezime}");
                }
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                sw.Write(u._pedagoshki_merki.Split(','));
                sw.Write(delimiter);
                sw.Write(u._delovoden_broj);
                //sw.Write(nekoja vrednost);
                //sw.Write(delimiter);
                //sw.Write(nekoja vrednost);
                //sw.Write(delimiter);
                sw.Write(u._smer); // zoso smer?
                sw.Write(delimiter);
                sw.Write(u._datum_sveditelstvo);


                sw.Write("\"");

                l.Add(sw.ToString());
            }
            return l;
        }
        public static void PrintGkDiploma(List<Ucenik> ucenici, Klasen klasen, int printerChoice)
        {
            List<string> data = InitGkDiploma(ucenici, klasen);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Landscape = true;

            data.Insert(0, "\"dipl\""); // mozno e da e "sveditelstva"
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
        public static List<string> InitGkDiploma(List<Ucenik> ucenici, Klasen klasen)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = ",";
            foreach (Ucenik u in ucenici)
            {
                sw.GetStringBuilder().Clear();

                // predmeti
                sw.Write("\"" + String.Join("/", u._maturska.Split(',').Select(x => x.Split(':')[0])) + "\"");
                sw.Write(";");

                // oceni
                sw.Write("\"" + String.Join(" ", u._maturska.Split(',').Select(x => x.Split(':')[1])) + "\"");
                sw.Write(";");

                // delovoden broj,reden broj(?), Ime na uchenik, Prezime na Uchenik, Datum na ragjanje, Mesto na ragjanje, opshtina, drzhava, drzhavjanstvo
                sw.Write("\"");
                sw.Write(u._delovoden_broj);
                sw.Write(delimiter);
                sw.Write(u._broj); // hardcoded
                sw.Write(delimiter);
                sw.Write(u._ime);
                sw.Write(delimiter);
                sw.Write(u._prezime);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje); // hardcoded
                sw.Write(delimiter);
                sw.Write(u._mesto_na_zhiveenje);
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo);
                sw.Write(delimiter);

                // Ime i prezime na staratel, ispiten rok, po koj pat polaga, kakov tip obrazovanie, koj smer, //////////, delovoden broj na prethodno sveditelstvo (?)
                sw.Write($"{u._tatko} {u._prezime}");
                sw.Write(delimiter);
                sw.Write(u._ispiten); // hardcoded
                sw.Write(delimiter);
                sw.Write(u._pat_polaga); // hardcoded //
                sw.Write(delimiter);
                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(u._smer);
                sw.Write(delimiter);
                sw.Write("//////////"); // hardcoded
                sw.Write(delimiter);
                sw.Write(u._prethoden_delovoden); // hardcoded
                sw.Write(delimiter);

                // opsht uspeh, uchebna godina(nezz dali segashna ili prethodna)(prethodna treba), klasen, direktor
                sw.Write(string.Format("{0:N2}", u._oceni.Average())); // testing
                sw.Write(delimiter);
                sw.Write(u._prethodna_godina);
                sw.Write(delimiter);
                if (klasen._srednoIme != "")
                {
                    sw.Write($"{klasen._ime} {klasen._srednoIme}-{klasen._prezime}");
                }
                else
                {
                    sw.Write($"{klasen._ime} {klasen._prezime}");
                }
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write("\"");
                sw.Write(";");

                // datum na polaganje
                sw.Write("\"" + String.Join("/", u._maturska.Split(',').Select(x => x.Split(':')[3])) + "\"");
                sw.Write(";");

                // delovoden broj
                sw.Write("\"" + String.Join("/", u._maturska.Split(',').Select(x => x.Split(':')[4])) + "\"");
                sw.Write(";");

                // percentile
                sw.Write("\"" + String.Join("/", u._maturska.Split(',').Select(x => x.Split(':')[2])) + "\"");

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
