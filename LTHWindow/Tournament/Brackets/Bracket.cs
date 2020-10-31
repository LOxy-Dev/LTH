using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LTHWindow.Tournament.Brackets
{
    // This class should not be instantiate.
    // Please use the inherited classes
    public interface IBracket
    {
        public bool IsFinished { get; protected set; }
        protected int ActualMatchId { get; set; }
        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        public Objectives Type { get; set; }

        public List<Player> Players { get; set; }
        protected List<Match> Matches { get; }

        public Match GetActualMatch() => Matches.ElementAt(ActualMatchId);

        // This method calculate the who won the match and switch to the next one
        public void CheckMatch()
        {
            
        }
        
        // Generate the visualisation panel
        public  WrapPanel GetVisualizer()
        {
            return null;
        }
    }

    public enum Objectives
    {
        BestOf = 0, FirstTo = 1
    }
}