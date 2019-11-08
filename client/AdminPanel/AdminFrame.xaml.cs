using AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaktionslogik für AdminFrame.xaml
    /// </summary>
    public partial class AdminFrame : Page
    {
        NavigationService ns;
        public AdminFrame()
        {
            InitializeComponent();

            DataContext = new AdminViewModel();

            //ListBox.ItemTemplate = new Pole().DT();
            foreach(Pole item in new AdminViewModel().Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }
            // for(int i = 0; i<3;i++)Ugrid.Children.Add(new DefaultPole("Test " + i.ToString(), "Answer").GetPole());
            Ugrid.Children.Add(new PredmetiPole(new string[] { "Makedonski", "Matematika", "Geografija" }, "PMA").GetPole());
            Ugrid.Children.Add(new PasswordPole("Password","Password").GetPole());

        }
     
    }
}

public class AdminViewModel
{
    public List<Pole> Items
    {
        get
        {
            return new List<Pole>
                {
                    new Pole { Name = "Корисничко име" , Question = new string[] { "ime prezime" } },
                    new Pole { Name = "Лозинка" , Question = new string[] { "Password" } , Type="PW"},
                    new Pole { Name = "Дозвола за печатење" , Question = new string[] { "True","False" } , Answer = "True" },
                };
        }
    }
}