using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;


namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für NewOceniFrame.xaml
    /// </summary>


    public partial class NewOceniFrame : Page
    {
        public Student CurrentStudent { get; set; }
        public NewOceniFrame(Student s)
        {
            InitializeComponent();

            CurrentStudent = s;
            title.DataContext = CurrentStudent;
            personalDataTabItem.DataContext = CurrentStudent;
            //title.Content = CurrentStudent.firstName;
            
            foreach(var x in CurrentStudent.GetType().GetProperties())
            {
                if(Attribute.IsDefined(x, typeof(PersonalDataAttribute)))
                {
                    personalDataTabItemFields.Children.Add(new Label() { Content = x.GetValue(CurrentStudent) });
                }
            }
        }
    }
}
