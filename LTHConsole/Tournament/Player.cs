using System.Dynamic;

namespace LTHConsole.Tournament
{
    public class Player
    {
        public string Name { get; }
        public int WLDRatio { get; private set; }
        public int[] WLD { get; private set; }
        public int Score { get; set; }

        public Player(string name)
        {
            Name = name;
            WLD = new[] {0, 0, 0};
            WLDRatio = 0;
        }

        public void AddResult(Results result)
        {
            switch (result)
            {
                case Results.Draw:
                    WLD[2]++;
                    WLDRatio += 2;
                    break;
                case Results.Win:
                    WLD[0]++;
                    WLDRatio += 5;
                    break;
                case Results.Loss:
                    WLD[1]++;
                    WLDRatio -= 5;
                    break;
            }
        }
        
        public void ClearScore()
        {
            WLD = new[] {0, 0, 0};
            WLDRatio = 0;
            Score = 0;
        }
    }
}