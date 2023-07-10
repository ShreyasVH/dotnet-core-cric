using System.Collections.Generic;

namespace Com.Dotnet.Cric.Responses
{
    public class BattingStats
    {
        public int Runs { get; set; }
        public int Balls { get; set; }
        public int Innings { get; set; }
        public int Fours { get; set; }
        public int Sixes { get; set; }
        public int NotOuts { get; set; } = 0;
        public int Highest { get; set; }
        public double? Average { get; set; }
        public double? StrikeRate { get; set; }
        public int Fifties { get; set; }
        public int Hundreds { get; set; }
        public int TwoHundreds { get; set; }
        public int ThreeHundreds { get; set; }
        public int FourHundreds { get; set; }

        public BattingStats(Dictionary<string, int> basicStats)
        {
            Runs = basicStats.GetValueOrDefault("runs", 0);
            Balls = basicStats.GetValueOrDefault("balls", 0);
            Innings = basicStats.GetValueOrDefault("innings", 0);
            Fours = basicStats.GetValueOrDefault("fours", 0);
            Sixes = basicStats.GetValueOrDefault("sixes", 0);
            Highest = basicStats.GetValueOrDefault("highest");
            Fifties = basicStats.GetValueOrDefault("fifties");
            Hundreds = basicStats.GetValueOrDefault("hundreds");
            TwoHundreds = basicStats.GetValueOrDefault("twoHundreds");
            ThreeHundreds = basicStats.GetValueOrDefault("threeHundreds");
            FourHundreds = basicStats.GetValueOrDefault("fourHundreds");
        }
    }
}