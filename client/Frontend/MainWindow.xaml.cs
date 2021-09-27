using System.Windows;


namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Main.Navigate(new Login_Page(Main));
            //Main.Content = new Login_Page(Main);
        }

    }
}
