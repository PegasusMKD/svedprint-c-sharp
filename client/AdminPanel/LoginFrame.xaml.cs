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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{Username} - {password.Password}");
            Middleware.Models.Admin a = new Middleware.Models.Admin(Username);
            try
            {
                a.GetData(password.Password);
                var x = Middleware.Controllers.Klasen.RetrieveUsers(a);
                a.UsernamePERMA = a.Username;
                a.isPrintAllowedLV = a.IsPrintAllowed;

                ns.Navigate(new MainFrame(ns, a , x));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.ExceptionMessages.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}
