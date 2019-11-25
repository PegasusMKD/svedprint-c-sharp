﻿using AdminPanel.Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für UcilisteFrame.xaml
    /// </summary>
    public partial class UcilisteFrame : Page
    {
        public UcilisteFrame(Admin admin)
        {
            InitializeComponent();


            foreach (Pole item in new UcilisteViewModel(admin).Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }
        }
    }

    public class UcilisteViewModel
    {

        Admin admin;
        public UcilisteViewModel(Admin admin)
        {
            this.admin = admin;
        }

        public List<Pole> Items
        {
            get
            {
                return new List<Pole>
                {

                    new Pole ("Име на Училиште" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "ImeView" ,  admin.Uchilishte , admin ),
                    new Pole ("Деловоден Број" , new string[] { "182/5" } , "DelovodenBrojView" , admin.Uchilishte  ),
                    new Pole ("Датум на Одобрено Сведителство" , admin.Uchilishte.MozniDatiSveditelstvaView.ToArray() , "DatiSveditelstvaView" , admin.Uchilishte , admin ),
                    new Pole ("Име на министерство" , new string[] { "Министерство за образование" } , "MinisterstvoView" , admin.Uchilishte  , admin ),
                    new Pole ("Број на главна книга" , new string[] { "55" } , "GlavnaKnigaView" , admin.Uchilishte , admin ),
                    new Pole ("Дати Матура" , admin.Uchilishte.MozniDatiMaturaView.ToArray() , "DatiMaturaView" , admin.Uchilishte , admin ),
                    new Pole ("Број на акт" , new string[] { "5" } , "AktView" , admin.Uchilishte , admin ),
                    new Pole ("Година на дозволување на актот" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "AktGodinaView" , admin.Uchilishte  , admin ),
                    new Pole ("Директор" , new string[] { "Име Презиме" } , "DirektorView" , admin.Uchilishte  , admin )
                };
            }
        }
    }
}
