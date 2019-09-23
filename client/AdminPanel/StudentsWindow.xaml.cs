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

    public partial class StudentsWindow : Window
    {

        Admin admin;
        public List<Ucenik> students { get; set; }
        private List<Ucenik> l;

        public StudentsWindow(Admin admin, List<Ucenik> students)
        {
            InitializeComponent();
            
            this.admin = admin;
            students = students.OrderBy(x => x.Paralelka).ThenBy(x => x.Broj).ToList();
            //students = students.OrderBy(x => int.Parse(x.Paralelka.Split('-')[1])).ToList();
            this.students = students;
            l = students;
            // StudentsList.ItemsSource = students;

            DataContext = this;
        }

        private void TxtSearch_Search(object sender, KeyEventArgs e)
        {
            // if (e.Key != Key.Enter) return;
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                StudentsList.ItemsSource = new List<Ucenik>(l);
                return;
            }
            List<Ucenik> tmp = new List<Ucenik>();
            foreach(var x in l)
            {
                if(x.Ime.ToLower().Contains(txtSearch.Text.ToLower()) || x.Prezime.ToLower().Contains(txtSearch.Text.ToLower()) || x.Paralelka.Contains(txtSearch.Text))
                {
                    tmp.Add(x);
                }
            }
            StudentsList.ItemsSource = tmp;
        }
    }
}