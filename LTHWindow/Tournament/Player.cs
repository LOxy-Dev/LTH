using System;

namespace LTHWindow.Tournament
{
    public class Player
    {
        public string Name { get; set;  }
        public int WldRatio { get; private set; }
        public int Score { get; set; }

        public Player()
        {
            Name = "Player";
            WldRatio = 0;
            Score = 0;
        }

        public void AddResult(Result result)
        {
            switch (result)
            {
                case Result.Draw:
                    WldRatio += 1;
                    break;
                case Result.Win:
                    WldRatio += 3;
                    break;
                case Result.Loss:
                    WldRatio -= 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
    }
}