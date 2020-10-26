using System.Collections.Generic;
using LTHWindow.Tournament.Rounds;

namespace LTHWindow.Tournament
{
    public class Tournament
    {
        public string Name { get; }
        public List<Player> Players { get; }
        private Round _round;
        public Tournament(string name, int nbPlayer, Round round)
        {
            Name = name;
            Players = new List<Player>(nbPlayer);
            _round = round;
        }
    }
}