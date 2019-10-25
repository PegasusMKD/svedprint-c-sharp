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

        List<ProfesoriInfo> PI;
        List<Pole> Items;
        ComboBox ParalelkaCB;
        public ProfesoriFrame()
        {
            InitializeComponent();
            //CB_Paralelki.ItemsSource = GetParalelki().ToArray();
            Pole ParalelkiPole = new Pole("Paralelki", GetParalelki().ToArray(), "parametar", "Small");
            ParalelkaCB = ParalelkiPole.GetComboBox();
            ParalelkaST.Children.Add(ParalelkaCB);

            PI = new List<ProfesoriInfo>();
            PI.Add(new ProfesoriInfo("user1", "pw", "ime1", "prezime1", "I-5"));
            PI.Add(new ProfesoriInfo("user2", "pw", "ime2", "prezime2", "II-1"));
            PI.Add(new ProfesoriInfo("user3", "pw", "ime3", "prezime3", "III-4"));
            PI.Add(new ProfesoriInfo("user4", "pw", "ime4", "prezime4", "IV-1"));
            CB_Profesori.ItemsSource = PI.Select(x => x.getFullName());


            Items = new List<Pole>
                {

                    new Pole ("Корисничко име" , new string[] { PI[0].Username } , "parametar"),
                    new Pole ( "Лозинка" ,  new string[] { PI[0].Password } , "" , "PW"),
                    new Pole ("Име" , new string[] { PI[0].Ime } , "parametar"  ),
                    new Pole ("Презиме" , new string[] { PI[0].Prezime } , "parametar"  ),
                    //new Pole ("PMA" , new string[] { "mat" , "mak" , "ger" , "asdf" , "fdas" , "dfass"  } , "parametar" , "Predmeti" , "dfass" ),

                };

            CB_Profesori.SelectedIndex = 0;
        }

        private void CB_Profesori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Ugrid.Children.Clear();
            ParalelkaCB.SelectedItem = PI[CB_Profesori.SelectedIndex].Paralelka;

            Items = new List<Pole>
                {

                    new Pole ("Корисничко име" , new string[] { PI[CB_Profesori.SelectedIndex].Username } , "parametar"),
                    new Pole ( "Лозинка" ,  new string[] { PI[CB_Profesori.SelectedIndex].Password } , "" , "PW"),
                    new Pole ("Име" , new string[] { PI[CB_Profesori.SelectedIndex].Ime } , "parametar"  ),
                    new Pole ("Презиме" , new string[] { PI[CB_Profesori.SelectedIndex].Prezime } , "parametar"  ),
                    //new Pole ("PMA" , new string[] { "mat" , "mak" , "ger" , "asdf" , "fdas" , "dfass"  } , "parametar" , "Predmeti" , "dfass" ),

                };
            foreach (Pole item in Items)
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
                    new Pole ( "Лозинка" ,  new string[] { "Password" } , "" , "PW"),
                    new Pole ("Име" , new string[] { "Име" } , "parametar"  ),
                    new Pole ("Презиме" , new string[] { "Презиме" } , "parametar"  ),
                    //new Pole ("PMA" , new string[] { "mat" , "mak" , "ger" , "asdf" , "fdas" , "dfass"  } , "parametar" , "Predmeti" , "dfass" ),

                };
            }
        }
    }

    public class ProfesoriInfo
    {
        public string Username;
        public string Password;
        public string Ime;
        public string Prezime;
        public string Paralelka;

        public ProfesoriInfo(string username, string password, string ime, string prezime, string paralelka)
        {
            Username = username;
            Password = password;
            Ime = ime;
            Prezime = prezime;
            Paralelka = paralelka;
        }

        public string getFullName()
        {
            return Ime + " " + Prezime;
        }
    }
}
