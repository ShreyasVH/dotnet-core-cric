using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class DismissalModeResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public DismissalModeResponse()
        {

        }

        public DismissalModeResponse(DismissalMode dismissalMode)
        {
            Id = dismissalMode.Id;
            Name = dismissalMode.Name;
        }
    }
}