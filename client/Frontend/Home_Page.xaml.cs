using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Home_Page.xaml
    /// </summary>
    public partial class Home_Page : Page
    {

        Frame Main;
        Page loginPage;
        public Home_Page(Frame m ,  Page loginpage)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;
           
        }

        private void MouseEnter(object sender, MouseButtonEventArgs e)
        {
            //Main.Content = loginPage;
            Main.Content = new Oceni();
        }

    }
}
