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
        public Settings(Frame m, Page homepage)
        {
            InitializeComponent();
            Main = m;
            HomePage = homepage;
            LoadListView();
        }

        string[] Menuitems = { "Групи", "Ученици", "Кориснички податоци" , "Просек", "Админ" };
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

            st.MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) => MouseLeftButtonDown(sender, e, brojDn + 1));
            st.MouseEnter += new MouseEventHandler(MouseEnter);
            st.MouseLeave += new MouseEventHandler(MouseLeave);

            return st;
        }

        object ClickedMenuItem;
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            DockPanel st = (DockPanel)sender;
            if (ClickedMenuItem != sender) st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e, int i)
        {
            if (ClickedMenuItem != null)
            {
                DockPanel st2 = (DockPanel)ClickedMenuItem;
                st2.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
            DockPanel st = (DockPanel)sender;
            st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(237, 106, 61));
            ClickedMenuItem = sender;

            Title.Content = Menuitems[i - 1];
        }


    }
}
