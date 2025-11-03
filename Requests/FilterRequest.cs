using System.Collections.Generic;

namespace dotnet.Requests
{
    public class FilterRequest
    {
        public string Type { get; set; }
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 30;
        public Dictionary<string, List<string>> Filters { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, Dictionary<string, string>> RangeFilters { get; set; } =
            new Dictionary<string, Dictionary<string, string>>();

        public Dictionary<string, string> SortMap { get; set; } = new Dictionary<string, string>();
    }
}