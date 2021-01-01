namespace LTHWindow.Tournament
{
    public class Match
    {
        public Player Player1 { get; }
        public Player Player2 { get; }
        
        public int[] Scores { get; set; }
        
        public string Completion { get; set; }

        public Match(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            
            Scores = new int[2];

            Completion = "Not played";
        }
    }
}