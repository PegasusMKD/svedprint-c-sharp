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
        /// <summary>
        /// Keeps track of the desired class
        /// <para>Should be changed to an index of a list or something similar, instead of a TextBox</para>
        /// </summary>
        public string Class = "";
        public string ClassView { get { return Class; } set { if (value != Class) Class = value; } }

        public StudentsWindow(Admin admin, List<Ucenik> students)
        {
            InitializeComponent();
            
            this.admin = admin;
            students = students.OrderBy(x => x.Paralelka).ThenBy(x => x.Broj).ToList();
            //students = students.OrderBy(x => int.Parse(x.Paralelka.Split('-')[1])).ToList();
            this.students = students;
            StudentsList.ItemsSource = students;
            
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(StudentsList.ItemsSource);
            collectionView.Filter = (x) =>
            {
                if (String.IsNullOrEmpty(txtSearch.Text)) return true;
                bool retval = false;
                foreach (var txt in txtSearch.Text.Split(' '))
                {
                    retval |= (x as Ucenik).Ime.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (x as Ucenik).Prezime.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        (x as Ucenik).Paralelka.IndexOf(txt, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return retval;
            };

            DataContext = this;
        }

        private void TxtSearch_Search(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(StudentsList.ItemsSource).Refresh();
        }

        private void Update_Students(object sender, RoutedEventArgs e)
        {
            Middleware.Controllers.Ucenik.TransferStudentsClasses(students, admin, Class);
        }

    }
}