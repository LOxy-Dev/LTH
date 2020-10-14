using System;
using System.Collections.Generic;
using System.Threading;
using LTHConsole.Tournament;
using LTHConsole.Tournament.Rounds;

namespace LTHConsole
{
    internal static class Program
    {
        public const string Version = "v. 1.0";

        private static bool _running;

        public static Tournament.Tournament Tournament { get; private set; }

        private static void Start()
        {
            // Set title
            Console.Title = $"TLH Console {Version}";
            
            // Print starting message
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=========================================");
            Console.WriteLine("LAN Tournament Helper\nMade by LOxy\n{0}", Version);
            Console.WriteLine("=========================================\n");
            Console.ResetColor();
            Thread.Sleep(5000);

            // Create the tournament object
            // Get name
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Please enter the name of the tournament...\n");
            Console.ResetColor();
            string tName = Console.ReadLine();
            // Get number of players
            int tNbPlayer;
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Please enter the number of player of the tournament...");
                    Console.ResetColor();
                    tNbPlayer = Int32.Parse(Console.In.ReadLine()!);
                    break;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer is not an integer.");
                    Console.ResetColor();
                    throw;
                }
            }
            // Get player's name
            List<Player> tPlayers = new List<Player>();
            List<string> names = new List<string>();
            for (int i = 0; i < tNbPlayer; i++)
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Enter the name of player " + (i + 1) + "...");
                    Console.ResetColor();
                    string input = Console.ReadLine()!;
                    // Check if this name is empty or already in list.
                    if (input == "" || names.Contains(input))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("ERROR : A player name can't be twice or empty.");
                        Console.ResetColor();
                    }
                    else
                    {
                        tPlayers.Add(new Player(input));
                        names.Add(input);
                        break;
                    }
                }
            }
            // Get type of tournament
            Round tRound = new Round();
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Enter the type of Tournament :\n 1) Championship format\n 2) Direct elimination tournament\n 3) Multi-rounds tournament");
                    Console.ResetColor();
                    int input = Int32.Parse(Console.ReadLine()!);
                    if (input < 4 && input > 0)
                    {
                        switch (input)
                        {
                            case 1:
                                tRound = new GroupRound();
                                break;
                            case 2:
                                tRound = new DirectEliminationRound();
                                break;
                            case 3:
                                tRound = new MultiRounds();
                                break;
                        }
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR : Your answer is not an integer");
                    Console.ResetColor();
                    throw;
                }
            }
            // Create and init the tournament
            Tournament = new Tournament.Tournament(tName, tNbPlayer, tPlayers, tRound);
            Tournament.Init();
            
            // Start the program's loop
            _running = true;
            Run();
        }

        private static void Run()
        {
            while (_running)
            {
                Tournament.Print();
            }
            Console.WriteLine("Loop finished");
        }

        public static void Stop()
        {
            _running = false;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            Start();
        }
    }

    public static class StringExtensions
    {
        public static string CenterString(this string stringToCenter, int totalLength)
        {
            return stringToCenter.PadLeft(((totalLength - stringToCenter.Length) / 2)
                                          + stringToCenter.Length)
                .PadRight(totalLength);
        }
    }
}