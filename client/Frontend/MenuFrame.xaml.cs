using Frontend.NewFrontEnd;
using MiddlewareRevisited.Models;
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

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für MenuFrame.xaml
    /// </summary>
    public partial class MenuFrame : Page
    {
        public MenuFrame(User user)
        {
            InitializeComponent();

            List<KeyValuePair<string, Page>> elements = new List<KeyValuePair<string, Page>>();


            foreach(Student s in user.schoolClass.students)
            {
                elements.Add(new KeyValuePair<string, Page>(s.lastName + " " + s.firstName, new NewOceniFrame(s)));
            }

            DesignMenu ListLayer = new DesignMenu(elements, ref Source);
            DesignModel Model = ListLayer;
            MainGrid.Children.Add(Model.GetModel());
        }
    }
}
