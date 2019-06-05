using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Middleware;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Home_Page.xaml
    /// </summary>
    public partial class Home_Page : Page
    {

        Frame Main;
        Page loginPage;
        public static Klasen KlasenKlasa;
        public static List<Ucenik> ucenici;
        public static List<Dictionary<string, string>> result;
        public static Dictionary<string, Smer> smerovi;

        public Home_Page(Frame m ,  Page loginpage , Klasen Klasen)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;
            KlasenKlasa = Klasen;

            SettingsImg.MouseLeftButtonDown += new MouseButtonEventHandler(SettingsImg_Clicked);


            result = Requests.GetData(new Dictionary<string, string>() {
                {RequestParameters.token, Klasen._token } 
            }, RequestScopes.GetParalelka);

            if(result.Count == 0)
            {
               /// MessageBox.Show("404");
            }

            ucenici = result.ConvertAll(x => new Ucenik(x));
            var uc = ucenici.OrderBy(x => x._prezime).ThenBy( x=> x._ime);
            ucenici = uc.ToList();
            if(KlasenKlasa._smerovi != "")
            {
                KlasenKlasa.PopulateSmerovi(ucenici);
            }
            else KlasenKlasa.PopulateSmeroviFromUcenici(ucenici);
            
        }

        private void SettingsImg_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main, this);
        }

        private void MainImgClicked(object sender, MouseButtonEventArgs e)
        {
            //Main.Content = loginPage;
            if (ucenici.Count == 0)
            { MessageBox.Show("Нема пополнето ученици"); return; }
            if (KlasenKlasa._p._smerovi.Count == 0)
            { MessageBox.Show("Нема Смерови"); return; }
            else Main.Content = new Oceni(Main, this);
        }

        private void PrintImgClicked(object sender, MouseButtonEventArgs e)
        {
            //Main.Content = new PrintFrame(Main, this);
        }
    }
}
