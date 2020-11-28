using System.Collections.Generic;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Tournament.Rounds
{
    public class MultiRounds : IRound
    {
        // Implemented fields
        public List<Player> Players { get; set; }

        IBracket IRound.Bracket { get; set; }

        public void Init()
        {
            
        }

        public WrapPanel GetGenerator()
        {
            throw new System.NotImplementedException();
        }
    }
}