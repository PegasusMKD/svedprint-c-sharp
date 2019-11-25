using AdminPanel.Middleware.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für ProfesoriFrame.xaml
    /// </summary>
    public partial class ProfesoriFrame : Page
    {
        List<string> GetParalelki()
        {
            List<string> Paralelki = new List<string>();
            foreach (string god in new string[] { "I", "II", "III", "IV" })
            {
                for (int i = 1; i <= 8; i++)
                {
                    Paralelki.Add(god + '-' + i.ToString());
                }
            }
            Paralelki.Add("Нема");
            return Paralelki;
        }


        public Admin admin;
        public Dictionary<string, List<Klasen>> users;
        List<Klasen> Profesori = new List<Klasen>();

        ComboBox ParalelkaCB;

        public ProfesoriFrame(Admin admin , Dictionary<string, List<Klasen>> users)
        {
            InitializeComponent();

            this.admin = admin;
            this.users = users;

            Pole ParalelkiPole = new Pole("Paralelki", GetParalelki().ToArray(), "parametar", "Small");
            ParalelkaCB = ParalelkiPole.GetComboBox();
            ParalelkaST.Children.Add(ParalelkaCB);

            foreach(List<Klasen> klasni in users.Values)
            {
                Profesori.AddRange(klasni);
            }

            CB_Profesori.ItemsSource = Profesori.Select(x => x.Ime + " " + x.Prezime);

            CB_Profesori.SelectedIndex = 0;

            this.MouseLeave += ProfesoriFrame_MouseLeave;
        }

        private void CB_Profesori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ugrid.Children.Clear();

            if (!string.IsNullOrEmpty(Profesori[CB_Profesori.SelectedIndex].Klas)) ParalelkaCB.SelectedItem = Profesori[CB_Profesori.SelectedIndex].Klas;
            else ParalelkaCB.SelectedItem = "Нема";

            if (Profesori[CB_Profesori.SelectedIndex].Polinja == null) Profesori[CB_Profesori.SelectedIndex].GetPolinja(users);

            foreach (Pole item in Profesori[CB_Profesori.SelectedIndex].Polinja)
            {
                Ugrid.Children.Add(item.GetPole());
            }
        }

        private void ProfesoriFrame_MouseLeave(object sender, MouseEventArgs e)
        {
            Middleware.Controllers.Klasen.UpdateUsers(admin, users);
        }
        
        public void AddProfessor(object sender, MouseEventArgs e)
        {

        }

    }
}
