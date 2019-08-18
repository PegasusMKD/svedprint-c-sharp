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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _username;
        private Middleware.Models.Admin admin;
        private Dictionary<string, List<Middleware.Models.Klasen>> users;
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


        public MainWindow(Middleware.Models.Admin admin,Dictionary<string,List<Middleware.Models.Klasen>> users)
        {
            InitializeComponent();
            DataContext = this;
            this.admin = admin;
            this.users = users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"{Username} - {password.Password}");
            if (Username != this.admin.Username)
                this.admin.Username = Username;

            Middleware.Models.Admin a = this.admin;
            try
            {
                a.UpdateData(password.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.ExceptionMessages.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Build_Users(object sender, RoutedEventArgs e)
        {
            UsersWindow main = new UsersWindow(this.admin,this.users);
            App.Current.MainWindow = main;
            main.Show();
            this.Close();


        }
    }
}

