using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdminPanel
{
    /// <summary>
    /// Interaktionslogik für AdminFrame.xaml
    /// </summary>
    public partial class AdminFrame : Page
    {
        NavigationService ns;
        public AdminFrame()
        {
            InitializeComponent();

            DataContext = new ViewModel();

            //ListBox.ItemTemplate = new Pole().DT();
            foreach(Pole item in new ViewModel().Items)
            {
                Ugrid.Children.Add(item.GetPole());
            }
        }
     
    }
}

public class ViewModel
{
    public List<Pole> Items
    {
        get
        {
            return new List<Pole>
                {
                    new Pole { Name = "Корисничко име" , Question = new string[] { "ime prezime" } ,Type = 0},
                    new Pole { Name = "Лозинка" , Question = new string[] { "*****" } ,Type = 1},
                    new Pole { Name = "Предмети" , Question = new string[] { "Математика" , "Македонски" } ,Type = 1},
                    new Pole { Name = "Дозвола за печатење" , Question = new string[] { "Машко","Женско"} ,Type = 2},
                    new Pole { Name = "Поранешно Школо" , Question = new string[] { "Машко","Женско" , "Машко", "Женско", "Машко", "Женско" } ,Type = 2},
                    new Pole { Name = "Дозвола" , Question = new string[] { "1" , "0" } ,Type = 2},
                };
        }
    }
}

public class Pole
{
    public string Name { get; set; }

    public string[] Question = new string[] { };

    string Answer;
    public int Type { get; set; }

    public string Parametar;

   
    Border Title()
    {
        Border bd = new Border();
        bd.Height = 50.0;
        bd.VerticalAlignment = VerticalAlignment.Top;
        bd.Background = (Brush)Application.Current.FindResource("MenuItemColor");
        bd.BorderThickness = new Thickness(2);
        bd.CornerRadius = new CornerRadius(10);
        bd.Tag = Type;
        bd.Child = GetLabel(Name);
        return bd;
    }

    Label GetLabel(string Content)
    {
        Label lbl = new Label();
        lbl.Content = Content;
        lbl.VerticalAlignment = VerticalAlignment.Center;
        lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
        lbl.FontFamily = new FontFamily("Arial");
        lbl.FontWeight = FontWeights.Bold;
        lbl.FontSize = 30;
        lbl.Foreground = (Brush)Application.Current.FindResource("TextColor");
        return lbl;
    }

    Object GetAnswer()
    {
        if (Name == "Лозинка") return GetPasswordBox();
        if (Name == "Предмети") return GetPredmeti();
        else if (Question.Length == 1) return AnswerBox(Question[0], new Thickness(5, 0, 5, 0));
        else if (Enumerable.SequenceEqual(Question, new string[] { "1", "0" })) return GetCheckBox();
        else if (Question.Length < 5) return RadioButtons();
        else if (Question.Length >= 5) return GetComboBox();
        else return null;
    }

    StackPanel GetPredmeti()
    {
        StackPanel st = new StackPanel();
        int i = 0;
        foreach(string answer in Question)
        {
            st.Children.Add(AnswerBox_Option(answer));
            if(i++ < Question.Length -1)st.Children.Add(UnderBorder());
        }
        return st;
    }
    Grid AnswerBox_Option(String Text)
    {
        Grid gd = new Grid();
        gd.Children.Add(AnswerBox(Text,new Thickness(5, 0, 55, 0)));
        gd.Children.Add(ImageBox("Resources/Icons/x.png"));
        return gd;
    }
    TextBox AnswerBox(string Text,Thickness margin)
    {
        TextBox tx = new TextBox();
        tx.FontSize = 35.0;
        tx.Background = Brushes.Transparent;
        tx.Foreground = (Brush)Application.Current.FindResource("TextColor");
        tx.HorizontalAlignment = HorizontalAlignment.Stretch;
        tx.VerticalAlignment = VerticalAlignment.Center;
        tx.BorderThickness = new Thickness(0);
        tx.Margin = margin;
        tx.Text = Text;
        tx.LostFocus += AnswerBox_LostFocus;
        return tx;
    }

    private void AnswerBox_LostFocus(object sender, RoutedEventArgs e)
    {
        TextBox tx = (TextBox)sender;
        Console.WriteLine(tx.Text);
        Answer = tx.Text;
        Update();
    }

    private void Update()
    {
        //UpdateData(Name, Answer, Parametar);
    }

    Image ImageBox(string ImagePath)
    {
        Image img = new Image();
        img.Height = 40.0;
        img.HorizontalAlignment = HorizontalAlignment.Right;
        BitmapImage ImageSource = new BitmapImage(new Uri(ImagePath, UriKind.Relative));
        img.Source = ImageSource;
        return img;
    }
    Viewbox RadioButtons()
    {
        Viewbox vp = new Viewbox();
        UniformGrid dp = new UniformGrid();
        dp.Columns = 2;
        foreach (string answer in Question)
        {
            RadioButton btn = new RadioButton();
            btn.Foreground = (Brush)Application.Current.FindResource("TextColor");
            btn.Margin = new Thickness(20, 5, 20, 5);
            btn.Content = answer;
            dp.Children.Add(btn);
        }
        vp.Child = dp;
        return vp;     
    } 
    ComboBox GetComboBox()
    {
        ComboBox cb = new ComboBox();
        cb.ItemsSource = Question;
        cb.FontSize = 35.0; 
        return cb;
    }
    Viewbox GetCheckBox()
    {
        CheckBox Checkb = new CheckBox();

        Viewbox vb = new Viewbox();
        vb.HorizontalAlignment = HorizontalAlignment.Center;
        vb.VerticalAlignment = VerticalAlignment.Center;
        vb.Height = 60.0;
        vb.Child = Checkb;
        return vb;
    }
    Grid GetPasswordBox()
    {
        Grid gd = new Grid();
        gd.Children.Add(AnswerBox(Question[0],new Thickness(5,5,50,5)));
        gd.Children.Add(ImageBox("Resources/Icons/eye.png"));
        return gd;
    }
    Border UnderBorder()
    {
        Border bd = new Border();
        bd.Height = 10;
        bd.VerticalAlignment = VerticalAlignment.Center;
        bd.Background = (Brush)Application.Current.FindResource("UnderBorderColor");
        bd.BorderThickness = new Thickness(2);
        bd.CornerRadius = new CornerRadius(5);
        return bd;
    }

    public StackPanel GetPole()
    {
        StackPanel Main = new StackPanel();
        Main.Margin = new Thickness(10);
        Main.Children.Add(Title());

        StackPanel content = new StackPanel();
        content.Children.Add((UIElement)GetAnswer());
        content.Children.Add(UnderBorder());
        content.Margin = new Thickness(5);

        Main.Children.Add(content);
        return Main;
    }


    /*
     * 
     *  FrameworkElementFactory FE_Title()
    {
        var bd = new FrameworkElementFactory(typeof(Border));
        bd.SetValue(Border.HeightProperty, 50.0);
        bd.SetValue(Border.VerticalAlignmentProperty, VerticalAlignment.Top);
        //bd.SetValue(Border.MarginProperty, new Thickness(20, 20, 20, 0));
        bd.SetValue(Border.BackgroundProperty, Application.Current.FindResource("MenuItemColor"));
        bd.SetValue(Border.BorderThicknessProperty, new Thickness(2));
        bd.SetValue(Border.CornerRadiusProperty, new CornerRadius(10.0));
        bd.SetValue(Border.TagProperty, new Binding("Type"));
        //var lbl = LabelCtr();
        //lbl.SetValue(Label.ContentProperty, new Binding("Name"));

        bd.AppendChild(lbl);

        return bd;
    }
 FrameworkElementFactory LabelCtr()
 {
     var lbl = new FrameworkElementFactory(typeof(Label));
     lbl.SetValue(Label.HorizontalAlignmentProperty, HorizontalAlignment.Center);
     lbl.SetValue(Label.VerticalAlignmentProperty, VerticalAlignment.Center);
     lbl.SetValue(Label.ForegroundProperty, Application.Current.FindResource("TextColor"));
     lbl.SetValue(Label.FontSizeProperty, 30.0);
     lbl.SetValue(Label.FontFamilyProperty, new FontFamily("Arial"));
     lbl.SetValue(Label.FontWeightProperty, FontWeights.Bold);

     return lbl;
 }

 FrameworkElementFactory TextBox()
 {
     //<TextBox Text ="одговор" FontSize="35" Background="{x:Null}" Foreground="White" HorizontalAlignment="Center" VerticalAlignment="Center" BorderThickness="0"/>
     var tb = new FrameworkElementFactory(typeof(TextBox));
     tb.SetValue(TextBlock.FontSizeProperty , 35.0);
     tb.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
     tb.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
     tb.SetValue(Control.ForegroundProperty, Brushes.White);
     tb.SetValue(Control.BackgroundProperty, Brushes.Transparent);
     tb.SetValue(Control.BorderThicknessProperty, new Thickness(0));
     tb.SetValue(Control.MarginProperty, new Thickness(5, 0 , 5 , 0));
     tb.SetValue(TextBlock.TextProperty, new Binding("Answer[0]"));

     return tb;
 }

 FrameworkElementFactory BottomBorder()
 {
     //  <Border Height="10" VerticalAlignment="Center" Background="White" BorderThickness="2" CornerRadius="5"/>
     var bd = new FrameworkElementFactory(typeof(Border));
     bd.SetValue(Border.HeightProperty, 10.0);
     bd.SetValue(Border.VerticalAlignmentProperty, VerticalAlignment.Center);
     bd.SetValue(Border.BackgroundProperty, Brushes.White);
     bd.SetValue(Border.BorderThicknessProperty, new Thickness(2));
     bd.SetValue(Border.CornerRadiusProperty, new CornerRadius(5));

     return bd;
 }



 public DataTemplate DT()
{
     FrameworkElementFactory st = new FrameworkElementFactory(typeof(StackPanel));
     st.SetValue(StackPanel.MarginProperty, new Thickness(20));
     st.AppendChild(Title().);
     var t = );
     st.AppendChild(TextBox());
     st.AppendChild(BottomBorder());

     var template = new DataTemplate();
     template.VisualTree = st;

     return template;
}*/ //wpf binding

    void SetType()
    {
        /*
        if (Answer.Length = 0)//""se ceka odgovor od user"
        {
            Type = 0;
        }
        if(Answer.Length = 1)//"ima eden odgovor"
        {
            Type = 1;
        }
        if(Answer[0] == "*")//"password"
        {
            Type = 2;
        }
        if(Answer.Length == 2)//RadioBox
        {
            Type = 3;
        }
        if(Answer.Length > 4)//Combobox
        {
            Type = 4;
        }
        if (Answer == new string{ "0" , "1" })//CheckBox
        {
            Type = 5;
        }*/
    }
}