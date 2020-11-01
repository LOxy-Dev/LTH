using System;

namespace LTHWindow.Tournament
{
    public class Player
    {
        public string Name { get; set;  }
        public int WLDRatio { get; private set; }
        public int Score { get; set; }

        public Player()
        {
            Name = "Player";
            WLDRatio = 0;
            Score = 0;
        }

        public void AddResult(Result result)
        {
            switch (result)
            {
                case Result.Draw:
                    WLDRatio += 1;
                    break;
                case Result.Win:
                    WLDRatio += 3;
                    break;
                case Result.Loss:
                    WLDRatio -= 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
    }
}