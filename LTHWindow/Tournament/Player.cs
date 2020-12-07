using System;

namespace LTHWindow.Tournament
{
    public class Player
    {
        public string Name { get; set;  }
        public int[] Wld { get; private set; }
        public int Score { get; set; }

        public Player()
        {
            Wld = new []{0, 0, 0};
            Name = "Player";
            Score = 0;
        }

        public void AddResult(Result result)
        {
            switch (result)
            {
                case Result.Draw:
                    Wld[2]++;
                    break;
                case Result.Win:
                    Wld[0]++;
                    break;
                case Result.Loss:
                    Wld[1]++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
    }
}