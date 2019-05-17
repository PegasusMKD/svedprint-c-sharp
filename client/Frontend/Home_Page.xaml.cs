using System.Collections.Generic;
using System.Linq;
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

            ucenici = result.ConvertAll(x => new Ucenik(x));
            KlasenKlasa.PopulateSmeroviFromUcenici(ucenici);
            
        }

        private void SettingsImg_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main, this);
        }

        private void MainImgClicked(object sender, MouseButtonEventArgs e)
        {
           //Main.Content = loginPage;
           Main.Content = new Oceni(Main,this);
        }

        private void PrintImgClicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new PrintFrame(Main, this);
        }
    }
}
