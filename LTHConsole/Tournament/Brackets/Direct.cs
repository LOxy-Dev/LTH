using System;
using System.Collections.Generic;

namespace LTHConsole.Tournament.Brackets
{
    public class Direct : Bracket
    {
        public Direct(List<Player> players) : base(players)
        {
            Init();
        }

        private void Init()
        {
            var nbPlayers = Players.Count % 2 != 0 ? Players.Count - 1 : Players.Count;

            for (int i = 0; i < nbPlayers; i += 2)
            {
                Matches.Add(new Match(Players[i], Players[i + 1]));
            }

            IsFinished = true;
        }

        public override void CheckMatch()
        {
            var match = GetActualMatch();
            Player winner;
            Player looser;
            if (Score[0] == ScoreObjective)
            {
                winner = match.Player1;
                looser = match.Player2;
            }
            else
            {
                winner = match.Player2;
                looser = match.Player1;
            }
            Win(winner, looser);
        }

        private void Win(Player winner, Player looser)
        {
            
        }

        public override void Print()
        {
            
        }
    }
}