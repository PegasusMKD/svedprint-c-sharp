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


        public NewOceniFrame(Student s,  User u)
        {
            InitializeComponent();
            init(s, u);

            MouseLeave += new MouseEventHandler(async (obj, e) =>
            {
                //CurrentStudent = await MiddlewareRevisited.Controllers.Student.updateStudent(CurrentStudent, currentUser);
                shouldUpdate = false;

            });
        }

        public void init(Student s, User u)
        {
            shouldUpdate = false;
            CurrentStudent = s;
            currentUser = u;
            title.DataContext = CurrentStudent;
            personalDataTabItem.DataContext = CurrentStudent;
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

            Smer_cb.ItemsSource = currentUser.schoolClass.subjectOrientations.Select(x => x.shortName).ToList();
            Smer_cb.SelectedItem = s.subjectOrientation.shortName;

            //  Block of code to try
            List<string> Predmeti = currentUser.schoolClass.subjectOrientations.Find(x => x.shortName == s.subjectOrientation.shortName).subjects;

            Ocenki.Clear();
            unigrid.Children.Clear();
            OcenkaBox tmp;

            //Debug.Assert(s.grades.Count == Predmeti.Count);
            int minCount = Math.Min(s.grades.Count, Predmeti.Count); // temporary fix, fix asap

            for (int i = 0; i < minCount; i++)
            {
                int ocenka = s.grades[i];
                tmp = new OcenkaBox(ocenka, Predmeti[i]);

                Ocenki.Add(tmp);
                unigrid.Children.Add(tmp.GetModel());
            }
        }

        public List<int> GetOcenki()
        {
            return Ocenki.Select(x => x.GetOcenka()).ToList();
        }
    }
}
