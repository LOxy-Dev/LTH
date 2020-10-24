using System.Collections.Generic;
using System.Linq;

namespace LTHConsole.Tournament.Brackets
{
    public class Bracket
    {
        public bool IsFinished { get; private set; }
        public BracketState State { get; protected set; }

        private int _actualMatchId;
        public int ScoreObjective { get; set; }
        public Objectives Type { get; set; }

        protected List<Player> Players;
        protected List<Match> Matches { get; set; }

        protected Bracket(List<Player> players)
        {
            Players = players;
            Matches = new List<Match>();
            _actualMatchId = 0;
            IsFinished = false;
        }

        public virtual void Init()
        {
            
        }

        public Match GetActualMatch()
        {
            return Matches.ElementAt(_actualMatchId);
        }

        // This method calculate the who won the match and switch to the next one
        public virtual void CheckMatch()
        {
            _actualMatchId++;
            if (_actualMatchId == Matches.Count) IsFinished = true;
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