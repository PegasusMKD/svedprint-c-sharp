using AdminPanel.Middleware.Models;
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
        Middleware.Models.Admin admin;
        public UcilisteFrame(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();


            foreach (Pole item in new UcilisteViewModel(admin).Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }

            this.MouseLeave += UcilisteFrame_MouseLeave;
        }

        private void UcilisteFrame_MouseLeave(object sender, MouseEventArgs e)
        {
            Middleware.Controllers.Uchilishte.UpdateDates(admin, admin.Uchilishte);
            Middleware.Controllers.Uchilishte.UpdateSchool(admin,admin.Uchilishte);
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

                    new Pole ("Име на училиште" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "ImeView" ,  admin.Uchilishte , admin ),
                    new Pole ("Директор" , new string[] { "Име Презиме" } , "DirektorView" , admin.Uchilishte  , admin ),
                    new Pole ("Датум на дозволување/одобрување свидетелство" , admin.Uchilishte.MozniDatiSveditelstvaView.ToArray() , "DataSveditelstvaView" , admin.Uchilishte , admin ),
                    new Pole ("Датум на дозволување/одобрување диплома" , admin.Uchilishte.MozniDatiMaturaView.ToArray() , "DataMaturaView" , admin.Uchilishte , admin ),
                    new Pole ("Деловоден број" , new string[] { "182/5" } , "DelovodenBrojView" , admin.Uchilishte  ),
                    new Pole ("Број на главна книга" , new string[] { "55" } , "GlavnaKnigaView" , admin.Uchilishte , admin ),
                    new Pole ("Број на акт" , new string[] { "5" } , "AktView" , admin.Uchilishte , admin ),
                    new Pole ("Година на дозволување/одобрување на актот" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "AktGodinaView" , admin.Uchilishte  , admin ),
                    new Pole ("Име на министерство" , new string[] { "Министерство за образование" } , "MinisterstvoView" , admin.Uchilishte  , admin )
                };
            }
        }
    }
}
