using System.Printing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Media;

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
        public static void PrintSveditelstva(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int printerChoice, int offsetx, int offsety)
        {
            List<string> data = InitSveditelstvo(siteUcenici, ucenici, klasen, offsetx, offsety);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;
            pd.PrinterSettings.Duplex = Duplex.Vertical;

            data.Insert(0, "\"sveditelstva\""); // mozno e da e "sveditelstva", "sveditelstvo"
            string outparam = String.Join("$", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            //return;
            int partition = 3; // test
            for (int part = 0; part < 15; part++)
            {
                if (partition * part > data.Count - 1)
                {
                    break;
                }

                printQueue.Clear();
                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    PrintQueueItem x = new PrintQueueItem();
                    x.sides = new System.Drawing.Image[2];
                    x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}front-{i}.jpg").AbsolutePath);
                    x.sides[1] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}back-{i}.jpg").AbsolutePath);

                    printQueue.Add(x);
                }

                currentPage = 0;
                maxSides = 2;
                pd.PrintPage += new PrintPageEventHandler(onPrintPage);

                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    currentSide = 0;
                    pd.Print();
                    currentPage++;
                }

                printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
            }
            pd.Dispose(); // test
        }

        static int currentSide;
        static int maxSides;

        private static void onPrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            if (currentSide < maxSides)
            {
                g.DrawImage(printQueue[currentPage].sides[currentSide], e.PageBounds);
                currentSide++;
            }
            e.HasMorePages = currentSide < maxSides;
            g.Dispose();
        }

        public static void PreviewSveditelstvo(List<Ucenik> siteUcenici, Ucenik u, Klasen k)
        {
            List<string> data = InitSveditelstvo(siteUcenici, new List<Ucenik>() { u }, k, 0, 0);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"sveditelstvo\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("$", data);

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
        public static void PreviewGlavnaKniga(List<Ucenik> siteUcenici, Ucenik u, Klasen k)
        {
            List<string> data = InitGlavnaKniga(siteUcenici, new List<Ucenik>() { u }, k, 0, 0);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"glavna\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("$", data);

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
            List<string> data = new List<string>(); // InitGkDiploma(new List<Ucenik>() { u }, k);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            data.Insert(0, "\"dipl\""); // mozno e da e "sveditelstva"
            string outparam = String.Join("$", data);

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
        public static List<string> InitSveditelstvo(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int offsetx, int offsety)
        {
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = "|";

            int n = siteUcenici.Count;
            int[] failed_offset = new int[n];
            bool[] failed_arr = new bool[n];

            // i = 0
            var result = didFail(siteUcenici[0], GetPolozilList(siteUcenici[0]));
            failed_offset[0] = result.offset;
            failed_arr[0] = result.did_fail;

            for (int i = 1; i < n; i++)
            {
                result = didFail(siteUcenici[i], GetPolozilList(siteUcenici[i]));
                failed_arr[i] = result.did_fail;
                failed_offset[i] = failed_offset[i - 1] + result.offset;
            }

            foreach (Ucenik u in ucenici)
            {
                int current_idx = siteUcenici.IndexOf(u);
                ////Addition of Pazzio
                //if (!u.CheckPass())
                //{
                //    Console.Write("Не положил или фали оценка!");
                //    continue;
                //}
                sw.GetStringBuilder().Clear();

                sw.Write("\"" + String.Join("/", klasen._p._smerovi[u._smer].GetCeliPredmeti(u._jazik, u._izborni, klasen._p._smerovi)) + "\"");
                //sw.Write("\"" + String.Join("/", klasen._p._smerovi[u._smer]._predmeti) + "\"");
                sw.Write(";");
                // oceni
                var ocenki = u._oceni;
                if (u._polagal.Count != 0)
                {
                    for (int i = 0; i < u._polagal.Count; i++)
                    {
                        if (u._polagal[i] != 0)
                        {
                            ocenki[i] = u._polagal[i];
                        }
                    }
                }
                sw.Write("\"" + String.Join(" ", ocenki) + "\"");
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
                sw.Write(u._srednoIme);
                sw.Write(delimiter);
                sw.Write(u._roden.Replace(',', '.'));
                sw.Write(delimiter);
                sw.Write($"{delimiter}{u._mesto_na_ragjanje}{delimiter}{klasen._drzava}"); // opstina
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo);
                sw.Write(delimiter);

                // momentalna i sledna ucebna godina, po koj pat ja uci godinata
                sw.Write(klasen._godina.ToString());// <-----
                sw.Write(delimiter);
                sw.Write((klasen._godina + 1).ToString());
                sw.Write(delimiter);
                sw.Write(u._pat_polaga.Split(' ')[0]); // fix
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
                if (int.Parse(year_dictionary[paralelka_godina]) >= 3)
                {
                    sw.Write(klasen._p._smerovi[u._smer]._cel_smer);

                }
                else
                {
                    sw.Write("//////");
                }
                sw.Write(delimiter);

                //Dodavano od Pazzio
                //Ovdeka treba nekoe mesto, ama neznam koe, treba da prashash
                //Vo shkolo
                //sw.Write(u._
                sw.Write(klasen._grad);
                sw.Write(delimiter);
                //sw.Write(klasen._odobreno_sveditelstvo);
                sw.Write("12.06.2019");
                sw.Write(delimiter);
                //sw.Write(klasen._delovoden_broj + '-' + year_dictionary[paralelka_godina] + '/' + klasen._paralelka.Split('-')[1] + '/' + ctr_passable.ToString());

                string[] db = klasen._delovoden_broj.Split('-');
                string[] paralelka_god = klasen._paralelka.Split('-');
                var val = int.Parse(db[1]) + int.Parse(year_dictionary[paralelka_god[0]]) - 1;

                if (!failed_arr[current_idx])
                {
                    //sw.Write($"{db[0]}-{val.ToString("D2")}/{paralelka_god[1]}/{u._broj - failed_offset[current_idx]}");
                }
                sw.Write("08-05/7/14");
                //sw.Write($"{db[0]}-{val.ToString("D2")}/{paralelka_god[1]}/26");

                sw.Write(delimiter);
                //sw.Write(klasen._ime + (klasen._srednoIme != "" ? $" {klasen._srednoIme}-" : " ") + klasen._prezime);
                sw.Write($"{klasen._ime} {(string.IsNullOrWhiteSpace(klasen._srednoIme) ? "" : $"{klasen._srednoIme}-")}{klasen._prezime}");
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                sw.Write(klasen._akt);
                sw.Write(delimiter);
                sw.Write(klasen._akt_godina);
                sw.Write(delimiter);
                sw.Write(klasen._ministerstvo);
                sw.Write(';');
                Console.WriteLine(u._proektni);
                List<string> proektni_list = new List<string>();
                foreach (var x in u._proektni.Split(';'))
                {
                    //proektni_list.Add(String.Join("/",x.Split(','))); // zoso kompliciranje
                    proektni_list.Add(x.Replace(',', '/'));

                }

                sw.Write(String.Join(delimiter, proektni_list));

                sw.Write("\"");
                sw.Write($";\"{offsetx}{delimiter}{offsety}\"");

                //if (current_idx == 0 ? failed_offset[0] == 0 : failed_offset[current_idx] == failed_offset[current_idx-1]) {
                //if (!failed_arr[current_idx])
                //{
                l.Add(sw.ToString());
                //}
            }
            return l;
        }
        public static void PrintGlavnaKniga(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int printerChoice, int offsetx, int offsety)
        {
            List<string> data = InitGlavnaKniga(siteUcenici, ucenici, klasen, offsetx, offsety);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;

            data.Insert(0, "\"glavna\"");
            string outparam = String.Join("$", data);
            //28.06.2019
            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            //return;
            int partition = 5;
            for (int part = 0; part < 9; part++)
            {
                if (partition * part > data.Count - 1)
                {
                    break;
                }

                printQueue.Clear();
                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    PrintQueueItem x = new PrintQueueItem();
                    x.sides = new System.Drawing.Image[1];
                    x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}gk-{i}.jpg").AbsolutePath);

                    printQueue.Add(x);
                }

                currentPage = 0;
                maxSides = 1;
                pd.PrintPage += new PrintPageEventHandler(onPrintPage);

                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    currentSide = 0;
                    pd.Print();
                    currentPage++;
                }

                printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
                pd.Dispose();
            }
        }



        public static List<string> InitGlavnaKniga(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int offsetx, int offsety)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("mk-MK");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("mk-MK");

            // https://raw.githubusercontent.com/darijan2002/ps/ps/gk/sample_params.txt?token=ADAAZQMNTPXEEBZ7LCUYXD245FMAC
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = "|";

            int n = siteUcenici.Count;
            int[] failed_offset = new int[n];
            bool[] failed_arr = new bool[n];

            // i = 0
            var result = didFail(siteUcenici[0], GetPolozilList(siteUcenici[0]));
            failed_offset[0] = result.offset;
            failed_arr[0] = result.did_fail;

            for (int i = 1; i < n; i++)
            {
                result = didFail(siteUcenici[i], GetPolozilList(siteUcenici[i]));
                failed_arr[i] = result.did_fail;
                failed_offset[i] = failed_offset[i - 1] + result.offset;
            }

            foreach (Ucenik u in ucenici)
            {
                int current_idx = siteUcenici.IndexOf(u);
                sw.GetStringBuilder().Clear();
                List<string> tmparr = new List<string>();

                // predmeti
                tmparr.Clear();
                tmparr.AddRange(klasen._p._smerovi[u._smer].GetCeliPredmeti(u._jazik, u._izborni, klasen._p._smerovi));
                while (tmparr.Count < 17)
                {
                    tmparr.Add("NaN");
                }
                tmparr.AddRange(u._proektni.Split(';').ToList().ConvertAll(x => x.Split(',')[0]));
                if (tmparr.Contains("Култура за заштита мир и толеранција") || tmparr.Contains("Култура за заштита"))
                {
                    // vaka e ama so da se prai
                    var xxzx = tmparr.FindIndex(x => x == "Култура за заштита мир и толеранција");
                    if (xxzx == -1)
                    {
                        tmparr.FindIndex(x => x == "Култура за заштита");
                    }

                    tmparr[xxzx] = "Култура за заштита, мир и толеранција";
                }
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
                List<string> tmppoloz = GetPolozilList(u);
                tmparr.AddRange(tmppoloz);
                while (tmparr.Count < 22)
                {
                    tmparr.Add("NaN");
                }
                sw.Write("\"" + String.Join(" ", tmparr) + "\"");
                sw.Write(";");

                // delovoden broj, godina (klas), paralelka, broj vo dnevnik
                sw.Write("\"");
                var god = klasen._paralelka.Split('-').FirstOrDefault();
                sw.Write(klasen._glavna_kniga + '/' + year_dictionary[god]);// <----
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
                if (string.IsNullOrWhiteSpace(u._tatko))
                {
                    sw.Write("// //");
                }
                else
                {
                    sw.Write(u._tatko);
                }

                sw.Write(delimiter);
                if (string.IsNullOrWhiteSpace(u._majka) || u._majka[0] == '/')
                {
                    sw.Write("/// ///");
                }
                else
                {
                    sw.Write(u._majka);
                }

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
                //sw.Write(string.IsNullOrEmpty(u._prethoden_uspeh) || u._prethoden_uspeh == "5.00" ? "" : u._prethoden_uspeh);

                decimal ocena;
                bool worked = decimal.TryParse(u._prethoden_uspeh.ToString(CultureInfo.GetCultureInfo("mk-MK")), out ocena);
                if (!worked)
                {
                    sw.Write(u._prethoden_uspeh);
                }
                else
                {
                    string[] ocena_zbor = new string[] { "", "Недоволен", "Доволен", "Добар", "Многу добар", "Одличен" };
                    int rounded;
                    if (ocena - Math.Floor(ocena) < Convert.ToDecimal(0.5))
                    {
                        rounded = Convert.ToInt32(Math.Floor(ocena));
                    }
                    else
                    {
                        rounded = Convert.ToInt32(Math.Ceiling(ocena));
                    }
                    int rounded2 = Convert.ToInt32(Math.Round(ocena));
                    sw.Write(ocena_zbor[rounded]);
                }

                sw.Write(delimiter);
                sw.Write($"20{klasen._godina - 1}/20{klasen._godina}");
                //sw.Write("1990/1991"); HARDCODED
                sw.Write(delimiter);
                sw.Write(u._prethodno_uchilishte);
                sw.Write(delimiter);
                sw.Write(u._pat_polaga); // fix
                sw.Write(delimiter);

                sw.Write(u._tip); // <------
                sw.Write(delimiter);

                string[] paralelka_godina = klasen._paralelka.Split('-');
                if (int.Parse(year_dictionary[paralelka_godina[0]]) >= 3)
                {
                    sw.Write(klasen._p._smerovi[u._smer]._cel_smer);
                }
                else
                {
                    sw.Write("///");
                }

                sw.Write(delimiter);
                sw.Write(u._povedenie);
                sw.Write(delimiter);
                sw.Write(u._opravdani);
                sw.Write(delimiter);
                sw.Write(u._neopravdani);
                sw.Write(delimiter);

                //Koja e celta na ovaa godina? ne bi bilo isto so Split-ot odma pod nego?
                sw.Write($"20{klasen._godina}/20{klasen._godina + 1}"); // ucebna godina. bara kalendarska godina. nesto kako prethodna_uchebna ama ne prethodna tuku segasna
                // workaround

                //sw.Write("1990/1991");
                sw.Write(delimiter);
                //sw.Write("IV - четврта");// <------

                sw.Write($"{rimskoDict.Keys.ToArray()[idx]} - {rimskoDict.Values.ToArray()[idx]}");

                sw.Write(delimiter);
                sw.Write($"{klasen._ime} {(string.IsNullOrWhiteSpace(klasen._srednoIme) ? "" : $"{klasen._srednoIme}-")}{klasen._prezime}");
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                //sw.Write(u._pedagoski_merki);
                sw.Write(delimiter);
                string[] db = klasen._delovoden_broj.Split('-');
                var val = int.Parse(db[1]) + int.Parse(year_dictionary[paralelka_godina[0]]) - 1;

                /* momentalno
                if (!failed_arr[current_idx])
                {
                    if ((current_idx == 0 && failed_offset[current_idx] == 0) ||
                        (current_idx > 0 && failed_offset[current_idx] == failed_offset[current_idx - 1]))
                    {
                        sw.Write($"{db[0]}-{val.ToString("D2")}/{paralelka_godina[1]}/{u._broj - failed_offset[current_idx]}");
                    }
                } */
                sw.Write("08-05/7/14");
                //sw.Write($"{db[0]}-{val.ToString("D2")}/{paralelka_godina[1]}/26");

                sw.Write(delimiter);
                //sw.Write(klasen._odobreno_sveditelstvo);
                sw.Write("12.06.2019");
                sw.Write(delimiter);
                // BELESKI
                tmparr.Clear();

                sw.Write("\";\"");

                if (u._polagal.Count != 0)
                {
                    for (int i = 0; i < u._oceni.Count; i++)
                    {
                        tmparr.Add("0");
                    }
                    sw.Write(string.Join(" ", tmparr));
                }

                sw.Write("\"");
                sw.Write($";\"{offsetx}{delimiter}{offsety}\"");


                //if (!failed_arr[current_idx])
                //{
                l.Add(sw.ToString());
                //}
            }
            return l;
        }

        private static List<string> GetPolozilList(Ucenik u)
        {
            if (string.IsNullOrWhiteSpace(u._proektni) || u._proektni == ";")
            {
                return new List<string> { "0", "0" };
            }

            if (u._proektni[u._proektni.Length - 1] == ';')
            {
                u._proektni = u._proektni.Remove(u._proektni.Length - 1);
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
            return tmppoloz;
        }

        private static (bool did_fail, int offset) didFail(Ucenik u, List<string> tmppoloz)
        {
            bool did_fail = false;
            int offset = 0;
            if (!u.CheckPass() || !"пз".Contains(u._polozhil.ToLower()[0]) || tmppoloz.Contains("0"))
            {
                offset = 1;
                did_fail = true;
            }
            if (u._oceni.Contains(0))
            {
                // se otpisal
                did_fail = false;
            }
            return (did_fail, offset);
        }

        static readonly Dictionary<string, string> rimskoDict = new Dictionary<string, string>() {
            { "IX", "деветто" },
            { "I", "прва"},
            { "II", "втора"},
            { "III", "трета"},
            { "IV", "четврта"}
        };

        public static void PrintGkDiploma(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int printerChoice, int offsetx, int offsety)
        {
            List<string> data = InitGkDiploma(siteUcenici, ucenici, klasen, offsetx, offsety);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = true;

            data.Insert(0, "\"gk_dipl\"");
            string outparam = String.Join("$", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            //return;
            int partition = 5;
            for (int part = 0; part < 9; part++)
            {
                if (partition * part > data.Count - 1)
                {
                    break;
                }

                printQueue.Clear();
                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    PrintQueueItem x = new PrintQueueItem();
                    x.sides = new System.Drawing.Image[1];
                    x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}gk_dipl-{i}.jpg").AbsolutePath);

                    printQueue.Add(x);
                }

                currentPage = 0;
                maxSides = 1;
                pd.PrintPage += new PrintPageEventHandler(onPrintPage);

                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    currentSide = 0;
                    pd.Print();
                    currentPage++;
                }

                printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
                pd.Dispose();
            }
        }

        public static List<string> InitGkDiploma(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int offsetx, int offsety)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("mk-MK");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("mk-MK");
            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = "|";

            var ToPrint = new List<Ucenik>();

            // nepolagaci
            ToPrint.AddRange(siteUcenici.Where(x => !x._oceni.Contains(1)));

            // polagaci
            ToPrint.AddRange(siteUcenici.Where(x => !ToPrint.Contains(x)));

            string[] paralelka_godina = klasen._paralelka.Split('-');
            string[] db = klasen._delovoden_broj.Split('-');
            var val = int.Parse(db[1]) + int.Parse(year_dictionary[paralelka_godina[0]]) - 1;

            int ctr = 1;
            for (int i = 0; i < ToPrint.Count; i++)
            {
                if (!ToPrint[i]._oceni.Contains(0))
                {
                    ToPrint[i]._delovoden_broj = $"{db[0]}-{val.ToString("D2")}/{paralelka_godina[1]}/{ctr++}";
                }
            }

            ToPrint.Sort(delegate (Ucenik u1, Ucenik u2)
            {
                if (u1._delovoden_broj == "")
                {
                    if (u2._delovoden_broj == "")
                    {
                        return u1._broj.CompareTo(u2._broj);
                    }
                    return 1;
                }
                if (u2._delovoden_broj == "")
                {
                    return -1;
                }
                return int.Parse(u1._delovoden_broj.Split('/')[2]).CompareTo(int.Parse(u2._delovoden_broj.Split('/')[2]));
            });

            foreach (Ucenik u in ToPrint.Where(x => ucenici.ConvertAll(z => z._broj).Contains(x._broj)))
            {
                if (u._polozhil_matura == "не положил") continue;

                sw.GetStringBuilder().Clear();

                // predmeti
                sw.Write("\"" + String.Join("/", u._maturska.Split('&').Select(x => x.Split('|')[0])) + "\"");
                sw.Write(";");

                // oceni
                sw.Write("\"" + String.Join(" ", u._maturska.Split('&').Select(x => x.Split('|')[1])) + "\"");
                sw.Write(";");

                // delovoden broj,reden broj(?), Ime na uchenik, Prezime na Uchenik, Datum na ragjanje, Mesto na ragjanje, opshtina, drzhava, drzhavjanstvo
                sw.Write("\"");
                sw.Write("36/1-2019"); // hardcoded
                sw.Write(delimiter);
                string delbr = delovoden(klasen, u);
                sw.Write(delbr); // mozno e da bide 1,2,3,4,5...
                sw.Write(delimiter);
                sw.Write(u._ime);
                sw.Write(delimiter);
                sw.Write(u._prezime);
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje);
                sw.Write(delimiter); // opstina
                sw.Write(delimiter);
                sw.Write(klasen._drzava);
                sw.Write(delimiter);
                sw.Write(u._drzavjanstvo);
                sw.Write(delimiter);

                // Ime i prezime na staratel, ispiten rok, po koj pat polaga, kakov tip obrazovanie, koj smer, //////////, delovoden broj na prethodno sveditelstvo (?)
                sw.Write($"{(u._srednoIme == u._tatko.Split(' ')[0] ? u._tatko : u._majka)}");
                sw.Write(delimiter);
                sw.Write(u._ispiten);
                sw.Write(delimiter);
                sw.Write(u._pat_polaga_ispit);
                sw.Write(delimiter);
                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(klasen._p._smerovi[u._smer]._cel_smer);
                sw.Write(delimiter);
                sw.Write("//////////"); // hardcoded
                sw.Write(delimiter);
                sw.Write(ToPrint.FirstOrDefault(x => x._broj == u._broj)._delovoden_broj);
                sw.Write(delimiter);
                sw.Write(klasen._odobreno_sveditelstvo);
                sw.Write(delimiter);

                // opsht uspeh, uchebna godina(nezz dali segashna ili prethodna)(prethodna treba), direktor, klasen
                // sw.Write(string.Format("{0:N2}", u._oceni.Average())); // testing

                decimal ocena;
                bool worked = decimal.TryParse(u._oceni.Average().ToString(), out ocena);
                // Convert.ToDecimal(u._oceni.Average());
                if (!worked)
                {
                    sw.Write(u._prethoden_uspeh);
                }
                else
                {
                    string[] ocena_zbor = new string[] { "", "Недоволен", "Доволен", "Добар", "Многу добар", "Одличен" };
                    int rounded;
                    if (ocena - Math.Floor(ocena) < Convert.ToDecimal(0.5))
                    {
                        rounded = Convert.ToInt32(Math.Floor(ocena));
                    }
                    else
                    {
                        rounded = Convert.ToInt32(Math.Ceiling(ocena));
                    }
                    int rounded2 = Convert.ToInt32(Math.Round(ocena));
                    sw.Write(ocena_zbor[rounded]);
                }
                sw.Write(delimiter);
                sw.Write($"20{klasen._godina}/20{klasen._godina + 1}");
                sw.Write(delimiter);
                sw.Write("државната"); // hardcoded
                sw.Write(delimiter);
                sw.Write(u._polozhil_matura);
                sw.Write(delimiter);
                sw.Write("државна"); // hardcoded
                sw.Write(delimiter);
                sw.Write($"{db[0]}-09/{delbr}"); // hardcoded
                //return new List<string>();
                sw.Write(delimiter);
                sw.Write("15.07.2019"); // hardcoded bez prigovor:  08.07.2019
                //sw.Write("26.08.2019");//polagac
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                sw.Write($"{klasen._ime} {(string.IsNullOrWhiteSpace(klasen._srednoIme) ? "" : $"{klasen._srednoIme}-")}{klasen._prezime}");
                sw.Write("\"");
                sw.Write(";");

                // datum na polaganje
                sw.Write("\"" + String.Join("|", u._maturska.Split('&').Select(x => x.Split('|')[3])) + "\"");
                sw.Write(";");

                // delovoden broj
                sw.Write("\"" + String.Join("|", u._maturska.Split('&').Select(x => x.Split('|')[4])) + "\"");
                sw.Write(";");

                // percentile
                sw.Write("\"" + String.Join("|", u._maturska.Split('&').Select(x => x.Split('|')[2])) + "\"");

                l.Add(sw.ToString());
            }
            return l;
        }

        private static string delovoden(Klasen klasen, Ucenik u)
        {
            return Requests.GetDelovoden(new Dictionary<string, string>
                {
                    { RequestParameters.token, klasen._token },
                    { RequestParameters.ime,  u._ime },
                    { RequestParameters.srednoIme, u._srednoIme },
                    { RequestParameters.prezime, u._prezime },
                    { RequestParameters.duplicate_ctr, u._duplicate_ctr.ToString() }
                });
        }

        public static void PrintDiploma(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int printerChoice, int offsetx, int offsety)
        {
            List<string> data = InitDiploma(siteUcenici, ucenici, klasen, offsetx, offsety);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            var vis = new DrawingVisual();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = true;

            data.Insert(0, "\"dipl\"");
            string outparam = String.Join("$", data);

            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam;
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            //return;
            int partition = 5;
            for (int part = 0; part < 9; part++)
            {
                if (partition * part > data.Count - 1)
                {
                    break;
                }

                printQueue.Clear();
                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    PrintQueueItem x = new PrintQueueItem();
                    x.sides = new System.Drawing.Image[1];
                    x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}dipl-{i}.jpg").AbsolutePath);

                    printQueue.Add(x);
                }

                currentPage = 0;
                maxSides = 1;
                pd.PrintPage += new PrintPageEventHandler(onPrintPage);

                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    currentSide = 0;
                    pd.Print();
                    currentPage++;
                }

                printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
                pd.Dispose();
            }
        }

        public static List<string> InitDiploma(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int offsetx, int offsety)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("mk-MK");
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("mk-MK");

            StringWriter sw = new StringWriter();
            List<string> l = new List<string>();
            string delimiter = "|";

            var ToPrint = new List<Ucenik>();

            // nepolagaci
            ToPrint.AddRange(siteUcenici.Where(x => !x._oceni.Contains(1)));

            // polagaci
            ToPrint.AddRange(siteUcenici.Where(x => !ToPrint.Contains(x)));

            string[] paralelka_godina = klasen._paralelka.Split('-');
            string[] db = klasen._delovoden_broj.Split('-');
            var val = int.Parse(db[1]) + int.Parse(year_dictionary[paralelka_godina[0]]) - 1;

            int ctr = 1;
            for (int i = 0; i < ToPrint.Count; i++)
            {
                if (!ToPrint[i]._oceni.Contains(0))
                {
                    ToPrint[i]._delovoden_broj = $"{db[0]}-{val.ToString("D2")}/{paralelka_godina[1]}/{ctr++}";
                }
            }

            ToPrint.Sort(delegate (Ucenik u1, Ucenik u2)
            {
                if (u1._delovoden_broj == "")
                {
                    if (u2._delovoden_broj == "")
                    {
                        return u1._broj.CompareTo(u2._broj);
                    }
                    return 1;
                }
                if (u2._delovoden_broj == "")
                {
                    return -1;
                }
                return int.Parse(u1._delovoden_broj.Split('/')[2]).CompareTo(int.Parse(u2._delovoden_broj.Split('/')[2]));
            });

            foreach (Ucenik u in ToPrint.Where(x => ucenici.ConvertAll(z => z._broj).Contains(x._broj)))
            {
                if (u._polozhil_matura == "не положил") continue;


                sw.GetStringBuilder().Clear();

                sw.Write("\"");

                // eksterni
                sw.Write(string.Join("|", u._maturski.GetRange(0, 3)
                    .ConvertAll(x => $"{x.predmet}/{x.ocena}/{x.percentilen.ToString(CultureInfo.GetCultureInfo("en-US"))}")));

                sw.Write("@");

                // interni
                sw.Write(string.Join("|", u._maturski.GetRange(3, u._maturski.Count - 4 /* tri eksterni i edna proektna */)
                    .ConvertAll(x => $"{x.predmet}/{x.ocena}/00.00")));

                sw.Write("@");

                // proektna
                sw.Write(string.Join("|", $"{u._maturski[u._maturski.Count - 1].predmet}/{u._maturski[u._maturski.Count - 1].ocena}/00.00"));

                sw.Write("\";");

                // "ime na uchilishte|mesto na uchilishte|glavna kniga|datum so momentalnata godina(mozhe i 00.00.2019)|ime na uchenik|
                // datum na ragjanje|mesto na ragjanje|drzhava|tip na obrazovanie|cel smer|delovoden broj na diploma|ime na klasen|
                // ime na direktor|akt broj|akt datum|ministerstvo
                sw.Write("\"");
                sw.Write(klasen._ucilishte);
                sw.Write(delimiter);
                sw.Write(klasen._grad);
                sw.Write(delimiter);
                sw.Write("36/1"); // hardcoded
                sw.Write(delimiter);
                sw.Write(DateTime.Now.Date.ToString("dd.MM.yyyy"));
                sw.Write(delimiter);
                sw.Write($"{u._ime} {u._prezime}");
                sw.Write(delimiter);
                sw.Write(u._roden);
                sw.Write(delimiter);
                sw.Write(u._mesto_na_ragjanje);
                sw.Write(delimiter);
                sw.Write(klasen._drzava);
                sw.Write(delimiter);
                sw.Write(u._tip);
                sw.Write(delimiter);
                sw.Write(klasen._p._smerovi[u._smer]._cel_smer.Trim());
                sw.Write(delimiter);
                sw.Write($"{db[0]}-09/{delovoden(klasen, u)}"); // hardcoded
                sw.Write(delimiter);
                //sw.Write("26.08.2019"); // hardcoded  bez prigovor :   08.07.2019
                sw.Write("08.07.2019"); // hardcoded  bez prigovor :   08.07.2019
                sw.Write(delimiter);
                sw.Write($"{klasen._ime} {(string.IsNullOrWhiteSpace(klasen._srednoIme) ? "" : $"{klasen._srednoIme}-")}{klasen._prezime}");
                sw.Write(delimiter);
                sw.Write(klasen._direktor);
                sw.Write(delimiter);
                sw.Write(klasen._akt);
                sw.Write(delimiter);
                sw.Write(klasen._akt_godina);
                sw.Write(delimiter);
                sw.Write(klasen._ministerstvo);

                sw.Write($"\";\"{offsetx}{delimiter}{offsety}\"");

                l.Add(sw.ToString());
            }
            return l;
        }

        public static void PrintPrednaStranaGK(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int printerChoice, int offsetx, int offsety)
        {
            List<string> data = InitPrednaStranaGK(siteUcenici, ucenici, klasen, offsetx, offsety);
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();

            // PrinterSettings.InstalledPrinters - lista na printeri
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[printerChoice];

            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A3);
            pd.OriginAtMargins = false;
            pd.DefaultPageSettings.Landscape = false;

            data.Insert(0, "\"start_page");
            string outparam = String.Join("$", data);
            //28.06.2019
            string pyscript = rootFolder + "\\print.exe";
            Process py = new Process();
            py.StartInfo.FileName = new Uri(pyscript).AbsolutePath;
            py.StartInfo.UseShellExecute = false;
            py.StartInfo.Arguments = outparam + "\"";
            py.StartInfo.CreateNoWindow = true;
            py.Start();
            py.WaitForExit();

            printQueue = new List<PrintQueueItem>();

            //return;
            int partition = 5;
            for (int part = 0; part < 9; part++)
            {
                if (partition * part > data.Count - 1)
                {
                    break;
                }

                printQueue.Clear();
                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    PrintQueueItem x = new PrintQueueItem();
                    x.sides = new System.Drawing.Image[1];
                    x.sides[0] = System.Drawing.Image.FromFile(new Uri($"{tmpFolder}start_page.jpg").AbsolutePath);

                    printQueue.Add(x);
                }

                currentPage = 0;
                maxSides = 1;
                pd.PrintPage += new PrintPageEventHandler(onPrintPage);

                for (int i = partition * part; i < Math.Min(data.Count - 1, partition * (part + 1)); i++)
                {
                    currentSide = 0;
                    pd.Print();
                    currentPage++;
                }

                printQueue.ForEach(x => x.sides.ToList().ForEach(job => job.Dispose()));
                pd.Dispose();
            }
        }



        public static List<string> InitPrednaStranaGK(List<Ucenik> siteUcenici, List<Ucenik> ucenici, Klasen klasen, int offsetx, int offsety)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("mk-MK");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("mk-MK");

            // https://raw.githubusercontent.com/darijan2002/ps/ps/gk/sample_params.txt?token=ADAAZQMNTPXEEBZ7LCUYXD245FMAC
            StringWriter sw = new StringWriter();
            string delimiter = "|";

            int n = siteUcenici.Count;
            int[] failed_offset = new int[n];
            bool[] failed_arr = new bool[n];

            // i = 0
            var result = didFail(siteUcenici[0], GetPolozilList(siteUcenici[0]));
            failed_offset[0] = result.offset;
            failed_arr[0] = result.did_fail;

            for (int i = 1; i < n; i++)
            {
                result = didFail(siteUcenici[i], GetPolozilList(siteUcenici[i]));
                failed_arr[i] = result.did_fail;
                failed_offset[i] = failed_offset[i - 1] + result.offset;
            }

            sw.GetStringBuilder().Clear();
            List<string> tmparr = new List<string>();

            // uciliste
            sw.Write($"{klasen._ucilishte};");
            // grad
            sw.Write($"{klasen._grad};");
            // tip na obrazovanie
            sw.Write($"{ucenici[0]._tip};");


            // smerovi
            var blacklist = new List<string> { "ПА", "Странски Јазици", "Изборни Предмети" };
            if ((new List<string> { "I", "II" }).Contains(klasen._paralelka.Split('-')[0])) sw.Write("///;");
            else 
            sw.Write($"{string.Join("|", klasen._smerovi.Split(',').Where(x => !blacklist.Contains(x)))};");

            sw.Write("///;");

            // paralelka
            sw.Write($"{klasen._paralelka.Replace('-', ';')};");

            // broj na gk
            sw.Write(klasen._glavna_kniga + '/' + year_dictionary[klasen._paralelka.Split('-').FirstOrDefault()]);// <----
            sw.Write(';');
            // ucebna godina
            sw.Write($"20{klasen._godina}/20{klasen._godina + 1};");

            // broj na maski i zenski
            sw.Write(";");

            // oceni i udeli

            sw.Write($"{ucenici.Count}");

            var proseci = ucenici.Select(x => decimal.Parse(x.prosek()));
            for (int i = 5; i >= 2; i--)
            {
                sw.Write($"|{proseci.Where(x => decimal.Round(x) == i).Count()}");
                if (proseci.Where(x => decimal.Round(x) == i).Count() == 0) sw.Write("|0,00%");
                else 
                sw.Write($"|{decimal.Round((decimal)100.0 * proseci.Where(x => decimal.Round(x) == i).Count() / proseci.Where(x => decimal.Round(x) > 1).Count(), 2, MidpointRounding.AwayFromZero) }%");
            }

            sw.Write($"|{proseci.Where(x => decimal.Round(x) > 1).Count()}");
            sw.Write($"|100,00%");


            sw.Write($"|{proseci.Where(x => decimal.Round(x) == 1).Count()}");
            sw.Write($"|{decimal.Round((decimal)100.0 * proseci.Where(x => decimal.Round(x) == 1).Count() / proseci.Count(), 2) }%");


            sw.Write(";");

            // klasen
            sw.Write($"{klasen._ime} {(!string.IsNullOrWhiteSpace(klasen._srednoIme) ? klasen._srednoIme + " " : "")}{klasen._prezime};");

            // direktor
            sw.Write(klasen._direktor);

            /*
            TODO:

Form of string: start_page$<School name>;<Town>;<Type of education>;<Module of subhects 1>|<Module of subjects 2>|...;<Educational profile?>;
<Year grade>;<Class number>;<Book's number>;<School year>;<Nmb of males>|<Nmb of females>|<Total>|<Nmb of males 2>|...;
<Nmb of students>|<Nmb of A's>|<Percent of A's>|<Nmb of B's>|<Percent of B's>|...;
<Name and last name of homeroom teacher>;<Name of director>

Example string ( for testing ): "start_page$Средно училиште „Раде Јовчевски - Корчагин“;Скопје;Гимназиско образование;Природно-математичко подрачје, комбинација А|Општествено-хуманистичко подрачје, комбинација Б|Јазично уметничко подрачје, комбинација Ц| Природно-математичко подрачје, комбинација Б;///;IV;1;54/3;2018/2019;0|0|20|30|15|10;32|22|15|10|20;Жаклина Пандова;м-р Драган Арсовски"
            */

            return new List<string> { sw.ToString() };
        }

    public static void PrintCarsav(int printerChoice) // TODO: testing
    {
        // Requests.GetCarsav();

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
