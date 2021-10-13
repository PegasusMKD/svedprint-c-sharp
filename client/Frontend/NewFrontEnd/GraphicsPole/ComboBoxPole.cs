using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Frontend.NewFrontEnd.GraphicsPole
{
    class ComboBoxPole
    {
        Design design = null;
        object Model_object = null;
        string Binding = null;
        public ComboBoxPole(string Question, string[] Answer, object Model_object, string Binding)
        {
            this.Model_object = Model_object;
            this.Binding = Binding;

            design = new Design(Question, GetComboBox(Answer));
        }

        public UIElement GetDesign()
        {
            return design.GetDesign();
        }

        public ComboBox GetComboBox(string[] Questions)
        {
            ComboBox cb = new ComboBox();
            cb.ItemsSource = Questions;

            if (Binding != null)
            {
                Binding myBind = new Binding();
                myBind.Path = new PropertyPath(Binding);
                myBind.Source = Model_object;
                myBind.Mode = BindingMode.TwoWay;
                myBind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                cb.SetBinding(Selector.SelectedValueProperty, myBind);
            }
            if (cb.SelectedValue == null) cb.SelectedIndex = 0;

            cb.FontSize = 35.0;
            cb.VerticalAlignment = VerticalAlignment.Top;
            cb.SelectionChanged += CB_SelectionChanged;
            return cb;
        }

        private void CB_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var cb = (ComboBox)sender;
            design.SetAnswer(cb.SelectedItem.ToString());
        }
    }
}
