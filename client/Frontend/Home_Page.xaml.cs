using Middleware;
using MiddlewareRevisited.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Home_Page.xaml
    /// </summary>
    public partial class Home_Page : Page
    {

        Frame Main;
        public User currentUser;
        public static Klasen KlasenKlasa;
        public static List<Ucenik> ucenici;
        public static List<Dictionary<string, string>> result;
        public static Dictionary<string, Smer> smerovi;
        public string CurrentUserData { get; set; }
        public SchoolClass schoolClass;

        public Home_Page(Frame m, User user)
        {
            InitializeComponent();
            Main = m;
            currentUser = user;

            // userData = $"{Klasen._ime} {(string.IsNullOrWhiteSpace(Klasen._srednoIme) ? "" : $"{Klasen._srednoIme}-")}{Klasen._prezime}, {Klasen._paralelka}";

            // stringot dole desno
            CurrentUserData = $"{currentUser.firstName} {(string.IsNullOrWhiteSpace(currentUser.middleName) ? "" : $"{currentUser.middleName}-")}{currentUser.lastName}, {currentUser.schoolClass.name}";

            DataContext = this;

            SettingsImg.MouseLeftButtonDown += new MouseButtonEventHandler(SettingsImg_Clicked);

            /*
                        result = Requests.GetData(new Dictionary<string, string>() {
                            {RequestParameters.token, Klasen._token }
                        }, RequestScopes.GetParalelka);

            */            //ucenici = result.ConvertAll(x => new Ucenik(x));
            //KlasenKlasa.SetSmeroviPredmeti(KlasenKlasa._token);
/*            if (KlasenKlasa._paralelka.Split('-')[0] == "IV") {
                foreach (Ucenik ucenik in ucenici)
                {
                    if (ucenik._maturska == "")
                    {
                        Debug.WriteLine($"{ucenik._ime} {ucenik._prezime}");
                        continue;
                    }
                    ucenik.LoadMaturski(KlasenKlasa);
                }
            }

*/        }

        private void SettingsImg_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main, this);
        }

        private void MainImgClicked(object sender, MouseButtonEventArgs e)
        {
            schoolClass = currentUser.schoolClass;
            if (schoolClass.students.Count == 0)
            { MessageBox.Show("Нема пополнето ученици"); return; }
            if (schoolClass.subjectOrientations.Count == 0)
            { MessageBox.Show("Нема Смерови"); return; }
            else
            {
                //Main.Content = new Oceni(Main, this);
                //Main.Content = new Oceni(schoolClass);
                //Main.Navigate(new Oceni(schoolClass));
                NavigationService.Navigate(new Oceni(schoolClass));
            }
        }

        private void PrintImgClicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PrintFrame(Main, this);
        }
    }
}
