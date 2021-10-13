using Frontend.NewFrontEnd.DesignModel;
using MiddlewareRevisited.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MenuFrame.xaml
    /// </summary>
    public partial class MenuFrame : Page
    {
        // instead of making new frames, reuse one frame
        private List<Student> students;
        public static User CurrentUser { get; set; }
        public MenuFrame(User user)
        {
            InitializeComponent();

            CurrentUser = user;
            students = user.schoolClass.students;
            MiddlewareRevisited.Controllers.Student.GetAllStudentsShortAsync().ContinueWith(val => students = val.Result).Wait();

            DesignMenu ListLayer = new DesignMenu(students, user, ref Source);
            DesignModel Model = ListLayer;
            MainGrid.Children.Add(Model.Element);
        }
    }
}
