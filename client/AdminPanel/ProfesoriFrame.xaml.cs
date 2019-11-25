using AdminPanel.Middleware.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

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
        }

        private void CB_Profesori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ugrid.Children.Clear();
            ParalelkaCB.SelectedItem = Profesori[CB_Profesori.SelectedIndex].Klas;

            if (Profesori[CB_Profesori.SelectedIndex].Polinja == null) Profesori[CB_Profesori.SelectedIndex].GetPolinja();


            foreach (Pole item in Profesori[CB_Profesori.SelectedIndex].Polinja)
            {
                Ugrid.Children.Add(item.GetPole());
            }
        }
    }

    public class ProfesorViewModel
    {
        public List<Pole> Items
        {
            get
            {
                return new List<Pole>
                {

                    new Pole ("Корисничко име" , new string[] { "ime prezime" } , "parametar"),
                    new Pole ( "Лозинка" ,  new string[] { "Password" } , "Password" , "PW"),
                    new Pole ("Име" , new string[] { "Име" } , "parametar"  ),
                    new Pole ("Презиме" , new string[] { "Презиме" } , "parametar"  ),
                    //new Pole ("PMA" , new string[] { "mat" , "mak" , "ger" , "asdf" , "fdas" , "dfass"  } , "parametar" , "Predmeti" , "dfass" ),

                };
            }
        }
    }
}
