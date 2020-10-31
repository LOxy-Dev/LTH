using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace LTHWindow.Tournament.Brackets
{
    public class Groups : Bracket
    {
        public Groups(List<Player> players) : base(players)
        {
            Players = players;
            
            Init();
        }

        private void Init()
        {
            for (var i = 0; i < Players.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    var match = new Match()
                    {
                        Player1 = Players.ElementAt(j),
                        Player2 = Players.ElementAt(i)
                    };
                    Matches.Add(match);
                }
            }
        }

        public override void CheckMatch()
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
            Players = Players.OrderByDescending(i => i.WldRatio).ThenByDescending(j => j.Score).ToList();
            if (ActualMatchId == Matches.Count) IsFinished = true;
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
            var actualMatch = GetActualMatch();
            var p1 = actualMatch.Player1;
            var p2 = actualMatch.Player2;

            p1.AddResult(Result.Draw);
            p2.AddResult(Result.Draw);
        }

        public override WrapPanel GetVisualiser()
        {
            return null;
        }
    }
}