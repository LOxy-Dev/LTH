using System;
using System.Collections.Generic;
using System.Linq;

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
        public Groups(List<Player> players, Objectives objectiveType, int scoreObjective)
        {
            Players = players;
            ActualMatchId = 0;

            Type = objectiveType;
            ScoreObjective = scoreObjective;
            
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
            var match = GetActualMatch();
            
            switch (Type)
            {
                case Objectives.BestOf:
                    // Check if score is valid
                    if (match.Scores[0] + match.Scores[1] != ScoreObjective)
                        return;

                    if (match.Scores[0] == match.Scores[1])
                        Draw(match);
                    else if (match.Scores[0] > ScoreObjective)
                        Win(match.Player1, match.Player2);
                    else
                        Win(match.Player2, match.Player1);
                    
                    break;
                
                case Objectives.FirstTo:
                    if (match.Scores[0] == ScoreObjective)
                        Win(match.Player1, match.Player2);
                    else
                        Win(match.Player2, match.Player1);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            ActualMatchId++;
            Players = Players.OrderByDescending(i => i.Score).ThenByDescending(j => j.Score).ToList();
            if (ActualMatchId == Matches.Count) IsFinished = true;
        }

        private void Win(Player winner, Player looser)
        {
            var match = GetActualMatch();
            
            winner.AddResult(Result.Win);
            looser.AddResult(Result.Loss);

            if (match.Scores[0] - match.Scores[1] > 0)
            {
                winner.Score += match.Scores[0] - match.Scores[1];
                looser.Score -= match.Scores[0] - match.Scores[1];
            }
            else
            {
                winner.Score -= match.Scores[0] - match.Scores[1];
                looser.Score += match.Scores[0] - match.Scores[1];
            }
        }
        
        private void Draw(Match match)
        {
            match.Player1.AddResult(Result.Draw);
            match.Player2.AddResult(Result.Draw);
        }
    }
}