using MainWindows;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Data data;

        public MainWindow()
        {
            InitializeComponent();
            data = new Data();
        }

        private void Login_Btn_Click(object sender, RoutedEventArgs e)
        {           

            if (data.CheckUser(Username_txt.Text, Password_txt.Text )> -1)
            {
                AlertPanel.Visibility = Visibility.Hidden;
                ShowAlertBox("Успешно логирање");
            }
            else
            {
                AlertPanel.Visibility = Visibility.Visible ;
                ShowAlertBox("неуспешно логирање");
            }

        }

        private void ShowAlertBox(string Alert)
        {

            AlertLabel.Content = Alert;
            if(AlertPanel.Margin.Bottom == -54)
            {
                AlertPanel.Margin = new Thickness(-2, 0, 1, 0);
            }

        }

       
    }
}
