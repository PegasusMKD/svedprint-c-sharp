using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für MainFrame.xaml
    /// </summary>
    public partial class MainFrame : Page
    {
        NavigationService ns;

        public MainFrame(NavigationService ns)
        {
            InitializeComponent();

            this.ns = ns;
            //Admin admin;Dictionary<string, ListKlasen> > users;

            AdminMainFrame.Navigate(new AdminFrame());

            DataContext = new MenuViewModel();

            ListBox.ItemTemplate = new MenuGrid().GetDataTemplate(AdminMainFrame);
        }

    }

    public class MenuViewModel
    {
        public List<NavItem> Items
        {
            get
            {
                return new List<NavItem>
                {
                    new NavItem { Name = "Админ" , page = new AdminFrame()},
                    new NavItem { Name = "Училиште" , page = new AdminFrame()},
                    new NavItem { Name = "Професори" , page = new ProfesoriFrame()}
                };
            }
        }
    }

    public class NavItem
    {
        public string Name { get; set; }
        public Page page { get; set; }

    }

    public class MenuGrid
    {
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
        }
    }
}
