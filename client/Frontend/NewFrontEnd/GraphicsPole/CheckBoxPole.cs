using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Frontend.NewFrontEnd.GraphicsPole
{
    class CheckBoxPole
    {
        Design DesignPole = null;
        object Model_object = null;
        string Binding = null;
        public CheckBoxPole(string Question, string DefaultAnswer, object Model_object, string Binding)
        {
            this.Model_object = Model_object;
            this.Binding = Binding;

            DesignPole = new Design(Question, GetCheckBox(DefaultAnswer));
            DesignPole.SetAnswer(DefaultAnswer);
        }
        public UIElement GetDesign()
        {
            return DesignPole.GetDesign();
        }

        Viewbox GetCheckBox(string DefaultAnswer)
        {
            CheckBox Checkb = new CheckBox();

            if (Binding != "")
            {
                Binding myBind = new Binding();
                myBind.Path = new PropertyPath(Binding);
                myBind.Source = Model_object;
                myBind.Mode = BindingMode.TwoWay;
                Checkb.SetBinding(ToggleButton.IsCheckedProperty, myBind);
            }

            if (DefaultAnswer == "True") Checkb.IsChecked = true;
            else Checkb.IsChecked = false;

            Checkb.Checked += Checkb_CheckedChange;
            Checkb.Unchecked += Checkb_CheckedChange;

            Viewbox vb = new Viewbox();
            vb.HorizontalAlignment = HorizontalAlignment.Center;
            vb.VerticalAlignment = VerticalAlignment.Center;
            vb.Height = 60.0;
            vb.Child = Checkb;
            return vb;
        }

        private void Checkb_CheckedChange(object sender, RoutedEventArgs e)
        {
            var Btn = (CheckBox)sender;
            DesignPole.SetAnswer(Btn.IsChecked.ToString());
        }
    }
}
