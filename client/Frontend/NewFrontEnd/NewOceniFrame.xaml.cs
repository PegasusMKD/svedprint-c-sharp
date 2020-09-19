using Frontend.NewFrontEnd;
using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für NewOceniFrame.xaml
    /// </summary>
    public partial class NewOceniFrame : Page
    {
        List<OcenkaBox> Ocenki = new List<OcenkaBox>();
        public NewOceniFrame(Student s, List<SubjectOrientation> subjectOrientations)
        {
            InitializeComponent();
            
            title.Content = s.firstName + s.lastName;
            title.Content = s.subjectOrientation.shortName;

            Smer_cb.ItemsSource = subjectOrientations.Select(x => x.shortName).ToList();
            Smer_cb.SelectedItem = s.subjectOrientation.shortName;
            int ctr = 0;

            try
            {
                //  Block of code to try
                List<string> Predmeti = subjectOrientations.Find(x=>x.shortName == s.subjectOrientation.shortName).subjects;
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
