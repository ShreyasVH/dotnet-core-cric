using System.Collections.Generic;

namespace dotnet.Requests
{
    public class FilterRequest
    {
        public string Type { get; set; }
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 30;
        public Dictionary<string, List<string>> Filters = new Dictionary<string, List<string>>();
        public Dictionary<string, Dictionary<string, string>> RangeFilters =
            new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, string> SortMap = new Dictionary<string, string>();
    }
}