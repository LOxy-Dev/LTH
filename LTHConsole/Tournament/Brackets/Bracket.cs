using System.Collections.Generic;
using System.Linq;

namespace LTHConsole.Tournament.Brackets
{
    public class Bracket
    {
        public bool IsFinished { get; private set; }
        public BracketState State { get; protected set; }

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

        public Match GetActualMatch()
        {
            return Matches.ElementAt(ActualMatchId);
        }

        // This method calculate the who won the match and switch to the next one
        public virtual void CheckMatch()
        {
            ActualMatchId++;
            if (ActualMatchId == Matches.Count) IsFinished = true;
        }

        // Print the state of the bracket
        public virtual void Print()
        {
            
        }
    }

    public enum Objectives
    {
        BestOf = 0, FirstTo = 1
    }
}