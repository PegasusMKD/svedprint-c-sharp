using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Middleware
{
    class PrintQueueItem
    {
        public System.Drawing.Image[] sides;
    }
    public class Print
    {
        static string tmpFolder = Path.GetTempPath() + @"pics\";
        static List<PrintQueueItem> printQueue;
        static int currentPage;
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
            pd.DefaultPageSettings.Landscape = false;

            data.Insert(0, "\"sveditelstva\""); // mozno e da e "sveditelstva", "sveditelstvo"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + @"\print.exe";

            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            for (int i = 0; i < data.Count - 1; i++)
            {
                PrintQueueItem x = new PrintQueueItem();
                x.sides = new System.Drawing.Image[2];
                x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}front-{i}.jpg").AbsolutePath);
                x.sides[1] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}back-{i}.jpg").AbsolutePath);

                printQueue.Add(x);
            }

            if (pd.PrinterSettings.CanDuplex)
            {
                pd.PrinterSettings.Duplex = Duplex.Vertical;
            }

            currentPage = 0;
            maxSides = 2;
            pd.PrintPage += new PrintPageEventHandler(onPrintPage);

            for (currentPage = 0; currentPage < data.Count - 1; currentPage++)
            {
                currentSide = 0;
                pd.Print();

            }

            //foreach(var x in printQueue)
            //{
            //    foreach(var k in x.sides)
            //    {
            //        k.Dispose();
            //    }
            //}

            printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
            pd.Dispose();
            // ClearTmpFolder();
        }

        static int currentSide;
        static int maxSides;

        private static void onPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(printQueue[currentPage].sides[currentSide], e.PageBounds);
            currentSide++;
            e.HasMorePages = (currentSide % maxSides != 0);
            g.Dispose();
        }

        public static void PreviewSveditelstvo(Ucenik u, Klasen k)
        {
            List<string> data = InitSveditelstvo(new List<Ucenik>() { u }, k);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"sveditelstvo\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            ClearTmpFolder();

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
        public static void PreviewGlavnaKniga(Ucenik u, Klasen k)
        {
            List<string> data = InitGlavnaKniga(new List<Ucenik>() { u }, k);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"glavna\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            ClearTmpFolder();

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            File.Move($"{tmpFolder}test-0.jpg", $"{tmpFolder}prev.jpg");
        }
        public static void PreviewGkDiploma(Ucenik u, Klasen k)
        {
            List<string> data = InitGkDiploma(new List<Ucenik>() { u }, k);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"dipl\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            ClearTmpFolder();

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            File.Move($"{tmpFolder}dipl-0.jpg", $"{tmpFolder}prev.jpg");
        }
        public static void ClearTmpFolder()
        {

            Directory.Delete(tmpFolder, true);
        }

        static readonly Dictionary<string, string> year_dictionary = new Dictionary<string, string>(){
            {"I","1"},
            {"II","2"},
            {"III","3"},
            {"IV","4"}
        };

        public static List<string> InitSveditelstvo(List<Ucenik> ucenici, Klasen klasen)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = ",";
            int ctr_passable = 0;
            foreach (Ucenik u in ucenici)
            {
                //Addition of Pazzio
                if (!u.CheckPass()) // really?
                {
                    Console.Write("Не положил или фали оценка!");
                    continue;
                }
                sw.GetStringBuilder().Clear();

                sw.Write("\"" + String.Join("/", klasen._p._smerovi[u._smer]._predmeti) + "\"");
                sw.Write(";");
                // oceni
                sw.Write("\"" + String.Join(" ", u._oceni) + "\"");
                sw.Write(";");

                // uchilishte, grad, broj glavna kniga, godina (klas)
                sw.Write("\"");
                sw.Write(klasen._ucilishte);
                sw.Write(delimiter);
                sw.Write(klasen._grad);
                sw.Write(delimiter);

                var paralelka_godina = klasen._paralelka.Split('-').FirstOrDefault();
                sw.Write(klasen._glavna_kniga + '/' + year_dictionary[paralelka_godina]);// <----
                sw.Write(delimiter); // broj glavna kniga
                sw.Write(paralelka_godina);
                sw.Write(delimiter);

                // ime prezime na ucenik, ime prezime na roditel, DOB, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
                sw.Write(u._ime + " " + u._prezime);
                sw.Write(delimiter);
                sw.Write(u._tatko);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write($",{u._mesto_na_ragjanje},{klasen._drzava}");
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo);
                sw.Write(delimiter);

                // momentalna i sledna ucebna godina, po koj pat ja uci godinata
                sw.Write(klasen._godina.ToString());// <-----
                sw.Write(delimiter);
                sw.Write((klasen._godina + 1).ToString());
                sw.Write(delimiter);
                sw.Write(u._pat_polaga.ToString());
                sw.Write(delimiter);

                // paralelka, povedenie, opravdani, neopravdani, tip, smer
                sw.Write(klasen._paralelka.Replace('-', ' '));
                sw.Write(delimiter);
                sw.Write(u._povedenie);
                sw.Write(delimiter);
                sw.Write(u._opravdani.ToString());
                sw.Write(delimiter);
                sw.Write(u._neopravdani.ToString());
                sw.Write(delimiter);
                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(klasen._p._smerovi[u._smer]._cel_smer);
                sw.Write(delimiter);

                //Dodavano od Pazzio
                //Ovdeka treba nekoe mesto, ama neznam koe, treba da prashash
                //Vo shkolo
                //sw.Write(u._
                sw.Write(klasen._grad);
                sw.Write(delimiter);
                sw.Write(klasen._odobreno_sveditelstvo);
                sw.Write(delimiter);
                //sw.Write(klasen._delovoden_broj + '-' + year_dictionary[paralelka_godina] + '/' + klasen._paralelka.Split('-')[1] + '/' + ctr_passable.ToString());

                sw.Write(delimiter);
                sw.Write(klasen._ime + (klasen._srednoIme != "" ? " " + klasen._srednoIme : "") + " " + klasen._prezime);
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                sw.Write(klasen._akt);
                sw.Write(delimiter);
                sw.Write(klasen._akt_godina);
                sw.Write(delimiter);
                sw.Write(klasen._ministerstvo);
                sw.Write(';');

                List<string> proektni_list = new List<string>();
                string[] v = u._proektni.Split(';');

                foreach (var x in v)
                {
                    //proektni_list.Add(String.Join("/",x.Split(','))); // zoso kompliciranje
                    proektni_list.Add(x.Replace(',', '/'));

                }

                sw.Write(String.Join(",", proektni_list));


                ctr_passable++;

                sw.Write("\"");

                int offsetx = 0, offsety = 0;
                sw.Write($";\"{offsetx},{offsety}\"");

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
            printQueue = new List<PrintQueueItem>();

            for (int i = 0; i < data.Count - 1; i++)
            {
                PrintQueueItem x = new PrintQueueItem();
                x.sides = new System.Drawing.Image[1];
                x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}gk-{i}.jpg").AbsolutePath);

                printQueue.Add(x);
            }

            currentPage = 0;
            maxSides = 1;
            pd.PrintPage += new PrintPageEventHandler(onPrintPage);

            for (currentPage = 0; currentPage < data.Count - 1; currentPage++)
            {
                currentSide = 0;
                pd.Print();

            }
        }
        public static List<string> InitGlavnaKniga(List<Ucenik> ucenici, Klasen klasen)
        {
            // https://raw.githubusercontent.com/darijan2002/ps/ps/gk/sample_params.txt?token=ADAAZQMNTPXEEBZ7LCUYXD245FMAC
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = ",";
            foreach (Ucenik u in ucenici)
            {
                sw.GetStringBuilder().Clear();
                List<string> tmparr = new List<string>();

                // predmeti
                tmparr.Clear();
                tmparr.AddRange(klasen._p._smerovi[u._smer]._predmeti);
                while (tmparr.Count < 17)
                {
                    tmparr.Add("NaN");
                }
                tmparr.AddRange(u._proektni.Split(';').ToList().ConvertAll(x => x.Split(',')[0]));
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
                var tmppoloz = u._proektni.Split(';').ToList().ConvertAll(x => x.Split(',')[1].ToLower());
                tmppoloz = tmppoloz.ConvertAll(x =>
                {
                    if (x == "реализирал")
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                });
                tmparr.AddRange(tmppoloz);
                while (tmparr.Count < 22)
                {
                    tmparr.Add("NaN");
                }
                sw.Write("\"" + String.Join(" ", tmparr) + "\"");
                sw.Write(";");

                // delovoden broj, godina (klas), paralelka, broj vo dnevnik
                sw.Write("\"");
                //sw.Write(klasen._delovoden_broj);
                sw.Write(delimiter);
                sw.Write(klasen._paralelka.Replace('-', delimiter[0]));
                sw.Write(delimiter);
                sw.Write(u._broj);
                sw.Write(delimiter);

                // ime prezime na ucenik, ime na tatko, ime na majka, DOB, mesto na raganje, naselba, opshtina, drzhava, drzhavjanstvo (hardcode)
                sw.Write(u._ime);
                sw.Write(delimiter);
                sw.Write(u._prezime);
                sw.Write(delimiter);
                sw.Write(u._tatko);
                sw.Write(delimiter);
                sw.Write(u._majka);
                sw.Write(delimiter);
                sw.Write(u._roden); // <------
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje); // mesto na ragjanje
                sw.Write(delimiter);
                sw.Write(u._mesto_na_zhiveenje);
                sw.Write(delimiter);
                sw.Write(klasen._drzava); // <------
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo); // hardcoded drzavjanstvo
                sw.Write(delimiter);

                var rimsko = klasen._paralelka.Split('-')[0];
                int idx = rimskoDict.Keys.ToList().IndexOf(rimsko);

                // prethodna godina (oddelenie), uspeh od prethodna godina, prethodna ucebna godina, prethodno uciliste, pat

                sw.Write($"{rimskoDict.Keys.ToArray()[idx - 1]} - {rimskoDict.Values.ToArray()[idx - 1]}");

                sw.Write(delimiter);
                sw.Write(u._prethoden_uspeh);
                sw.Write(delimiter);
                //sw.Write(u._prethodna_uchebna);
                //sw.Write("1990/1991"); HARDCODED
                sw.Write(delimiter);
                sw.Write(u._prethodno_uchilishte);
                sw.Write(delimiter);
                sw.Write(u._pat_polaga);
                sw.Write(delimiter);

                sw.Write(u._tip); // <------
                sw.Write(delimiter);
                sw.Write(klasen._p._smerovi[u._smer]._cel_smer);
                sw.Write(delimiter);
                sw.Write(u._povedenie);
                sw.Write(delimiter);
                sw.Write(u._opravdani);
                sw.Write(delimiter);
                sw.Write(u._neopravdani);
                sw.Write(delimiter);

                //Koja e celta na ovaa godina? ne bi bilo isto so Split-ot odma pod nego?
                //sw.Write(klasen._godina); // ucebna godina. bara kalendarska godina. nesto kako prethodna_uchebna ama ne prethodna tuku segasna
                // workaround

                //sw.Write("1990/1991");
                sw.Write(delimiter);
                //sw.Write("IV - четврта");// <------

                sw.Write($"{rimskoDict.Keys.ToArray()[idx]} - {rimskoDict.Values.ToArray()[idx]}");

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
                sw.Write(u._merki);
                sw.Write(delimiter);
                //sw.Write(u._delovoden_broj);
                //sw.Write("08-7/16/2"); // <----- HARDCODED
                sw.Write(delimiter);
                sw.Write(klasen._odobreno_sveditelstvo);
                //sw.Write("14.06.2019"); // <------ HARDCODED


                sw.Write("\"");

                int offsetx = 0, offsety = 0;
                sw.Write($";\"{offsetx},{offsety}\"");

                l.Add(sw.ToString());
            }
            return l;
        }

        static readonly Dictionary<string, string> rimskoDict = new Dictionary<string, string>() {
            { "9", "деветто" },
            { "I", "прва"},
            { "II", "втора"},
            { "III", "трета"},
            { "IV", "четврта"}
        };

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

            data.Insert(0, "\"dipl\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("?", data);

            string pyscript = rootFolder + @"\print.exe";
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
                    args.Graphics.DrawImage(System.Drawing.Image.FromFile($"{tmpFolder}dipl-{i}.jpg"), args.PageBounds);
                    pd.DocumentName = $"{tmpFolder}dipl-{i}.jpg";
                };
                pd.Print();
            }

            ClearTmpFolder();
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
        public static void PrintCarsav(int printerChoice) // TODO: testing
        {
            Requests.GetCarsav();

            Excel.Application excelApp = new Excel.Application();
            string filepath = Path.Combine(tmpFolder, "excel.xlsx");
            Excel.Workbook file = excelApp.Workbooks.Open(filepath);
            Excel.Worksheet sheet = (Excel.Worksheet)file.Worksheets[1]; // base 1
            sheet.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
            sheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
            sheet.PrintOutEx(Preview: true, ActivePrinter: PrinterSettings.InstalledPrinters[printerChoice]);

            file.Close();
            excelApp.Quit();
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
