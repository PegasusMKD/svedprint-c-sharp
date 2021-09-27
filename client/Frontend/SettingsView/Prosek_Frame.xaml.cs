using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using static Frontend.SettingsDesign;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Prosek_Frame.xaml
    /// </summary>
    public partial class Prosek_Frame : Page
    {

        Klasen UserKlas;
        List<Ucenik> Ucenici;

        public Prosek_Frame()
        {
            InitializeComponent();
            //UserKlas = Home_Page.KlasenKlasa;
            //Ucenici = Home_Page.ucenici;

            GetData(PredmetiProsekCalc());
        }

        private void GetData(Dictionary<String, String> res)
        {

            int ctr = 0;
            foreach (KeyValuePair<string, string> entry in res)
            {

                StackPanel st = new StackPanel();
                st.Children.Add(ContentLabel(entry.Key));
                st.Children.Add(TextBorderGrid(entry.Value));

                if (ctr % 2 == 0)
                {
                    st1.Children.Add(st);
                }
                else
                {
                    st2.Children.Add(st);
                }
                ctr++;
            }
        }

        private Grid TextBorderGrid(string TextBoxText)
        {
            Grid gd = new Grid();
            gd.Margin = new Thickness(0, 0, 0, 10);

            Border border = CreateBorder(10, 0, 0, 5, "#FF3D84C6");
            border.Margin = new Thickness(25, 0, 20, 0);

            Border Circle = CreateBorder(60, 0, 0, 30, "#FF3D84C6");
            Circle.Margin = new Thickness(0, -35, 5, -10);
            Circle.VerticalAlignment = VerticalAlignment.Top;
            Circle.HorizontalAlignment = HorizontalAlignment.Right;
            Circle.Width = 60;

            Label tx = CreateLabel(TextBoxText, 23, "Arial");
            tx.Margin = new Thickness(0);
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            Circle.Child = tx;

            gd.Children.Add(border);
            gd.Children.Add(Circle);

            return gd;
        }

        private Label ContentLabel(string Text)
        {
            Label label = CreateLabel(Text, 24, "arial");
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.Margin = new Thickness(30, 0, 0, 0);
            return label;
        }

        private Dictionary<string, string> PredmetiProsekCalc()
        {
            // StringBuilder zosto pobrzo e od obicno string concatenation
            Dictionary<string, StringBuilder> PredmetiProsek = new Dictionary<string, StringBuilder>();
            foreach (Ucenik ucenik in Ucenici)
            {
                if (ucenik._smer == "" || UserKlas._p._smerovi.Keys.Contains(ucenik._smer) == false) continue;
                // List<string> PredmetiOdSmer = UserKlas._p._smerovi[ucenik._smer]._predmeti;
                List<string> PredmetiOdSmer = UserKlas._p._smerovi[ucenik._smer].GetCeliPredmeti(ucenik._jazik, ucenik._izborni, UserKlas._p._smerovi);

                if (PredmetiOdSmer.Count == 0) continue;

                for (int i = 0; i < ucenik._oceni.Count && i < PredmetiOdSmer.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(PredmetiOdSmer[i])) continue;
                    if (PredmetiProsek.ContainsKey(PredmetiOdSmer[i]))
                    {
                        // PredmetiProsek[PredmetiOdSmer[i]] = PredmetiProsek[PredmetiOdSmer[i]] + " " + ucenik._oceni[i].ToString();
                        PredmetiProsek[PredmetiOdSmer[i]].AppendFormat(" {0}", ucenik._oceni[i]);
                    }
                    else
                    {
                        // PredmetiProsek[PredmetiOdSmer[i]] = ucenik._oceni[i].ToString();
                        PredmetiProsek.Add(PredmetiOdSmer[i], new StringBuilder(ucenik._oceni[i].ToString()));
                    }
                }

            }

            Dictionary<string, StringBuilder> t = PredmetiProsek;
            Dictionary<string, string> retval = new Dictionary<string, string>();
            foreach (var predmet in t.ToArray())
            {
                //PredmetiProsek[predmet.Key].Clear();
                //PredmetiProsek[predmet.Key].Append(Array.ConvertAll(predmet.Value.ToString().Split(' '), x => float.Parse(x)).Average().ToString("n2"));
                if (predmet.Key == "") continue;
                retval[predmet.Key] = Array.ConvertAll(predmet.Value.ToString().Split(' '), x => float.Parse(x)).Average().ToString("n2");
            }


            return retval;
        }

    }
}
