using System.Collections.Generic;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Tournament.Rounds
{
    public class Round
    {
        protected List<Player> Players;

        public Bracket Bracket { get; protected set; }

        protected readonly WrapPanel Generator = new WrapPanel
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