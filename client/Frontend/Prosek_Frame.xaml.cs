﻿using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
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
            UserKlas = Home_Page.KlasenKlasa;
            Ucenici = Home_Page.ucenici;

            Dictionary<String, String> dic = new Dictionary<string, string>();
            dic.Add("Македонски", "5.0");
            dic.Add("Математика", "4.2");
            dic.Add("Физика", "2.7");
            dic.Add("Географија", "4.2");
            dic.Add("Историја", "5.0");
            dic.Add("Филозофија", "1.9");
            dic.Add("Француски", "2.7");
            dic.Add("Музичко", "4.9");

            Calc();
            GetData(PredmetiProsek);

        }

        private void GetData(Dictionary<String, String> res)
        {

            int ctr = 0;
            foreach (KeyValuePair<string, string> entry in res)
            {
                StackPanel st = new StackPanel();
                st.Children.Add(ContentTextBox(entry.Key));
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

            Border Circle = CreateBorder(50, 0, 0, 20, "#FF3D84C6");
            Circle.Margin = new Thickness(0, -35, 5, -10);
            Circle.VerticalAlignment = VerticalAlignment.Top;
            Circle.HorizontalAlignment = HorizontalAlignment.Right;
            Circle.Width = 50;

            TextBox tx = ContentTextBox(TextBoxText);
            tx.FontSize = 25;
            tx.Margin = new Thickness(0);
            tx.HorizontalAlignment = HorizontalAlignment.Center;
            tx.VerticalAlignment = VerticalAlignment.Center;
            Circle.Child = tx;

            gd.Children.Add(border);
            gd.Children.Add(Circle);

            return gd;
        }

        Dictionary<string, string> PredmetiProsek = new Dictionary<string, string>();
        private void Calc()
        {
            foreach(Ucenik ucenik in Ucenici)
            {
                List<string> PredmetiOdSmer = UserKlas._p._smerovi[ucenik._smer]._predmeti;

                for (int i = 0; i <PredmetiOdSmer.Count; i++)
                {
                    if (PredmetiProsek.ContainsKey(PredmetiOdSmer[i]))
                    {
                        PredmetiProsek[PredmetiOdSmer[i]] = PredmetiProsek[PredmetiOdSmer[i]] + " " + ucenik._oceni[i].ToString();
                    }
                    else
                    {
                        PredmetiProsek[PredmetiOdSmer[i]] = ucenik._oceni[i].ToString();
                    }
                }

            }

            Dictionary<string, string> t = PredmetiProsek;
            foreach(var predmet in  t.ToArray())
            {
                PredmetiProsek[predmet.Key] = Array.ConvertAll(predmet.Value.Split(' '), x => float.Parse(x)).Average().ToString("n2");
            }
        }

    }
}
