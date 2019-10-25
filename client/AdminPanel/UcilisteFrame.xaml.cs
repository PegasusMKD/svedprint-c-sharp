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
        public UcilisteFrame()
        {
            InitializeComponent();

            foreach (Pole item in new UcilisteViewModel().Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }
        }
    }

    public class UcilisteViewModel
    {
        public List<Pole> Items
        {
            get
            {
                return new List<Pole>
                {

                    new Pole ("Име на Училиште" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "parametar"  ),
                    new Pole ("Деловоден Број" , new string[] { "182/5" } , "parametar"  ),
                    new Pole ("Датум на Одобрено Сведителство" , new string[] { "25.05.2020" } , "parametar"  ),
                    new Pole ("Име на министерство" , new string[] { "Министерство за образование" } , "parametar"  ),
                    new Pole ("Број на главна книга" , new string[] { "55" } , "parametar"  ),
                    new Pole ("Број на акт" , new string[] { "5" } , "parametar"  ),
                    new Pole ("Година на дозволување на актот" , new string[] { "СУГС Раде Јовчевски Корчагин" } , "parametar"  ),
                    new Pole ("Директор" , new string[] { "Име Презиме" } , "parametar"  )
                };
            }
        }
    }
}
