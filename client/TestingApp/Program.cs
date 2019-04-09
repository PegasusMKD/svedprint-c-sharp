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

            Dictionary<string, Smer> smerovi = new Dictionary<string, Smer>();
            Smerovi s = new Smerovi();
            foreach (string smer in s.GetType().GetFields().Select(field => field.GetValue(s)).ToList())
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
            var tmp = Requests.GetData(
                new Dictionary<string, string>()
                {
                    {RequestParameters.token, klasen._token},
                },
                RequestScopes.GetParalelka
            );

            uceniks = new List<Ucenik>();
            tmp.ForEach(x => uceniks.Add(new Ucenik(x)
            {
                _s = smerovi[x["smer"]]
            }));
            //Console.WriteLine(uceniks[0]._paralelka);

        }
        static void Main()
        {
            //init();
            print();
        }

        static void print()
        {
            // Print.print2pdf();
            //Print.print2prntr(new List<Ucenik>() { uceniks[0], uceniks[1], uceniks[2] }, klasen);
            Print.print2prntr(new List<Ucenik>(), new Klasen());
        }
    }
}
