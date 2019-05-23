using Middleware;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;

namespace TestingApp
{
    class Program
    {
        static Klasen klasen;
        static List<Ucenik> uceniks;
        static void init()
        {
           string path = Environment.CurrentDirectory + "\\outnew.json";
           string path2 = Environment.CurrentDirectory + "\\outold.json";
           File.Copy(path, path2, true);
           StreamWriter sw = new StreamWriter(path);
           Console.SetOut(sw);
           Console.WriteLine(DateTime.Now);

            string user = "Pazzio2";
            string pass = "test_pass";
            klasen = Login.LoginWithCred(user, pass);

            List<Dictionary<string, string>> paralelka = Requests.GetData(new Dictionary<string, string>()
            {
                { "token", klasen._token}
            }, RequestScopes.GetParalelka);

            klasen.PopulateSmeroviFromUcenici(uceniks = paralelka.ConvertAll(x => new Ucenik(x)));            

            Dictionary<string, Smer> smerovi = new Dictionary<string, Smer>();
            foreach (string smer in klasen.GetSmerovi())
            {
                var res = Requests.GetData(
                    new Dictionary<string, string>()
                    {
                        {RequestParameters.token, klasen._token },
                        {RequestParameters.smer, smer}
                    },
                    RequestScopes.GetPredmetiSmer
                );
                Smer x = new Smer(res[0]["predmeti"].Split(',').ToList(), smer);
                smerovi.Add(smer, x);
            }
            klasen._p._smerovi = smerovi;
            
            sw.Close();
            
            //Console.WriteLine(uceniks[0]._paralelka);

        }
        static void Main()
        {
            init();
            //print();
        }

        static void print()
        {
            // Print.print2pdf();
            //Print.print2prntr(new List<Ucenik>() { uceniks[0], uceniks[1], uceniks[2] }, klasen);

            Console.WriteLine("Choose printer:");
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                Console.WriteLine(string.Format("{0}: {1}", i, PrinterSettings.InstalledPrinters[i]));
            }
            int choice = int.Parse(Console.ReadLine());

            Print.PrintSveditelstva(new List<Ucenik>(), new Klasen(), choice);
        }
    }
}
