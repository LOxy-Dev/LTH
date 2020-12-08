using System;
using LTHConsole.Tournament.Brackets;

namespace LTHConsole.Tournament
{
    public class Match
    {
        private Objectives _objective;
        private int _scoreObj;
        public int[] Score { get; private set; }
        public Player Player1 { get; }
        public Player Player2 { get; }

        public Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;

            _objective = Program.Tournament.Round.Bracket.Type;
            _scoreObj = Program.Tournament.Round.Bracket.ScoreObjective;
        }

        public bool SetScore(int scoreP1, int scoreP2)
        {
            var highest = scoreP1 > scoreP2 ? scoreP1 : scoreP2;
            if (_objective == Objectives.BestOf)
            {
                if (scoreP1 + scoreP2 < _scoreObj)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Non played round(s) : {0}", _scoreObj - (scoreP1 + scoreP2));
                    Console.ResetColor();
                    return false;
                }

                if (scoreP1 + scoreP2 > _scoreObj)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : too many round(s) played");
                    Console.ResetColor();
                    return false;
                }
            }

            if (_objective == Objectives.FirstTo)
            {
                if (scoreP1 == _scoreObj || scoreP2 == _scoreObj)
                {
                    if (scoreP1 == scoreP2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("ERROR : 2 scores can't be equals...");
                        Console.ResetColor();
                        return false;
                    }
                    
                    if (highest < _scoreObj)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("ERROR : The highest score can't be higher than the score objective");
                        Console.ResetColor();
                        return false;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : One of the score must be equal to the score objective...");
                    Console.ResetColor();
                    return false;
                }
            }

            Score = new[] {scoreP1, scoreP2};
            return true;
        }
    }
}