using System.Windows;

namespace Frontend.NewFrontEnd.GraphicsPole
{
    class Template
    {
        Design DesignPole = null;
        object Model_object = null;
        string Binding = null;
        public Template(string Question, string Answer, object Model_object, string Binding)
        {
            this.Model_object = Model_object;
            this.Binding = Binding;

            //DesignPole = new Design(Question, answer);
        }
        public UIElement GetDesign()
        {
            return DesignPole.CreateStackPanelDesign();
        }
    }
}
