using Frontend.NewFrontEnd;
using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaktionslogik für MenuFrame.xaml
    /// </summary>
    public partial class MenuFrame : Page
    {
        // instead of making new frames, reuse one frame
        private NewOceniFrame currentStudentDetailsPage;
        public static User CurrentUser { get; set; }
        public MenuFrame(User user)
        {
            InitializeComponent();

            CurrentUser = user;
            Dictionary<Student, Page> elements = new Dictionary<Student, Page>();


            int ctr = 0;
            foreach(Student s in user.schoolClass.students)
            {
                elements.Add(s, currentStudentDetailsPage);
                ctr++;
            }

            DesignMenu ListLayer = new DesignMenu(elements, user, ref Source);
            DesignModel Model = ListLayer;
            MainGrid.Children.Add(Model.GetModel());
        }
    }
}
