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

            DataContext = new AdminViewModel(admin);

            foreach (Pole item in new AdminViewModel(admin).Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }

            this.MouseLeave += AdminFrame_MouseLeave;

        }

        private void AdminFrame_MouseLeave(object sender, MouseEventArgs e)
        {
            //foreach(StackPanel sp in Ugrid.Children)
            //{
            //    foreach(Pole item in sp.Children)
            //    {
            //        if (item.Type == "PW")
            //        {
            //            Middleware.Controllers.Admin.UpdateData(admin, item.Answer.ToString());
            //        }
            //    }
            //}

            //Middleware.Controllers.Admin.UpdatePrint(admin);
        }

        /// <summary>
        /// The Button function which activates the year transfer
        /// <para>We should add some kind of a notification when he clicks the button</para>
        /// <para>And ask him if he's sure about the transfer, And for them to stop using the software for the next 24 hours or so.</para>
        /// </summary>
        private void Transfer_Year(object sender, RoutedEventArgs e)
        {
            
            bool retval = Middleware.Controllers.Global.TransferYear(admin);
            if (retval) throw new Exception("Започна префрлувањето на учениците во следната учебна година.\n Проверете утре дали добро се извршила транзицијата.\n Доколку не се префрлиле, ве молиме исконтактирајте ги администраторите на системот, или девелоперите!");
            else throw new Exception("Има некој проблем во системот, ве молиме обидете се подоцна, или исконтактирајте ги администраторите!");

        }
    }

}

public class AdminViewModel
{
    public Klasen Klasen;
    public Admin admin;
    public Dictionary<string, List<Klasen>> users;
    public List<Pole> Items
    {
        get
        {
            return new List<Pole>
                {
                   new Pole { Name = "Корисничко име" , Question = new string[] { admin.Username }, admin = admin, Parametar = "Username" },
                   new Pole { Name = "Лозинка" , Question = new string[] { "" } , Type="PW", Model_object = admin, Parametar = "Password" },
                   new Pole { Name = "Дозволено Принтање" , Question = new string[] { "True","False" } , Answer = "True", Model_object = admin, Parametar = "IsPrintAllowed" , Type = "CheckBox" },
                        //new Pole { Klasen = users["IV"][5]}
                };
        }
    }

    public AdminViewModel(Admin admin)
    {
        this.admin = admin;
        
    }
}