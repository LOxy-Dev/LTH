using System.Collections.Generic;
using LTHWindow.Tournament.Rounds;

namespace LTHWindow.Tournament
{
    public class Tournament
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public List<Player> Players { get; set; }
        public Round Round { get; set; }
        public Tournament()
        {
            Name = "Tournament";
            Players = new List<Player>();
            Round = new Round();
        }
    }
}