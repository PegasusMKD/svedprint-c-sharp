using Middleware;
using MiddlewareRevisited;
using MiddlewareRevisited.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Frontend.SettingsDesign;

namespace Frontend
{
    /// <summary>
    /// Interaktionslogik für EditUcenici_Page.xaml
    /// </summary>
    public partial class EditUcenici_Page : Page
    {

        User currentUser;
        List<Student> students;
        List<SubjectOrientation> subjectOrientations;
        int BrojDn = 0;

        public EditUcenici_Page(ref List<SubjectOrientation> subjectOrientations, ref User currentUser, ref List<Student> students)
        {
            InitializeComponent();
            
            this.currentUser = currentUser;
            this.students = students;
            this.subjectOrientations = subjectOrientations;

            Refresh();
        }

        List<TextBox> ListPolinjaTxt;
        List<ComboBox> ListPolinjaCB;
        List<Pole> polinja = new List<Pole>();
        private void Refresh()
        {
            ListPolinjaTxt = new List<TextBox>();
            ListPolinjaCB = new List<ComboBox>();
            st1.Children.Clear();
            st2.Children.Clear();
            polinja.Clear();
            string[] PApredmeti = { "Ги немате пополнето Проектните Активности" };
            string[] Smerovi = { "Ги немате пополнето Смеровите" };
            if (subjectOrientations.Select(orientation => orientation.shortName).Contains("ПА")) PApredmeti = (from subjectOrientation in subjectOrientations where subjectOrientation.shortName.Equals("ПА") select subjectOrientation).FirstOrDefault().subjects.ToArray();
            if (subjectOrientations.Count > 3)
            {
                Smerovi = subjectOrientations.Select(orientation => orientation.shortName).ToArray();
            }
            
            List<string> polinjaUpdate = new List<string>();
            
            polinja.Add(new Pole("Име", RequestParameters.ime,  new string[] { "Име" } ));
            polinja.Add(new Pole("Презиме", RequestParameters.prezime, new string[] { "Презиме" }));
            polinja.Add(new Pole("Татково име", RequestParameters.srednoIme, new string[] { "Татково име" }));
            polinja.Add(new Pole("Смер", RequestParameters.smer, Smerovi));
            polinja.Add(new Pole("Број во дневник", RequestParameters.broj, new string[] { "0" }));
            polinja.Add(new Pole("Датум на раѓање", RequestParameters.roden, new string[] { "00.00.0000" }));
            polinja.Add(new Pole("Пол", RequestParameters.gender, new string[] { "Машки" , "Женски" }));
            polinja.Add(new Pole("Место на раѓање", RequestParameters.mesto_na_ragjanje, new string[] { "Скопје" }));
            polinja.Add(new Pole("Место на живеење", RequestParameters.mesto_na_zhiveenje, new string[] { "Скопје" }));
            polinja.Add(new Pole("Родител (Татко)", RequestParameters.tatko, new string[] { "Име Презиме" }));
            polinja.Add(new Pole("Родител (Мајка)", RequestParameters.majka, new string[] { "Име Презиме" }));
            polinja.Add(new Pole("По кој пат ја учи годината", RequestParameters.pat_polaga, new string[] { "прв пат" , "втор пат" , "трет пат" }));
            polinja.Add(new Pole("Дали е положена годината", RequestParameters.polozhil, new string[] { "завршил" , "не завршил" }));
            polinja.Add(new Pole("Претходна година", RequestParameters.prethodna_godina, new string[] { "I", "II", "III" , "IV"}));
            polinja.Add(new Pole("Претходно училиште", RequestParameters.prethodno_uchilishte, new string[] { "СУГС - Раде Јовчевски Корчагин" }));
            polinja.Add(new Pole("Претходен успех", RequestParameters.prethoden_uspeh, new string[] { "Одличен" , "Многу добар" , "Добар", "Доволен"}));

            if(students.Count > 0 && students.Count > BrojDn)
            {
                // conflicting code
                // do not use this page
                // Dictionary<string, string> PolinjaModels = students[BrojDn].GetPolinja(); 
                foreach (Pole pole in polinja)
                {
                    ///polinja.Find(y => y.RequestParametar == x.Key).Odgovor = x.Value;
                    // polinja.Find(x => x.Ime == pole.Ime).Odgovor = PolinjaModels[pole.RequestParametar];
                }
            }

            if (polinja.Find(x => x.RequestParametar == RequestParameters.broj).GetOdgovor() == "0") polinja.Find(x => x.RequestParametar == RequestParameters.broj).Odgovor = (BrojDn+1).ToString();

            int index = 0;
            foreach (Pole Pole in polinja)
            {
                StackPanel stackPanel = new StackPanel();

                StackPanel st = new StackPanel();

                if (Pole.isCB)
                {
                    ComboBox CB = CreateComboBox(Pole.Ime , Pole.DefaultVrednost);
                    CB.SelectedValue = Pole.GetOdgovor();
                    CB.SelectionChanged += CB_SelectionChanged;
                    ListPolinjaCB.Add(CB);
                    st.Children.Add(CB);
                }
                else
                {
                    TextBox textBox = ContentTextBox(Pole.GetOdgovor());
                    textBox.Margin = new Thickness(30, 0, 30, 0);
                    textBox.GotFocus += txtPolinjaGotFocus;
                    textBox.LostFocus += txtPolinjaLostFocus;
                    textBox.TextChanged += txtPolinjaTxtChanged;
                    textBox.Tag = Pole.Ime;
                    ListPolinjaTxt.Add(textBox);
                    st.Children.Add(textBox);
                }

                stackPanel.Children.Add(ContentBorder(Pole.Ime));
                stackPanel.Children.Add(st);
                stackPanel.Children.Add(UnderTextBorder());

                if (index % 2 == 0)
                {
                    st1.Children.Add(stackPanel);
                }
                else
                {
                    st2.Children.Add(stackPanel);
                }
                ++index;
            }
        }

        private void txtPolinjaTxtChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if(tx.Text != "")polinja.Find(x => x.Ime == tx.Tag.ToString()).Odgovor = tx.Text;
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)(sender);
            polinja.Find(x => x.Ime == cb.Tag.ToString()).Odgovor = cb.SelectedValue.ToString();
        }

        private void txtPolinjaLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if (tx.Text == "") AddText(sender, pol.DefaultVrednost[0]);
            else polinja.Find( x=> x.Ime == tx.Tag.ToString()).Odgovor = tx.Text;
        }

        private void txtPolinjaGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tx = (TextBox)(sender);
            Pole pol = polinja.Find(x => x.Ime == tx.Tag.ToString());
            if (((TextBox)sender).Text == pol.DefaultVrednost[0]) RemoveText(sender);
        }

        private void AddText(object sender, string v)
        {
            if (string.IsNullOrWhiteSpace(((System.Windows.Controls.TextBox)sender).Text))
            {
                ((TextBox)sender).Text = v;
            }
        }

        private void RemoveText(object sender)
        {
            ((TextBox)sender).Text = "";
        }

        private Border ContentBorder(string LabelContent)
        {
            Border bd = CreateBorder(40, 5, 20, 10, "#FFED6A3E");
            bd.Child = CreateLabel(LabelContent, 20, "Arial");
            return bd;
        }

        private void LeftTriangleClicked(object sender, MouseEventArgs e)
        {
            BrojDnLabel.Text = Valid(-1);
            Refresh();
        }

        private void RightTriangleClicked(object sender, MouseButtonEventArgs e)
        {
            BrojDnLabel.Text = Valid(+1);
            Refresh();
        }

        String Valid(int x)
        {
            if (BrojDn + x >= 0 && BrojDn + x <= students.Count) BrojDn += x;
            return (BrojDn + 1).ToString();
        }

        private async void SaveBtnClicked(object sender, MouseButtonEventArgs e)
        {
            //if (BrojDn > Ucenici.Count)
            //{
            //    MessageBox.Show("Ученикот со таков број во дневник не постои");
            //}
            //if(Ucenici.Count == BrojDn)
            //{
            //    CreateUcenik(polinja[0].Odgovor, polinja[2].Odgovor, polinja[1].Odgovor, polinja[3].GetOdgovor(), BrojDn + 1);
            //    int bk = BrojDn;
            //    BrojDn = Ucenici.Count - 1;
            //    BrojDn = bk;

            //    polinja.Add(new Pole("Поведение", RequestParameters.povedenie, new string[] { "Примeрно" }));
            //    polinja.Add(new Pole("Педагошки мерки", RequestParameters.pedagoshki_merki, new string[] { "0" }));
            //    polinja.Add(new Pole("Јазици", RequestParameters.jazik, new string[] { "0:1" }));
            //    polinja.Add(new Pole("Изборен Предмет 1", RequestParameters.izborni, new string[] { "0"}));
            //}
            try
            {
                await Save();
            } catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }
        }

        private async Task Save()
        {
            Student student = new Student();

            if(students.Count > BrojDn)
            {
                student = students[int.Parse(BrojDnLabel.Text) - 1];
            }
            Util.UpdateObjectByFields<Student>(polinja, student);
            await MiddlewareRevisited.Controllers.Student.UpdateStudent(student, currentUser);
        }

        public string ProektniToString(List<string> tx)
        {
            int i = 0;
            string s = "";
            foreach (string x in tx)
            {
                if (i % 2 == 0)
                {
                    s += x + ",";
                }
                else s += x + ";";
                i++;
            }
            s = s.Substring(0, s.Length - 1);
            return s;
        }

        private string Output(string answer)
        {
            switch (answer)
            {
                case "000":
                    return "Успешно зачувување на ученикот";
                case "123":
                    return "Паралелката нема ученици";
                case "502":
                    return "Не постои ученикот, ве молиме креирајте го";
                case "107":
                    return "Сервисот не е достапен, ве молиме исконтактирајте ги админите";
                default:
                    return answer;
            }
        }

        private void CreateUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
               
        }

        private void CreateUcenik(string ime, string srednoime, string prezime, string smer, int br)
        {
            //if (UserKlas._p._smerovi.ContainsKey(smer) == false)
            //{
            //    MessageBox.Show("Смерот не се совпаѓа");
            //    return;
            //}
            //List<Ucenik> match = Ucenici.Where(x => (x._prezime == prezime) && (x._ime == ime) && (x._srednoIme == srednoime)).ToList();
            //Ucenici.Add(new Ucenik()
            //{
            //    _ime = ime,
            //    _srednoIme = srednoime,
            //    _prezime = prezime,
            //    _smer = UserKlas._p._smerovi[smer]._smer,
            //    _broj = br,
            //    _oceni = new List<int>(Enumerable.Repeat(0, UserKlas._p._smerovi[smer]._predmeti.Count))
            //}) ; // better constructor, must be tested
            //// Ucenici.Add(new Ucenik(ime, srednoime, prezime, UserKlas._p._smerovi[smer], br));
            //Ucenici.Last()._duplicate_ctr = match.Count();
            //Ucenici.Last().CreateServerUcenik(UserKlas._token);
            //MessageBox.Show("успешно креирање на нов ученик");
        }
        
        private async void DeleteUcenikImgClicked(object sender, MouseButtonEventArgs e)
        {
            //if (BrojDn >= Ucenici.Count)
            //{
            //    MessageBox.Show("Ученикот со тој број не постои");
            //    return;
            //}
            //await Ucenici[BrojDn].DeleteUcenik(UserKlas._token);
            //Ucenici.RemoveAt(BrojDn);
            //SortUcenici();
            //Refresh();
        }

    }
}
