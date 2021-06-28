using MiddlewareRevisited.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Home_Page.xaml
    /// </summary>
    public partial class Home_Page : Page
    {

        Frame Main;
        private MiddlewareRevisited.Models.User currentUser;
        private List<Student> students;
        private List<SubjectOrientation> subjectOrientations;
        private SchoolClass schoolClass;

        public static List<Student> students_static;
        public static User currentUser_static;

        public string currentUserData { get; set; }


        public Home_Page(Frame m, User user)
        {
            InitializeComponent();
            Main = m;
            currentUser = user;
            schoolClass = currentUser.schoolClass;
            subjectOrientations = schoolClass.subjectOrientations;
            MiddlewareRevisited.Controllers.Student.GetAllStudentsFullAsync(user).ContinueWith(st => students = st.Result).Wait();

            students_static = students;
            currentUser_static = currentUser;

            currentUserData = $"{currentUser.firstName} {(string.IsNullOrWhiteSpace(currentUser.middleName) ? "" : $"{currentUser.middleName}-")}{currentUser.lastName}, {currentUser.schoolClass.name}";

            DataContext = this;

            SettingsImg.MouseLeftButtonDown += new MouseButtonEventHandler(SettingsImg_Clicked);

        }

        private void SettingsImg_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main, this, ref subjectOrientations, ref currentUser, ref students);
        }

        private void MainImgClicked(object sender, MouseButtonEventArgs e)
        {
            schoolClass = currentUser.schoolClass;
            if (students.Count == 0)
            { MessageBox.Show("Нема пополнето ученици"); return; }
            if (subjectOrientations.Count == 0)
            { MessageBox.Show("Нема Смерови"); return; }
            else
            {
                //Main.Content = new Oceni(Main, this);
                //Main.Content = new Oceni(schoolClass);
                //Main.Navigate(new Oceni(schoolClass));
                //if (gradesPage == null) gradesPage = new Oceni(currentUser);
                //NavigationService.Navigate(gradesPage);
                //NavigationService.Navigate(new NewOceniFrame(students));
                NavigationService.Navigate(new MenuFrame(currentUser));
            }
        }

        private void PrintImgClicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PrintFrame(Main, this);
        }
    }
}
