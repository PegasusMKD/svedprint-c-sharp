using AdminPanel.Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminPanel
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    /// 

    public partial class UsersWindow : Window
    {
        Dictionary<string, List<Middleware.Models.Klasen>> users;
        Admin admin;

        public UsersWindow(Admin admin,Dictionary<string, List<Middleware.Models.Klasen>> users)
        {
            InitializeComponent();

            this.users = users;
            this.admin = admin;
            User.DataContext = users["IV"][0];

            DataContext = this;
        }

        private void Update_Users(object sender, RoutedEventArgs e)
        {
            Middleware.Controllers.Klasen.UpdateUsers(this.admin, this.users);
        }
    }
}