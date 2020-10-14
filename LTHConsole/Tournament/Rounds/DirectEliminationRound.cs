using System;
using System.Collections.Generic;
using System.Linq;
using LTHConsole.Tournament.Brackets;

namespace LTHConsole.Tournament.Rounds
{
    public class DirectEliminationRound : Round
    {
        public override void Init()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Enter the number of participants..."); 
            Console.ResetColor();

            int answer; 
            while (true) 
            { 
                try 
                { 
                    answer = Int32.Parse(Console.ReadLine()!);

                    if (answer > 0 && answer <= Program.Tournament.NbPlayer)
                    {
                        break;
                    }
                    // Print an error if the answer is lower than 0 or upper than the number of player registered
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer is not valid");
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer is not an integer.");
                    Console.ResetColor();
                }
            }
            List<Player> participants = new List<Player>(answer);
            
            Console.ResetColor();
            // Quick select
            if (answer == Program.Tournament.NbPlayer)
            {
                participants = Program.Tournament.Players;
            }
            // Manual select
            else
            {
                for (int i = 0; i < answer; i++)
                {
                    // Write the list of the players
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Allowed players :");
                    int j = 1;
                    foreach (var player in Program.Tournament.Players)
                    {
                        if (!participants.Contains(player))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("  {0}) : {1}", j, player.Name);
                            j++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("  {0}", player.Name);
                        }
                    }

                    Console.ResetColor();

                    // Add the selected player to the array
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Enter the digit of the participant...");
                    Console.ResetColor();
                    while (true)
                    {
                        try
                        {
                            int input = Int32.Parse(Console.ReadLine()!);
                            if (input < j && input > 0)
                            {
                                participants.Add(Program.Tournament.Players.ElementAt(input - 1));
                                break;
                            }

                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("ERROR : Your answer is not allowed.");
                        }
                        catch (FormatException)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("ERROR : Your answer is not an integer");
                            Console.ResetColor();
                            throw;
                        }
                    }
                }
            }
            
            Bracket = new Direct(participants);
        }
        
        public override void Print()
        {
            
        }
    }
}