using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class WinMarginTypeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public WinMarginTypeResponse()
        {

        }

        public WinMarginTypeResponse(WinMarginType winMarginType)
        {
            Id = winMarginType.Id;
            Name = winMarginType.Name;
        }
    }
}