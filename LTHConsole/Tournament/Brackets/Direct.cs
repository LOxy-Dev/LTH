using System;
using System.Collections.Generic;

namespace LTHConsole.Tournament.Brackets
{
    public class Direct : Bracket
    {
        private int NbMatches;

        private Player _bufferPlayer;
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

            NbMatches = nbPlayers / 2;
            SetState();
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
            NbMatches--;
            if (NbMatches == 0)
            {
                NbMatches = State switch
                {
                    BracketState.Plus => 32,
                    BracketState.ThirtyTwo => 16,
                    BracketState.Sixteen => 8,
                    BracketState.Eight => 4,
                    BracketState.Quarter => 2,
                    BracketState.Semi => 1,
                    _ => NbMatches
                };
                SetState();
            }
            base.CheckMatch();
        }

        private void Win(Player winner, Player looser)
        {
            if (_bufferPlayer == null)
            {
                _bufferPlayer = winner;
            }
            else
            {
                Matches.Add(new Match(_bufferPlayer, winner));
                _bufferPlayer = null;
            }
        }

        public override void Print()
        {
            
        }

        private void SetState()
        {
            if (NbMatches <= 1) State = BracketState.Final;
            else if (NbMatches <= 2) State = BracketState.Semi;
            else if (NbMatches <= 4) State = BracketState.Quarter;
            else if (NbMatches <= 8) State = BracketState.Eight;
            else if (NbMatches <= 16) State = BracketState.Sixteen;
            else if (NbMatches <= 32) State = BracketState.ThirtyTwo;
            else State = BracketState.Plus;
        }
    }

    public enum BracketState
    {
        Final = 1, Semi = 2, Quarter = 4, Eight = 8, Sixteen = 16, ThirtyTwo = 32, Plus = 0
    }
}