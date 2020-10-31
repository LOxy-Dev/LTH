using System.Collections.Generic;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Tournament.Rounds
{
    public interface IRound
    {
        public List<Player> Players { get; set; }

        public IBracket Bracket { get; set; }

        public void Init()
        {
            
        }

        public WrapPanel GetGenerator()
        {
            return null;
        }
    }
}