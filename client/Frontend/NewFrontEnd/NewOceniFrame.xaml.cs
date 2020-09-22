using Frontend.NewFrontEnd;
using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für NewOceniFrame.xaml
    /// </summary>


    public partial class NewOceniFrame : Page
    {
        public Student CurrentStudent { get; set; }
        private User currentUser;
        List<OcenkaBox> Ocenki = new List<OcenkaBox>();
        private bool shouldUpdate;


        public NewOceniFrame(Student s, List<SubjectOrientation> subjectOrientations, User u)
        {
            InitializeComponent();

            shouldUpdate = false;
            CurrentStudent = s;
            currentUser = u;
            title.DataContext = CurrentStudent;
            personalDataTabItem.DataContext = CurrentStudent;
            MouseLeave += new MouseEventHandler(async (obj, e) =>
            {
                    CurrentStudent = await MiddlewareRevisited.Controllers.Student.updateStudent(CurrentStudent, currentUser);
                    shouldUpdate = false;

            });
            //title.Content = CurrentStudent.firstName;

            foreach (var x in CurrentStudent.GetType().GetProperties())
            {
                DockPanel dockPanel = new DockPanel()
                {
                    LastChildFill = true
                };
                if (Attribute.IsDefined(x, typeof(PersonalDataAttribute)))
                {

                    var txt = new TextBox();
                    Binding binding = new Binding(x.Name) { UpdateSourceTrigger = UpdateSourceTrigger.Default, Mode = BindingMode.TwoWay };
                    txt.SetBinding(TextBox.TextProperty, binding);
                    //txt.TextChanged += new TextChangedEventHandler((o, e) => { shouldUpdate = true; });

                    var lbl = new Label();
                    lbl.Content = x.CustomAttributes.FirstOrDefault().ConstructorArguments[0].Value;

                    DockPanel.SetDock(lbl, Dock.Left);
                    dockPanel.Children.Add(lbl);
                    DockPanel.SetDock(txt, Dock.Right);
                    dockPanel.Children.Add(txt);

                    personalDataTabItemFields.Children.Add(dockPanel);
                }
            }
            title.Content = $"{s.firstName} {s.lastName}";
            //title.Content = s.subjectOrientation.shortName;

            Smer_cb.ItemsSource = subjectOrientations.Select(x => x.shortName).ToList();
            Smer_cb.SelectedItem = s.subjectOrientation.shortName;
            int ctr = 0;

            try
            {
                //  Block of code to try
                List<string> Predmeti = subjectOrientations.Find(x => x.shortName == s.subjectOrientation.shortName).subjects;
                foreach (int ocenka in s.grades)
                {
                    Ocenki.Add(new OcenkaBox(ocenka, Predmeti[ctr++]));
                    unigrid.Children.Add(Ocenki.Last().GetModel());
                }
            }
            catch (Exception e)
            {
                //  Block of code to handle errors
            }

        }

        public List<int> GetOcenki()
        {
            return Ocenki.Select(x => x.GetOcenka()).ToList();
        }
    }
}
