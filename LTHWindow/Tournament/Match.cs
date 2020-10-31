namespace LTHWindow.Tournament
{
    public class Match
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public Match()
        {
            Player1 = null;
            Player2 = null;
        }
    }
}