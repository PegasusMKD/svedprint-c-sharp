using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using AdminPanel;
using AdminPanel.Middleware.Models;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Page
    {
        NavigationService ns;
        Admin admin;
        Dictionary<string, List<Middleware.Models.Klasen>> users;

        public MainFrame(NavigationService ns, Admin admin, Dictionary<string, List<Middleware.Models.Klasen>> users)
        {
            InitializeComponent();

            this.ns = ns;
            this.admin = admin;
            this.users = users;
            //Admin admin;Dictionary<string, ListKlasen> > users;

            MenuViewModel menu = new MenuViewModel(admin, users);
            AdminMainFrame.Navigate(menu.items[0].page);

            DataContext = menu;

            ListBox.ItemTemplate = new MenuGrid(admin, users).GetDataTemplate(AdminMainFrame);
        }



        private void Build_Users(object sender, RoutedEventArgs e)
        {
            //UsersWindow main = new UsersWindow(this.admin, this.users);
            //App.Current.MainWindow = main;
            //main.Show();
            //this.Close();


        }

        private void Build_Students(object sender, RoutedEventArgs e)
        {
            //List<Middleware.Models.Ucenik> k = Middleware.Controllers.Ucenik.RetrieveStudents(admin);
            //StudentsWindow main = new StudentsWindow(this.admin, k);
            //App.Current.MainWindow = main;
            //main.Show();
            //this.Close();
        }



        public class MenuViewModel
        {
            public Admin admin;
            public Dictionary<string, List<Klasen>> users;
            public List<NavItem> items;
            public MenuViewModel(Admin admin, Dictionary<string, List<Klasen>> users)
            {
                this.admin = admin;
                this.users = users;
                this.items = new List<NavItem> {
                    new NavItem { Name = "Админ" , page = new AdminFrame(admin , users)},
                    new NavItem { Name = "Училиште" , page = new UcilisteFrame(admin)},
                    new NavItem { Name = "Професори" , page = new ProfesoriFrame(admin,users)},
                    new NavItem { Name = "Ученици" , page = new TransferStudentsFrame(admin,users)}
                };
            }
            public List<NavItem> Items { get { return this.items; } }

        }

        public class NavItem
        {
            public string Name { get; set; }
            public Page page { get; set; }

        }

        public class MenuGrid
        {

            public Admin admin;
            public Dictionary<string, List<Klasen>> users;

            public MenuGrid(Admin admin, Dictionary<string, List<Klasen>> users)
            {
                this.admin = admin;
                this.users = users;
            }

            FrameworkElementFactory MenuBorder(Frame frame)
            {
                var BorderFEF = new FrameworkElementFactory(typeof(Border));
                BorderFEF.SetValue(Border.TagProperty, new Binding("page"));
                BorderFEF.SetValue(Border.BackgroundProperty, (Brush)(Application.Current.FindResource("MenuItemColor")));
                BorderFEF.SetValue(Border.PaddingProperty, new Thickness(40, 20, 20, 20));
                BorderFEF.AddHandler(Border.MouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) => Border_MouseLeftButtonDown(sender, e, frame)));
                BorderFEF.AddHandler(Border.MouseEnterEvent, new MouseEventHandler(Border_MouseEnter));
                BorderFEF.AddHandler(Border.MouseLeaveEvent, new MouseEventHandler(Border_MouseLeave));

                BorderFEF.AppendChild(LabelMenu());

                return BorderFEF;
            }

            FrameworkElementFactory LabelMenu()
            {
                var LabelFwF = new FrameworkElementFactory(typeof(Label));

                LabelFwF.SetValue(Label.FontSizeProperty, 30.0);
                //LabelFwF.SetValue(MarginProperty, 0);
                LabelFwF.SetValue(Label.ForegroundProperty, Brushes.White);
                LabelFwF.SetValue(Label.ContentProperty, new Binding("Name"));
                return LabelFwF;
            }

            public DataTemplate GetDataTemplate(Frame frame)
            {
                var BorderFrameWorkFactory = MenuBorder(frame);

                var template = new DataTemplate();
                template.VisualTree = BorderFrameWorkFactory;

                return template;
            }


            object PrevClickedMenuItem = null;

            public Border MenuItemBorder(string Text)
            {
                Border bd = new Border();
                bd.Background = (Brush)(new BrushConverter()).ConvertFrom("#FFFF7B54");
                bd.MouseEnter += Border_MouseEnter;
                bd.MouseLeave += Border_MouseLeave;
                //bd.MouseLeftButtonDown += Border_MouseLeftButtonDown;
                bd.Tag = Text.ToString();

                Label lbl = new Label();
                lbl.FontSize = 30;
                lbl.Margin = new Thickness(40, 20, 20, 20);
                lbl.Foreground = Brushes.White;
                lbl.Content = Text;

                bd.Child = lbl;

                return bd;
            }//old Design
            private void Border_MouseEnter(object sender, MouseEventArgs e)
            {
                Border bd = (Border)sender;
                bd.Background = (Brush)(Application.Current.FindResource("MenuItemHoverColor"));
            }
            private void Border_MouseLeave(object sender, MouseEventArgs e)
            {
                Border bd = (Border)sender;

                if (PrevClickedMenuItem != sender) bd.Background = (Brush)(Application.Current.FindResource("MenuItemColor"));//(new BrushConverter()).ConvertFrom("#FFFF7B54");
            }
            private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, Frame frame)
            {
                Border bd = (Border)sender;
                frame.Navigate((Page)bd.Tag);

                if (PrevClickedMenuItem != null)//hover
                {
                    Border PrevBorder = (Border)PrevClickedMenuItem;
                    PrevBorder.Background = (Brush)(Application.Current.FindResource("MenuItemColor"));
                }

                PrevClickedMenuItem = sender;

                new Task(() => { Update(admin, users); }).Start();

            }

            void Update(Admin admin, Dictionary<string, List<Klasen>> users)
            {
                //Middleware.Controllers.Klasen.UpdateUsers(admin, users);
                //Middleware.Controllers.Uchilishte.UpdateDates(admin, admin.Uchilishte);
                //Middleware.Controllers.Admin.UpdateData(admin);
                //Middleware.Controllers.Admin.UpdatePrint(admin);
            }
        }
    }
}

