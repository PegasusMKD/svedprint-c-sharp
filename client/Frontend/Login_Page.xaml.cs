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

            if (data.CheckUser(Username_txt.Text, Password_txt.Text) > -1)
            {
                ShowAlertBox("Успешно логирање");
                Main.Content = new Home_Page(Main,this);
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
