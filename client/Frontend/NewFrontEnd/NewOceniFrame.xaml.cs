using MiddlewareRevisited.Models;
using System.Windows.Controls;


namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für NewOceniFrame.xaml
    /// </summary>
    public partial class NewOceniFrame : Page
    {
        public NewOceniFrame(Student s)
        {
            InitializeComponent();
            
            title.Content = s.firstName + s.lastName;
        }
    }
}
