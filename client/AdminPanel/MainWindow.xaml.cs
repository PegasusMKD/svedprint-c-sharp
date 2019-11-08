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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            MainFrame.Navigate(new LoginFrame(MainFrame.NavigationService));
        }

        private void Build_Users(object sender, RoutedEventArgs e)
        {
            UsersWindow main = new UsersWindow(this.admin,this.users);
            App.Current.MainWindow = main;
            main.Show();
            this.Close();


        }

        private void Build_Students(object sender, RoutedEventArgs e)
        {
            List<Middleware.Models.Ucenik> k = Middleware.Controllers.Ucenik.RetrieveStudents(admin);
            StudentsWindow main = new StudentsWindow(this.admin, k);
            App.Current.MainWindow = main;
            main.Show();
            this.Close();
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

