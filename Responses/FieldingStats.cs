using System.Collections.Generic;

namespace Com.Dotnet.Cric.Responses
{
    public class FieldingStats
    {
        public int Catches { get; set; } = 0;
        public int RunOuts { get; set; } = 0;
        public int Stumpings { get; set; } = 0;

        public FieldingStats(Dictionary<string, int> fieldingStats)
        {
            Catches = fieldingStats.GetValueOrDefault("Caught", 0);
            RunOuts = fieldingStats.GetValueOrDefault("Run Out", 0);
            Stumpings = fieldingStats.GetValueOrDefault("Stumped", 0);
        }
    }
}