using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaktionslogik für LoginFrame.xaml
    /// </summary>
    public partial class LoginFrame : Page , INotifyPropertyChanged
    {
        private string _username;
        NavigationService ns;
        public string Username
        {

            get => _username;
            set
            { 
                if (value != _username)
                { 
                    _username = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public LoginFrame(NavigationService ns)
        {
            InitializeComponent();
            DataContext = this;

            this.ns = ns;

            Username = (string)FindResource("UsernameLabelText");
            Pw_Label.Content = (string)FindResource("PasswordLabelText");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ns.Navigate(new MainFrame(ns));
            return;
            Debug.WriteLine($"{Username} - {password.Password}");
            Middleware.Models.Admin AdminAccount = new Middleware.Models.Admin(Username);
            try
            {
                AdminAccount.GetData(password.Password);
                var KlasniData = Middleware.Controllers.Klasen.RetrieveUsers(AdminAccount);


                MainWindow main = new MainWindow();
                App.Current.MainWindow = main;
                main.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.ExceptionMessages.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Pw_GotFocus(object sender, RoutedEventArgs e)
        {
            Pw_Label.Visibility = Visibility.Hidden;
        }

        private void Pw_LostFocus(object sender, RoutedEventArgs e)
        {
            if (password.Password == "") Pw_Label.Visibility = Visibility.Visible;
        }

        private void Username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Username == (string)FindResource("UsernameLabelText")) Username = string.Empty;
        }

        private void Username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (username.Text == string.Empty) Username = (string)FindResource("UsernameLabelText");
        }
    }
}
