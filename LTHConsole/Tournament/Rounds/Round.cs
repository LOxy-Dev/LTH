using System.Collections.Generic;
using LTHConsole.Tournament.Brackets;

namespace LTHConsole.Tournament.Rounds
{
    // TODO add a the possibility to set the objective
    public class Round
    {
        protected List<Player> Participants;

        public Bracket Bracket { get; protected set; }

        public virtual void Init()
        {
            
        }

        public virtual void Print()
        {
            
        }

        public Match GetNextMatch()
        {
            return Bracket.GetActualMatch();
        }
    }
}