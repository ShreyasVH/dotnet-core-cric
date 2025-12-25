using System.Collections.Generic;

namespace Com.Dotnet.Cric.Responses
{
    public class StatsResponse
    {
        public int Count { get; set; } = 0;
        public List<Dictionary<string, string>> Stats { get; set; } = new List<Dictionary<string, string>>();
    }
}