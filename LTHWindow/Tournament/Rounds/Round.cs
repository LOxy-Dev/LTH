using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace LTHWindow.Tournament.Rounds
{
    public class Round
    {
        protected List<Player> Players;

        protected WrapPanel Generator = new WrapPanel
        {
            Name = "Generator",
            Orientation = Orientation.Vertical
        };

        public virtual void Init()
        {
            
        }

        public virtual WrapPanel GetGenerator()
        {
            return null;
        }
    }
}