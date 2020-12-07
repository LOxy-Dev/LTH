using System;
using System.Collections.Generic;
using LTHConsole.Tournament.Rounds;

namespace LTHConsole.Tournament
{
    public class Tournament
    {
        private readonly string _name;

        public readonly Round Round;

        public int NbPlayer { get; }
        public List<Player> Players { get; }

        public Tournament(string name, int nbPlayer, List<Player> players, Round round)
        {
            _name = name;
            NbPlayer = nbPlayer;
            Players = players;
            Round = round;
        }

        public void Init()
        {
            // Set the new title
            Console.Title = $"LTH Console {Program.Version} : {_name}";
            Round.Init();
        }

        public void Print()
        {
            while (!Round.Bracket.IsFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tournament : {0}\n", _name);
            
                Round.Print();
            
                Console.ResetColor(); 
            }
            Program.Stop();
        }
    }
}