using System.Collections.Generic;

namespace LTHWindow.Tournament.Brackets
{
    public class Direct : Bracket
    {
        public Direct(List<Player> players) : base(players)
        {
            Players = players;
        }
    }
}