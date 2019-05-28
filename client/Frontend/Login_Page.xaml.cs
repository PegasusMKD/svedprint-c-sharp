using MainWindows;
using Middleware;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            login();
        }



        private void login()
        {

            Klasen temp = Login.LoginWithCred(Username_txt.Text, Password_txt.Text);

            if (temp._ime != "002" && temp._ime != string.Empty && temp._ime != null)
            {
                ShowAlertBox("Успешно логирање");
                Main.Content = new Home_Page(Main, this, temp);
            }
            else
            {
                ShowAlertBox("Неуспешно логирање");
            }

        }

        [Conditional("DEBUG")]
        private void hardcode_userdata()
        {
          
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

        private void Username_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Username_txt.Text == "Корисничко име") RemoveText(sender);
        }

        private void Username_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Username_txt.Text == "") AddText(sender, "Корисничко име");
        }

        private void AddText(object sender, string v)
        {
            if (string.IsNullOrWhiteSpace(((System.Windows.Controls.TextBox)sender).Text))
            {
                ((System.Windows.Controls.TextBox)sender).Text = v;
            }
        }

        private void Password_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Password_txt.Text == "Лозинка") RemoveText(sender);
        }

        private void RemoveText(object sender)
        {
            ((System.Windows.Controls.TextBox)sender).Text = "";
        }

        private void Password_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Password_txt.Text == "") AddText(sender, "Лозинка");
        }
       
        private void Login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login();
            }
        }
        
    }
}
