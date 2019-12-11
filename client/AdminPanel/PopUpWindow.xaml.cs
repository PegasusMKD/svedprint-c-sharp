using AdminPanel.Middleware.Models;
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
using System.Windows.Shapes;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : Window
    {

        List<string> GetParalelki()
        {
            List<string> Paralelki = new List<string>();
            foreach (string god in new string[] { "I", "II", "III", "IV" })
            {
                for (int i = 1; i <= 8; i++)
                {
                    Paralelki.Add(god + '-' + i.ToString());
                }
            }
            return Paralelki;
        }
        public string size { get; set; }

        public bool return1 = false;

        public PopUpWindow(List<Ucenik> SelectedStudents)
        {
            InitializeComponent();

            DataContext = this;

            size = SelectedStudents.Count().ToString();
            ParalelkiCB.ItemsSource = GetParalelki();
            ParalelkiCB.SelectedIndex = 16;

        }

        private void ConfirmBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.return1 = true;
            this.Close();
        }
    }
}
