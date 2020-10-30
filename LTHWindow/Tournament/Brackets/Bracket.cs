using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LTHWindow.Tournament.Brackets
{
    // This class should not be instantiate.
    // Please use the inherited classes
    public class Bracket
    {
        public bool IsFinished { get; protected set; }

        protected int ActualMatchId = 0;
        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        public Objectives Type { get; set; }

        protected List<Player> Players;
        protected List<Match> Matches { get; }
        
        protected Bracket(List<Player> players)
        {
            Players = players;
            Matches = new List<Match>();
            Score = new int[2];
            IsFinished = false;
        }

        public virtual Match GetActualMatch()
        {
            return Matches.ElementAt(ActualMatchId);
        }

        // This method calculate the who won the match and switch to the next one
        public virtual void CheckMatch()
        {
            
        }
        
        // Generate the visualisation panel
        public virtual WrapPanel GetVisualiser()
        {
            return null;
        }
    }

    public enum Objectives
    {
        BestOf = 0, FirstTo = 1
    }
}