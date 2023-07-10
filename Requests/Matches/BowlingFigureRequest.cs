namespace Com.Dotnet.Cric.Requests.Matches
{
    public class BowlingFigureRequest
    {
        public long PlayerId { get; set; }
        public int Balls { get; set; }
        public byte Maidens { get; set; }
        public int Runs { get; set; }
        public int Wickets { get; set; }
        public int Innings { get; set; }
    }
}