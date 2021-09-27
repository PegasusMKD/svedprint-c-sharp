using MiddlewareRevisited.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frontend
{
    public partial class Login_Page : Page
    {
        Frame Main;
        System.Windows.Threading.DispatcherTimer AlertTimer;


        public Login_Page(Frame m)
        {
            InitializeComponent();

            Main = m;
            AlertTimer = new System.Windows.Threading.DispatcherTimer();
            AlertTimer.Tick += new EventHandler(AlertTimer_Tick);
            AlertTimer.Interval = new TimeSpan(0, 0, 5);

            Username_txt.Text = "9i2Bzz1aqF";
            Password_txt.Password = "qaeI6TKeJ7";

            login();

            InjectServerLabel();
        }

        private void InjectServerLabel()
        {
            ServerLabel.Content = $"Server branch: {Middleware.Login.ServerBranch}";
        }

        private async void Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            await login();
        }

        private async Task login()
        {
            // Klasen temp = await Login.LoginWithCredAsync(Username_txt.Text, Password_txt.Password);
            var dt = DateTime.Now;
            try
            {
                User u = await MiddlewareRevisited.Login.httpClientLogin(Username_txt.Text, Password_txt.Password);
                ShowAlertBox((DateTime.Now - dt).ToString());
                NavigationService.Navigate(new Home_Page(Main, u));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                ShowAlertBox(ex.StackTrace.ToString());
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
            AlertTimer.Stop();
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
            if (string.IsNullOrWhiteSpace(((TextBox)sender).Text)) 
                ((TextBox)sender).Text = v;
        }

        private void Password_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            Pw_Label.Visibility = Visibility.Hidden;
        }

        private void RemoveText(object sender)
        {
            ((TextBox)sender).Text = "";
        }

        private void Password_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Password_txt.Password == "") Pw_Label.Visibility = Visibility.Visible;
        }

        private async void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                await login();
        }

    }
}
