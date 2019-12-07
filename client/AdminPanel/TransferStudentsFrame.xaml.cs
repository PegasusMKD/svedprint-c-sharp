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

        List<Ucenik> students = new List<Ucenik>();
        List<Ucenik> SelectedStudents = new List<Ucenik>();
        public TransferStudentsFrame(Admin admin,Dictionary<string, List<Klasen>> users)
        {
            InitializeComponent();
            ParalelkiCB.ItemsSource = GetParalelki().ToArray();

            DataContext = this;

            students.Add(new Ucenik { Ime = "luka", Prezime = "jovanovski", Broj = "1", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "fico", Prezime = "jovanov", Broj = "2", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "darijan", Prezime = "sekerov", Broj = "3", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "bojan", Prezime = "suklev", Broj = "4", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "kocka", Prezime = "kostevski", Broj = "5", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "ivan", Prezime = "kocevski", Broj = "6", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "ime7", Prezime = "prezime", Broj = "7", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "ime8", Prezime = "prezime", Broj = "8", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "ime9", Prezime = "prezime", Broj = "9", Tatko = "tatkovo" });
            students.Add(new Ucenik { Ime = "ime10", Prezime = "prezime", Broj = "10", Tatko = "tatkovo" });

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

            //Console.WriteLine(SelectedStudents.Last().Ime);
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
            PopUpWindow PopUp = new PopUpWindow(SelectedStudents);
            PopUp.Show();
            PopUp.Closed += PopUp_Window_Closed;
        }

        private void PopUp_Window_Closed(object sender, EventArgs e)
        {
            StudentsList.Items.Refresh();

            PopUpWindow PopUp = (PopUpWindow)(sender);
            if (PopUp.return1)
            {
               
            }
            //StudentsList.ItemsSource = students;
        }
    }


}
