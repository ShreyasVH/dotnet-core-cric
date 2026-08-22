namespace Com.Dotnet.Cric.Requests.Matches
{
    public class PartnershipRequest
    {
        public int Innings { get; set; }
        public int Wicket { get; set; }
        public int Runs { get; set; }
        public int Balls { get; set; }
        public bool Ended { get; set; }
        public long PlayerId1 { get; set; }
        public int Runs1 { get; set; }
        public int Balls1 { get; set; }
        public long PlayerId2 { get; set; }
        public int Runs2 { get; set; }
        public int Balls2 { get; set; }
    }
}