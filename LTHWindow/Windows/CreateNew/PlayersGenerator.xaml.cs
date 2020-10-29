//using System.Linq; // UNUSER
using System.Windows.Controls;
using LTHWindow.Tournament;

namespace LTHWindow.Windows.CreateNew
{
    public partial class PlayersGenerator : UserControl
    {

        public PlayersGenerator()
        {
            InitializeComponent();
        }

        public void Init()
        {
            BoxesPanel.Children.Clear();
            
            for (var i = 0; i < App.Tournament.Players.Capacity; i++)
            {
                var box = new TextBox
                {
                    Text = "Player" + (i + 1),
                };

                BoxesPanel.Children.Add(box);
                BoxesPanel.Children.Add(new Separator()
                {
                    Opacity = 0,
                    Height = 5
                });
            }
        }

        public void GeneratePlayers()
        {
            // Return if a field is empty
            if (!IsFill()) return;
            
            // Reset the list
            App.Tournament.Players.Clear();
            
            foreach (var me in BoxesPanel.Children)
            {

                if (me.GetType() != typeof(TextBox)) continue;
                
                var tBox = (TextBox) me;

                App.Tournament.Players.Add( new Player(tBox.Text));
            }
        }
        
        public bool IsFill()
        {
            foreach (var me in BoxesPanel.Children)
            {
                if (me.GetType() != typeof(TextBox)) continue;

                var tBox = (TextBox) me;
                var text = tBox.Text;
                // Check if empty
                if (text.Length < 1) return false;
                /* IF CONTAINS BANNED CHARACTERS
                // Check if it contains impossible values
                var impossibleValues = new[] {' ', '/', ';', '°'};
                foreach (var c in text)
                {
                    if (impossibleValues.Contains(c)) return false;
                }*/
            }
            return true;
        }
    }
}