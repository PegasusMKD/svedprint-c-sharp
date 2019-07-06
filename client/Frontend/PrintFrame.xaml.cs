using Middleware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für PrintFrame.xaml
    /// </summary>
    public partial class PrintFrame : Page
    {
        Frame Main;
        Page HomePage;
        public PrintFrame(Frame m, Page homepage)
        {
            if (!Properties.Settings.Default.IsPrintAllowed) return; // print block, treba test
            InitializeComponent();
            // SvedImg.Source = new BitmapImage(new Uri(new Uri(Directory.GetCurrentDirectory(), UriKind.Absolute), new Uri(@"C:\Users\lukaj\Documents\front-0.png", UriKind.Relative)));
            Main = m;
            HomePage = homepage;
            LoadPrinterBox();
            LoadListView();
            Menu.SelectedIndex = 0;
        }

        private void LoadPrinterBox()
        {
            combobox_printer.ItemsSource = PrinterSettings.InstalledPrinters;
            if (combobox_printer.HasItems)
            {
                combobox_printer.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Програмата не пронајде принтер." + Environment.NewLine + "Ве молиме поврзете принтер!", "Грешка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        string[] Menuitems = { "Свидетелство", "Главна книга", "Главна книга за диплома", "Диплома" };
        private void LoadListView()
        {

            for (int i = 0; i < Menuitems.Length; i++)
            {
                Menu.Items.Add(MenuDP(Menuitems[i], i));
            }
        }


        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private DockPanel MenuDP(string Name, int brojDn)
        {
            DockPanel st = new DockPanel();
            System.Windows.Controls.Label tx = new System.Windows.Controls.Label();
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
            if (ClickedMenuItem != sender)
            {
                st.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(165, 166, 140));
            }
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

        private void Home_Button_Clicked(object sender, MouseButtonEventArgs e)
        {
            Main.Content = HomePage;
        }

        private void Btn_edinecnoprint_Clicked(object sender, RoutedEventArgs e)
        {
            int offsetx, offsety;
            offsetx = int.Parse(X_offset.Text);
            offsety = int.Parse(Y_offset.Text);
            List<int> toPrint = uceniciToPrint.Text.Split(',').ToList().ConvertAll(x => int.Parse(x));
            // fix dodeka broevite vo dnevnik se isti
            List<Ucenik> uceniks = Home_Page.ucenici.Where(x => toPrint.Contains(Home_Page.ucenici.IndexOf(x) + 1)).ToList();
            // posle fix za razlicni broevi
            //List<Ucenik> uceniks = Home_Page.ucenici.Where(x => toPrint.Contains(x._broj)).ToList();

            //Middleware.Print.PrintSveditelstva(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
            switch (Menu.SelectedIndex)
            {
                case 0:
                    Print.PrintSveditelstva(Home_Page.ucenici, uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
                case 1:
                    Print.PrintGlavnaKniga(Home_Page.ucenici, uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
                case 2:
                    Print.PrintGkDiploma(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
                case 3:
                    Print.PrintDiploma(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
            }
        }

        private void Btn_celprint_Clicked(object sender, RoutedEventArgs e)
        {
            int offsetx, offsety;
            offsetx = int.Parse(X_offset.Text);
            offsety = int.Parse(Y_offset.Text);
            switch (Menu.SelectedIndex)
            {
                case 0:
                    //Middleware.Print.PrintSveditelstva(Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);


                    for (int i = 0; i < Home_Page.ucenici.Count(); i += 5)
                    {

                        List<Ucenik> uceniks = new List<Ucenik>();
                        for (int j = 0; j < 5; j++)
                        {
                            uceniks.Add(Home_Page.ucenici[i + j]);
                            if (i + j == Home_Page.ucenici.Count() - 1) break;
                        }
                        Print.PrintSveditelstva(Home_Page.ucenici, uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);

                    }


                    break;
                case 1:
                    Print.PrintGlavnaKniga(Home_Page.ucenici, Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
                case 2:
                    Print.PrintDiploma(Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
                case 3:
                    Print.PrintDiploma(Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
                    break;
            }
        }
    }
}
