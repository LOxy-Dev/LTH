using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LTHWindow.Tournament.Brackets
{
    public class Groups : IBracket
    {
        // Implemented fields
        public bool IsFinished { get; set; }

        public int ActualMatchId { get; set; }
        public int ScoreObjective { get; set; }
        public int[] Score { get; }
        
        public Objectives Type { get; set; }

        public List<Player> Players { get; set; }
        public List<Match> Matches { get; }

        // Constructor
        public Groups(List<Player> players)
        {
            Players = players;
            ActualMatchId = 0;
            
            Matches = new List<Match>();
            
            // Create matches
            for (var i = 0; i < Players.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    var match = new Match(Players.ElementAt(j), Players.ElementAt(i));
                    Matches.Add(match);
                }
            }
        }

        public Match GetActualMatch() => Matches[ActualMatchId];

        public void CheckMatch()
        {
            if (Score[0] == ScoreObjective && Score[1] == ScoreObjective)
            {
                Draw();
            }
            else if (Score[0] == ScoreObjective)
            {
                Win(GetActualMatch().Player1, GetActualMatch().Player2);
            }
            else if (Score[1] == ScoreObjective)
            {
                Win(GetActualMatch().Player2, GetActualMatch().Player1);
            }
            else
            {
                Draw();
            }
            ActualMatchId++;
            Players = Players.OrderByDescending(i => i.WLDRatio).ThenByDescending(j => j.Score).ToList();
            if (ActualMatchId == Matches.Count) IsFinished = true;
        }

        public WrapPanel GetVisualizer()
        {
            return new WrapPanel();
        }

        private void Win(Player winner, Player looser)
        {
            winner.AddResult(Result.Win);
            looser.AddResult(Result.Loss);

            if (Score[0] - Score[1] > 0)
            {
                winner.Score += Score[0] - Score[1];
                looser.Score -= Score[0] - Score[1];
            }
            else
            {
                winner.Score -= Score[0] - Score[1];
                looser.Score += Score[0] - Score[1];
            }
        }
        
        private void Draw()
        {
            
        }
    }
}