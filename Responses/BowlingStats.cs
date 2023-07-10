using System.Collections.Generic;

namespace Com.Dotnet.Cric.Responses
{
    public class BowlingStats
    {
        public int Innings { get; set; }
        public int Balls { get; set; }
        public int Maidens { get; set; }
        public int Runs { get; set; }
        public int Wickets { get; set; }
        public double? Economy { get; set; }
        public double? Average { get; set; }
        public double StrikeRate { get; set; }
        public int Fifers { get; set; }
        public int TenWickets { get; set; }

        public BowlingStats(Dictionary<string, int> basicStats)
        {
            Innings = basicStats.GetValueOrDefault("innings", 0);
            Balls = basicStats.GetValueOrDefault("balls", 0);
            Maidens = basicStats.GetValueOrDefault("maidens", 0);
            Runs = basicStats.GetValueOrDefault("runs", 0);
            Wickets = basicStats.GetValueOrDefault("wickets", 0);
            Fifers = basicStats.GetValueOrDefault("fifers", 0);
            TenWickets = basicStats.GetValueOrDefault("tenWickets", 0);
        }
    }
}