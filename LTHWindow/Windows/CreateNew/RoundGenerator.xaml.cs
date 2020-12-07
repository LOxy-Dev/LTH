using System.Windows.Controls;

namespace LTHWindow.Windows.CreateNew
{
    public partial class RoundGenerator : UserControl
    {
        public RoundGenerator()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var panel = App.Tournament.Round.GetGenerator();
            
            var t = new WrapPanel();
            t.Children.Add(new Label {Content = "Test"});
            Grid.Children.Add(panel);
        }
    }
}