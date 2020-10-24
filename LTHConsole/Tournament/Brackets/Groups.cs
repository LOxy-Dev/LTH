using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LTHConsole.Tournament.Brackets
{
    // TODO Add a multi group system
    // TODO Add a first and second leg matches system
    // TODO Organize the order of matches (actually P1 plays all his matches the P2...)
    public class Groups : Bracket
    {
        public Groups(List<Player> players) : base(players)
        {
            
        }

        public override void Init()
        {
            // Generate matches
            for (int i = 0; i < Players.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    Match match = new Match(Players.ElementAt(j), Players.ElementAt(i));
                    Matches.Add(match);
                }
            }
            // Randomize order
            var rnd = new Random();
            Matches = Matches.OrderBy(x => rnd.Next()).ToList();
        }

        public override void CheckMatch()
        {
            var match = GetActualMatch();
            if (match.Score[0] == match.Score[1])
            {
                Draw();
            }
            else if (match.Score[0] > ScoreObjective)
            {
                Win(match.Player1, match.Player2);
            }
            else
            {
                Win(match.Player2, match.Player1);
            }
            // Ordering players by score
            Players = Players.OrderByDescending(i => i.WLDRatio).ThenByDescending(j => j.Score).ToList();
            base.CheckMatch();
        }

        private void Win(Player winner, Player looser)
        {
            var match = GetActualMatch();

            var pts = Math.Abs(match.Score[0] - match.Score[1]);
            
            winner.AddResult(Results.Win);
            looser.AddResult(Results.Loss);

            winner.Score += pts;
            looser.Score -= pts;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n{0} has won!\n", winner.Name);
            Console.ResetColor();
        }

        private void Draw()
        {
            var actualMatch = GetActualMatch();
            var p1 = actualMatch.Player1;
            var p2 = actualMatch.Player2;

            p1.AddResult(Results.Draw);
            p2.AddResult(Results.Draw);
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nDraw!\n");
            Console.ResetColor();
        }

        public override void Print()
        {
            var longestName = Players.Max(r => r.Name.Length);
            var firstLine = "|  Position  |  WLD  |  Score  |";
            var builder = new StringBuilder();
            builder.Append("|  ");
            for (int i = 0; i < longestName; i++)
            {
                builder.Append("x");
            }

            builder.Append("  ");
            
            var line = new StringBuilder();
            for (int i = 0; i < firstLine.Length; i++)
            {
                line.Append(i % 1 == 0 ? "-" : "--");
            }

            line.Append("------");
            firstLine = builder + firstLine;

            Console.ForegroundColor = ConsoleColor.Yellow;
            // Write the first line
            Console.WriteLine(line);
            Console.WriteLine(firstLine);
            Console.WriteLine(line);
            
            // Write the players' lines
            foreach (var me in Players)
            {
                // Write name
                Console.Write("| ");
                Console.Write(me.Name.CenterString(longestName + 2));
                Console.Write(" |");
                // Write Position
                Console.Write((Players.IndexOf(me) + 1).ToString().CenterString("  Position  ".Length));
                Console.Write("|");
                // Write WLD
                var wld = new StringBuilder();
                wld.Append(me.WLD[0]);
                wld.Append("/");
                wld.Append(me.WLD[1]);
                wld.Append("/");
                wld.Append(me.WLD[2]);
                Console.Write(wld.ToString().CenterString("  WLD  ".Length));
                Console.Write("|");
                // Write score
                Console.WriteLine(me.Score.ToString().CenterString("  Score  ".Length) + "|");
                // Write separator
                Console.WriteLine(line);
            }
            
            Console.ResetColor();
        }
    }
}