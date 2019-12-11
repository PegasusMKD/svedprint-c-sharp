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
            students = Middleware.Controllers.Ucenik.RetrieveStudents(admin);
            DataContext = this;
            //foreach(Ucenik student in tmp_students) students.Add(new Ucenik)
            //students.Add(new Ucenik { Ime = "ime1", Prezime = "prezime", Broj = "1", Tatko = "tatkovo" });
            
            StudentsList.ItemsSource = students;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)(sender);
            if(cb.IsChecked.Value == true)
            {
                SelectedStudents.Add(students[int.Parse(cb.Tag.ToString())-1]);
            }
            else
            {
                SelectedStudents.Remove(students[int.Parse(cb.Tag.ToString())-1]);
            }

            Console.WriteLine(SelectedStudents.Last().Ime);
        }
    }


}
