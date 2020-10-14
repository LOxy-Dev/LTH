using System.Collections.Generic;

namespace LTHConsole.Tournament.Brackets
{
    public class Direct : Bracket
    {
        public Direct(List<Player> players) : base(players)
        {
            Players = players;
        }
    }
}