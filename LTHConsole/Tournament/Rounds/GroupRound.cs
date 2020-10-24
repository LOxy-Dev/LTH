using System;
using System.Collections.Generic;
using System.Linq;
using LTHConsole.Tournament.Brackets;

namespace LTHConsole.Tournament.Rounds
{
    public class GroupRound : Round
    {
        public override void Init()
        {
            InitGroups();
            InitParticipants();
        }

        // This method init the number of groups
        private void InitGroups()
        {
            
        }

        // This method init the participants
        private void InitParticipants()
        {
            // Select the number of participants
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
            
            // If we selected whole participants
            if (answer == Program.Tournament.NbPlayer)
            {
                participants = Program.Tournament.Players;
            }
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
                        Console.ForegroundColor = !participants.Contains(player) ? ConsoleColor.Yellow : ConsoleColor.DarkGray;
                        Console.WriteLine("  {0}) : {1}", j, player.Name);
                        j++;
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
                                participants.Add(Program.Tournament.Players.ElementAt((input - 1)));
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

            // Create the bracket
            Bracket = new Groups(participants);
            Bracket.Type = Objectives.BestOf;
            Bracket.ScoreObjective = 5;
            Bracket.Init();
        }
    }
}