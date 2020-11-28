using System.Collections.Generic;
using System.Windows.Controls;
using LTHWindow.Tournament.Brackets;

namespace LTHWindow.Tournament.Rounds
{
    public class DirectEliminationRound : IRound
    {
        public List<Player> Players { get; set; }
        public IBracket Bracket { get; set; }

        public void Init()
        {
            
        }

        public WrapPanel GetGenerator()
        {
            throw new System.NotImplementedException();
        }
    }
}