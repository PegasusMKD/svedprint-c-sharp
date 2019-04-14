using Middleware;
using System.Collections.Generic;
using System.Linq;

namespace TestingApp
{
    class Program
    {
        static Klasen klasen;
        static List<Ucenik> uceniks;
        static void init()
        {
            string user = "Pazzio2";
            string pass = "test_pass";
            klasen = Login.LoginWithCred(user, pass);

            List<Dictionary<string, string>> paralelka = Requests.GetData(new Dictionary<string, string>()
            {
                { "token", klasen._token},
                {"paralelka", klasen._paralelka }
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
            Print.print2prntr(new List<Ucenik>(), new Klasen());
        }
    }
}
