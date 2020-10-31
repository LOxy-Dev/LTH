using System.Collections.Generic;

namespace LTHWindow.Tournament.Brackets
{
    public class Direct : IBracket
    {
        bool IBracket.IsFinished { get; set; }

        int IBracket.ActualMatchId { get; set; }

        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        public Objectives Type { get; set; }

        List<Player> IBracket.Players { get; set; }

        List<Match> IBracket.Matches { get; }
    }
}