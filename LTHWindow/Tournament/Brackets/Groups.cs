using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LTHWindow.Tournament.Brackets
{
    public class Groups : IBracket
    {
        // Implemented fields
        bool IBracket.IsFinished { get; set; }

        int IBracket.ActualMatchId { get; set; }

        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        public Objectives Type { get; set; }

        public List<Player> Players { get; set; }

        List<Match> IBracket.Matches { get; }
        
        // Constructor
        public Groups()
        {
            
        }
        
    }
}