using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MainWindows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Middleware;

namespace Frontend
{
    public partial class Login_Page : Page
    {
        Frame Main;
        Data data;
        System.Windows.Threading.DispatcherTimer AlertTimer;


        public Login_Page(Frame m)
        {
            InitializeComponent();
            Main = m;
            data = new Data();
            AlertTimer = new System.Windows.Threading.DispatcherTimer();
            AlertTimer.Tick += new EventHandler(AlertTimer_Tick);
            AlertTimer.Interval = new TimeSpan(0, 0, 5);
        }


        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            Klasen temp = Login.LoginWithCred(Username_txt.Text, Password_txt.Text);
            Klasen klas = new Klasen
            {
                _ime = "luka",
                _prezime = "prez",
                _paralelka = "IV5"
            };
            if (temp._ime != "002" && temp._ime != string.Empty)
            {
                ShowAlertBox("Успешно логирање");
                Main.Content = new Home_Page(Main,this,temp);
            }
            else
            {
                ShowAlertBox("Неуспешно логирање");
            }

        }

        private void ShowAlertBox(string Alert)
        {
            AlertLabel.Content = Alert;
            AlertTimer.Start();
            AlertPanel.Visibility = Visibility.Visible;
        }

        private void AlertTimer_Tick(object sender, EventArgs e)
        {
           AlertPanel.Visibility = Visibility.Hidden;
        }
    }
}
