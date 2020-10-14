using System;
using System.Collections.Generic;
using LTHConsole.Tournament.Rounds;

namespace LTHConsole.Tournament
{
    public class Tournament
    {
        private readonly string _name;

        private readonly Round _round;

        public int NbPlayer { get; }
        public List<Player> Players { get; }

        public Tournament(string name, int nbPlayer, List<Player> players, Round round)
        {
            _name = name;
            NbPlayer = nbPlayer;
            Players = players;
            _round = round;
        }

        public void Init()
        {
            // Set the new title
            Console.Title = $"LTH Console {Program.Version} : {_name}";
            _round.Init();
        }

        public void Print()
        {
            while (!_round.Bracket.IsFinished)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Tournament : {0}\n", _name);
            
                _round.Print();
            
                Console.ResetColor(); 
            }
            Program.Stop();
        }
    }
}