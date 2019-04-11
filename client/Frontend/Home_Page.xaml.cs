using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public static List<Dictionary<string, string>> result;
        public static Dictionary<string, Smer> smerovi;

        public Home_Page(Frame m ,  Page loginpage , Klasen Klasen)
        {
            InitializeComponent();
            Main = m;
            loginPage = loginpage;
            KlasenKlasa = Klasen;

            SettingsImg.MouseLeftButtonDown += new MouseButtonEventHandler(SettingsImg_Clicked);

            result =  Requests.GetData(new Dictionary<string, string>() {
                {RequestParameters.token, Klasen._token }
            }, RequestScopes.GetParalelka);

            getPredmeti();
            
        }

        private void SettingsImg_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = new Settings(Main, this);
        }

        private void getPredmeti()
        {
            smerovi = new Dictionary<string, Smer>();

            foreach (var x in "ПМА,PMB,OHA,OHB,JUA,JUB".Split(','))
            {
                smerovi.Add(x, new Smer(Requests.GetData(new Dictionary<string, string>(){
                    { RequestParameters.token, KlasenKlasa._token},
                    { RequestParameters.smer, x }
                }, RequestScopes.GetPredmetiSmer)[0]["predmeti"].Split(',').ToList(), x));
            }
        }

        private void MouseEnter(object sender, MouseButtonEventArgs e)
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
