using AdminPanel;
using AdminPanel.Middleware.Models;
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
        Admin admin;
        static Dictionary<string, List<Middleware.Models.Klasen>> users;

        public AdminFrame(Admin admin, Dictionary<string, List<Middleware.Models.Klasen>> Users)
        {
            InitializeComponent();
            this.admin = admin;
            users = Users;


            Klasen klasen = users["IV"][0];
            DataContext = new AdminViewModel(klasen);

            //ListBox.ItemTemplate = new Pole().DT();
            foreach (Pole item in new AdminViewModel(klasen).Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }
            // for(int i = 0; i<3;i++)Ugrid.Children.Add(new DefaultPole("Test " + i.ToString(), "Answer").GetPole());
            Ugrid.Children.Add(new PredmetiPole(new string[] { "Makedonski", "Matematika", "Geografija" }, "PMA").GetPole());
            Ugrid.Children.Add(new PasswordPole("Password", "Password").GetPole());

            Console.WriteLine(klasen.Username);
        }

    }

      
}

public class AdminViewModel
{
    public Klasen Klasen;
    public List<Pole> Items
    {
        get
        {
            return new List<Pole>
                {
                   new Pole { Name = "Корисничко име" , Question = new string[] { "ime prezime" }, Model_object = Klasen, Parametar = "UsernameBind" },
                        new Pole { Name = "Презиме" , Question = new string[] { "Password" } , Type="PW", Model_object = Klasen, Parametar = "PrezimeBind"},
                        new Pole { Name = "Паралелка" , Question = new string[] { "True","False" } , Answer = "True", Model_object = Klasen, Parametar = "SrednoImeBind" },
                        //new Pole { Klasen = users["IV"][5]}
                };
        }
    }

    public AdminViewModel(Klasen klasen)
    {
        this.Klasen = klasen;
    }
}