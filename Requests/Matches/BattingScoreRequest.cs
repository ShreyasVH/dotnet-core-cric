using System.Collections.Generic;

namespace Com.Dotnet.Cric.Requests.Matches
{
    public class BattingScoreRequest
    {
        public long PlayerId { get; set; }
        public int Runs { get; set; }
        public int Balls { get; set; }
        public int Fours { get; set; }
        public int Sixes { get; set; }
        public int? DismissalModeId { get; set; }
        public long? BowlerId { get; set; }
        public List<long> FielderIds { get; set; }
        public int Innings { get; set; }
        public int? Number { get; set; }
    }
}