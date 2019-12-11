using AdminPanel;
using AdminPanel.Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Ugrid.Columns = 3;
            this.MouseLeave += AdminFrame_MouseLeave;

        }

        private void AdminFrame_MouseLeave(object sender, MouseEventArgs e)
        {

            var pass = ((((Ugrid.Children[1] as StackPanel).Children[1] as StackPanel).Children[0] as Grid).Children[0] as TextBox).Text;

            if (admin.UsernamePERMA != admin.Username || !string.IsNullOrWhiteSpace(pass))
            {
                Thread t = new Thread(() => Middleware.Controllers.Admin.UpdateData(admin, pass))
                {
                    IsBackground = true
                };
                t.Start();

                admin.UsernamePERMA = admin.Username;
            }

            if (admin.IsPrintAllowed != admin.isPrintAllowedLV)
            {
                Thread t = new Thread(() => Middleware.Controllers.Admin.UpdatePrint(admin))
                {
                    IsBackground = true,
                };
                t.Start();

                admin.isPrintAllowedLV = admin.IsPrintAllowed;
            }
        }
        /// <summary>
        /// The Button function which activates the year transfer
        /// <para>We should add some kind of a notification when he clicks the button</para>
        /// <para>And ask him if he's sure about the transfer, And for them to stop using the software for the next 24 hours or so.</para>
        /// </summary>
        private void Transfer_Year(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Дали сте сигурни дека сакате да се префрлат сите ученици една година погоре?\n" +
                    "Тоа би значело:\n" +
                    "    - Бришење на сите ученици во 4та година;\n" +
                    "    - Ресетирање/Бришење на сите оцени, деловодни броеви и други информации кои се менуваат годишно;\n\n" +
                    "Доколку сте сигурни и знаете дека никој не го користи софтверот од вашето училиште во моментот, притиснете на копчето \"Yes\", а доколку не, притиснете на копчето \"No\"!", "", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No) == MessageBoxResult.No) return;

                bool retval = Middleware.Controllers.Global.TransferYear(admin);
                if (retval) MessageBox.Show(
                    "Започна префрлувањето на учениците во следната учебна година.\n" +
                    "Проверете утре дали добро се извршила транзицијата.\n" +
                    "Доколку не се префрлиле, ве молиме исконтактирајте ги администраторите на системот, или девелоперите!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                else throw new Exception("Има некој проблем во системот, ве молиме обидете се подоцна, или исконтактирајте ги администраторите!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                   new Pole { Name = "Корисничко име" , Question = new string[] { admin.Username }, admin = admin,Model_object=admin, Parametar = "Username" },
                   new Pole { Name = "Лозинка" , Question = new string[] { "" } , Type="PW", Model_object = admin, Parametar = "Password" },
                   new Pole { Name = "Дозволено печатење" , Question = new string[] { "True","False" } , Answer = "True", Model_object = admin, Parametar = "IsPrintAllowed" , Type = "CheckBox" }
                };
        }
    }

    public AdminViewModel(Admin admin)
    {
        this.admin = admin;
        
    }
}