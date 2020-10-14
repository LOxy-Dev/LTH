using System;
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
            
            // Get the next match of the bracket
            var match = Bracket.GetActualMatch();
            var player1 = match.Player1;
            var player2 = match.Player2;
            // Print the match
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Actual match : {0} VS {1}", player1.Name, player2.Name);
            // Print the objective
            Console.Write(Bracket.Type == 0 ? "Best of " : "First to ");
            Console.WriteLine(Bracket.ScoreObjective);
            Console.ResetColor();
            
            // Ask for the score
            int i;
            // Player 1
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Enter the score of {0}...", player1.Name);
                Console.ResetColor();
                try
                {
                    i = Int32.Parse(Console.ReadLine()!);
                    if (i >= 0 && i <= Bracket.ScoreObjective)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer can't be lower than 0 or upper than {0}.", Bracket.ScoreObjective);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e);
                    Console.ResetColor();
                    throw;
                }
            }

            Bracket.Score[0] = i;
            
            // Player 2
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Enter the score of {0}...", player2.Name);
                Console.ResetColor();
                try
                {
                    i = Int32.Parse(Console.ReadLine()!);
                    if (i >= 0 && i <= Bracket.ScoreObjective)
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer can't be lower than 0 or upper than {0}.", Bracket.ScoreObjective);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e);
                    Console.ResetColor();
                    throw;
                }
            }
            Bracket.Score[1] = i;
            
            // Set the winner
            Bracket.CheckMatch();
            Bracket.Print();
        }
    }
}