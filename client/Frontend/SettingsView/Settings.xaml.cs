using MiddlewareRevisited.Models;
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

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {

        Frame Main;
        Page HomePage;
        List<Page> ListPages = new List<Page>();
        public Settings(Frame m, Page homepage, ref List<SubjectOrientation> subjectOrientations, ref User currentUser, ref List<Student> students)
        {
            InitializeComponent();
            Main = m;
            HomePage = homepage;
            LoadListView();
            Title.Content = Menuitems[0];

            LoadMainList();
            ListPages.Add(new Smerovi_Page(ref subjectOrientations, ref currentUser, ref students));
            ListPages.Add(new EditUcenici_Page(ref subjectOrientations, ref currentUser, ref students));
            //ListPages.Add(new Prosek_Frame());
            Settings_Frame.Content = ListPages[0];
        }

        private void LoadMainList()
        {
            Border border = new Border();
            border.Height = 50;
            border.Margin = new Thickness(20, 0, 20, 10);
            border.VerticalAlignment = VerticalAlignment.Center;
            border.Background = System.Windows.Media.Brushes.White;
            border.BorderThickness = new Thickness(2);
            border.CornerRadius = new System.Windows.CornerRadius(10);
            Label lbl = new Label();
        }

        string[] Menuitems = { "Смерови", "Ученици", "Просек", "Админ" };
        private void LoadListView()
        {
            for (int i = 0; i < Menuitems.Length; i++)
            {
                Menu.Items.Add(MenuDP(Menuitems[i], i));
            }
        }

        private void Home_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = HomePage;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private DockPanel MenuDP(string Name, int brojDn)
        {
            DockPanel st = new DockPanel();
            Label tx = new Label();
            tx.Content = Name;
            st.Children.Add(tx);
            st.Height = 50;
            st.Width = 400;
            st.MaxWidth = 400;
            st.HorizontalAlignment = HorizontalAlignment.Left;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            tx.FontSize = 22;
            tx.FontFamily = new System.Windows.Media.FontFamily("Arial Black");
            tx.Foreground = System.Windows.Media.Brushes.White;
            tx.VerticalAlignment = VerticalAlignment.Center;

            st.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => MenuMouseClick(sender, e, brojDn));
            st.MouseEnter += new MouseEventHandler(MenuMouseEnter);
            st.MouseLeave += new MouseEventHandler(MenuMouseLeave);

            return st;
        }

        object ClickedMenuItem;
        private void MenuMouseEnter(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
        }

        private void MenuMouseLeave(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            if (ClickedMenuItem != sender) st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
        }

        private void MenuMouseClick(object sender, MouseButtonEventArgs e, int i)
        {
            if (ClickedMenuItem != null)
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;

            if(i<2)
            Settings_Frame.Content = ListPages[i];


            Title.Content = Menuitems[i];
        }


    }
}
