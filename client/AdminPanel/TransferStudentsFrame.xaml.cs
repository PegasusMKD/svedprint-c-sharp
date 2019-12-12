using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using AdminPanel;
using AdminPanel.Middleware.Models;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für TransferStudentsFrame.xaml
    /// </summary>
    public partial class TransferStudentsFrame : Page
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

        List<Ucenik> students;
        List<Ucenik> SelectedStudents = new List<Ucenik>();
        Admin admin;
        public TransferStudentsFrame(Admin admin,Dictionary<string, List<Klasen>> users)
        {
            InitializeComponent();
            ParalelkiCB.ItemsSource = GetParalelki().ToArray();
            students = Middleware.Controllers.Ucenik.RetrieveStudents(admin);
            this.admin = admin;
            DataContext = this;
            StudentsList.ItemsSource = students;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)(sender);
            int index = int.Parse(cb.Tag.ToString()) - 1;
            if (cb.IsChecked.Value == true)
            {
                SelectedStudents.Add(students[index]);
            }
            else
            {
                SelectedStudents.Remove(students[index]);
            }

        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox SearchBox = (TextBox)(sender);
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(StudentsList.ItemsSource);
            collectionView.Filter = (x) =>
            {
                if (String.IsNullOrEmpty(SearchBox.Text)) return true;
                bool retval = false;
                foreach (var txt in SearchBox.Text.Split(' '))
                {
                    retval |= 
                        (x as Ucenik).Ime.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (x as Ucenik).Prezime.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (x as Ucenik).Tatko.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (x as Ucenik).Broj.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return retval;
            };

            StudentsList.ItemsSource = collectionView;
        }

        private void PremestiBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(students[0].Ime);
            PopUpWindow PopUp = new PopUpWindow(students);
            PopUp.Show();
            PopUp.Closed += PopUp_Window_Closed;
        }

        private void PopUp_Window_Closed(object sender, EventArgs e)
        {
            StudentsList.Items.Refresh();

            PopUpWindow PopUp = (PopUpWindow)(sender);
            if (PopUp.return1)
            {
                string _class = PopUp.ParalelkiCB.SelectedItem.ToString();
                Thread t = new Thread(() => Middleware.Controllers.Ucenik.TransferStudentsClasses(students, admin, _class))
                {
                    IsBackground = true
                };
                t.Start();
            }
            else Console.WriteLine("Or not :(");
        }
    }


}
