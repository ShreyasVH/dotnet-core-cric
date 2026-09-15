namespace Com.Dotnet.Cric.Requests.Matches
{
    public class BallwiseDetailRequest
    {
        public long BatsmanPlayerId { get; set; }
        public long BowlerPlayerId { get; set; }
        public int Innings { get; set; }
        public int Ball { get; set; }
        public int TotalRuns { get; set; }
        public int BatsmanRuns { get; set; }
        public int BowlerRuns { get; set; }
        public int ExtrasRuns { get; set; }
        public string ExtrasType { get; set; }
        public bool Dismissal { get; set; }
        public long Timestamp { get; set; }
    }
}