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
                    Text = "Player " + (i + 1)
                };

                BoxesPanel.Children.Add(box);
            }
        }

        public void GeneratePlayers()
        {
            if (IsFill())
            {
                foreach (TextBox me in BoxesPanel.Children)
                {
                    App.Tournament.Players.Clear();
                    
                    App.Tournament.Players.Add(new Player(me.Text));
                }
            }
        }
        
        public bool IsFill()
        {
            foreach (TextBox me in BoxesPanel.Children)
            {
                if (me.Text == "") return false;
            }
            return true;
        }
    }
}