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
            
            Grid.Children.Add(panel);
        }

        public bool IsFill()
        {
            foreach (var child in Grid.Children)
            {
                if (child.GetType() != typeof(WrapPanel))
                    continue;

                foreach (var item in ((WrapPanel) child).Children)
                {
                    if ((item as WrapPanel)?.Name != "ObjectiveSelector")
                        continue;
                    
                    foreach (var element in ((WrapPanel) item).Children)
                    {
                        if (element.GetType() != typeof(ComboBox))
                            continue;

                        return ((ComboBox) element).SelectedItem != null;
                    }
                }
            }

            return false;
        }
    }
}