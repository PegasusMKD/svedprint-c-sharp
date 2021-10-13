using MiddlewareRevisited.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Frontend.NewFrontEnd.DesignModel
{
    class DesignMenu : DesignModel
    {
        private NewOceniFrame childFrame;
        private Frame parentFrame;
        private User currentUser;
        public DesignMenu(List<Student> elements, User User, ref Frame ParentFrame)
        {
            Element = new ListView();
            ScrollViewer.SetHorizontalScrollBarVisibility(Element, ScrollBarVisibility.Hidden);
            parentFrame = ParentFrame;
            currentUser = User;


            (Element as ListView).ItemsSource = elements.Select(element => {
                Label lbl = (Label)new MenuLabel($"{element.firstName} {element.lastName}").Element;
                lbl.MouseLeftButtonDown += (sender, e) => Label_Clicked(sender, e, element);
                return lbl;
            });

            if (elements.Count == 0) return;
            else childFrame = new NewOceniFrame(elements[0], User);
            parentFrame.Navigate(childFrame);
        }

        private void Label_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e, Student student)
        {
            Label lbl = (Label)sender;
            //MessageBox.Show("clicked");
            //Source_Frame.Content = Target;
            if (childFrame == null) childFrame = new NewOceniFrame(student, currentUser);
            else childFrame.init(student, currentUser);
            parentFrame.Navigate(childFrame);
        }
    }
}
