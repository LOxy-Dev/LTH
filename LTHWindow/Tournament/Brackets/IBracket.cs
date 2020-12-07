using System.Collections.Generic;

namespace LTHWindow.Tournament.Brackets
{
    public interface IBracket
    {
        public bool IsFinished { get; set; }
        public int ActualMatchId { get; set; }
        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        public Objectives Type { get; set; }

        public List<Player> Players { get; set; }
        public List<Match> Matches { get; }

        public Match GetActualMatch();

        // This method calculate the who won the match and switch to the next one
        public void CheckMatch();

        // Generate the visualisation panel
    }

    public enum Objectives
    {
        BestOf = 0, FirstTo = 1
    }
}