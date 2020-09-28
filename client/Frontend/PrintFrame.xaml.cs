using MiddlewareRevisited.Models.PrintModels;
using MiddlewareRevisited.Models.PrintModels.Printer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für PrintFrame.xaml
    /// </summary>
    public partial class PrintFrame : Page
    {
        Frame Main;
        Page HomePage;
        PrintIterator<ReportCard> iteratorContainer;
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
            List<string> filenames = new List<string>() { "./Models/PrintModels/ReportCard/rsz_1front.jpg", "./Models/PrintModels/ReportCard/rsz_1back.jpg" };
            this.iteratorContainer = new PrintIterator<ReportCard>(ref Home_Page.students_static, Home_Page.currentUser_static, filenames);
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

        string[] Menuitems = { "Свидетелство", "Главна книга", "Предна страна на главна книга", "Главна книга за диплома", "Диплома" };
        private void LoadListView()
        {
            // ne treba da gi dava opciite za diplomi za tie sto ne se cetvrta
            //int n = Menuitems.Count();
            //if(Home_Page.KlasenKlasa._paralelka.Split('-')[0] != "IV")
            //{
            //    n = Menuitems.Count(x => !x.ToLower().Contains("диплома"));
            //}
            //for (int i = 0; i < n; i++)
            //{
            //    Menu.Items.Add(MenuDP(Menuitems[i], i));
            //}
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
            tx.VerticalAlignment = System.Windows.VerticalAlignment.Center;
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
            int offsetx = int.Parse(X_offset.Text);
            int offsety = int.Parse(Y_offset.Text);
            int val = combobox_printer.SelectedIndex;
            List<int> studentsToPrint = getIdxOfStudentsToPrint(studentsToPrint);

            Task.Factory.StartNew(() =>
            {
                IEnumerable<dynamic> printIterator = this.iteratorContainer.printIterator(offsetx, offsety);
                int ctr = 0;
                foreach (dynamic reportCard in printIterator)
                {
                    ctr++;
                    if (!studentsToPrint.Contains(ctr)) continue;
                    Printer printer = new Printer();
                    printer.print(reportCard[0].Clone(), val);

                }
            });

        }

        private List<int> getIdxOfStudentsToPrint(List<int> studentsToPrint)
        {
            List<int> studentsToPrint = new List<int>();
            string[] studentsToPrintStrings = uceniciToPrint.Text.Split(',');
            foreach (string student in studentsToPrintStrings)
            {
                if (student.Contains("-"))
                {
                    string[] rangeBounds = student.Split('-');
                    studentsToPrint.AddRange(Enumerable.Range(int.Parse(rangeBounds[0]), int.Parse(rangeBounds[1])));
                }
                else studentsToPrint.Add(int.Parse(student));
            }
            return studentsToPrint;
        }

        private void Btn_celprint_Clicked(object sender, RoutedEventArgs e)
        {
            //int offsetx, offsety;
            //offsetx = int.Parse(X_offset.Text);
            //offsety = int.Parse(Y_offset.Text);
            //switch (Menu.SelectedIndex)
            //{
            //    case 0:
            //        Print.PrintSveditelstva(Home_Page.ucenici, /*uceniks*/ Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
            //        break;
            //    case 1:
            //        Print.PrintGlavnaKniga(Home_Page.ucenici, Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
            //        break;
            //    case 2:
            //        Print.PrintPrednaStranaGK(Home_Page.ucenici, Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
            //        break;
            //    case 3:
            //        Print.PrintGkDiploma(Home_Page.ucenici, Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
            //        break;
            //    case 4:
            //        Print.PrintDiploma(Home_Page.ucenici, Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex, offsetx, offsety);
            //        break;
            //}
        }
    }
}
