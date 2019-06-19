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

        string[] Menuitems = { "Сведителство", "Главна Книга", "Постапки" };
        private void LoadListView()
        {

            for (int i = 0; i < Menuitems.Length; i++)
            {
                Menu.Items.Add(MenuDP(Menuitems[i], i));
            }
        }

        private Bitmap InsertText(string text, string imageFilePath)
        {

            PointF firstLocation = new PointF(900, 050);

            //string imageFilePath = @"C:\Users\lukaj\OneDrive\Desktop";
            Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(imageFilePath);// + @"\bk.png");//load the image file

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Font arialFont = new Font("Arial", 50, System.Drawing.FontStyle.Bold))
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    graphics.DrawString(text, arialFont, SystemBrushes.WindowText, firstLocation);
                }
            }

            //bitmap.Save(imageFilePath+ @"\bk2.jpeg");
            return bitmap;
        }

        private void Print()
        {
            string input = @"C:\Users\lukaj\OneDrive\Desktop\bk1.jpeg";
            PrintDialog printDialog = new PrintDialog();
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, args) =>
            {
                args.Graphics.DrawImage(InsertText("македонски", @"C:\Users\lukaj\OneDrive\Desktop\bk.jpeg"), args.PageBounds);
            };
            pd.OriginAtMargins = false;
            //pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters

            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                // MessageBox.Show(String.Format("{0}: {1}", i, PrinterSettings.InstalledPrinters[i]));
            }
            //int choice = 0;

            int choice = 3;
            pd.PrinterSettings.PrinterName = PrinterSettings.InstalledPrinters[choice];
            pd.DefaultPageSettings.PaperSize = pd.PrinterSettings.PaperSizes.Cast<PaperSize>().First<PaperSize>(size => size.Kind == PaperKind.A4);
            pd.DefaultPageSettings.Landscape = true;

            pd.Print();
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

        private void Btn_edinecnoprint_MouseUp(object sender, MouseButtonEventArgs e)
        {
            List<int> toPrint = uceniciToPrint.Text.Split(',').ToList().ConvertAll(x => int.Parse(x));
            // fix dodeka broevite vo dnevnik se isti
            List<Ucenik> uceniks = Home_Page.ucenici.Where(x => toPrint.Contains(Home_Page.ucenici.IndexOf(x) + 1)).ToList();
            // posle fix za razlicni broevi
            //List<Ucenik> uceniks = Home_Page.ucenici.Where(x => toPrint.Contains(x._broj)).ToList();

            //Middleware.Print.PrintSveditelstva(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
            switch (Menu.SelectedIndex)
            {
                case 0:
                    Middleware.Print.PrintSveditelstva(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
                    break;
                case 1:
                    Middleware.Print.PrintGlavnaKniga(uceniks, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
                    break;
            }
        }

        private void Btn_celprint_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (Menu.SelectedIndex)
            {
                case 0:
                    Middleware.Print.PrintSveditelstva(Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
                    break;
                case 1:
                    Middleware.Print.PrintGlavnaKniga(Home_Page.ucenici, Home_Page.KlasenKlasa, combobox_printer.SelectedIndex);
                    break;
            }
        }
    }
}
