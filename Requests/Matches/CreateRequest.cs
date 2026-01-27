using System;
using System.Collections.Generic;

namespace Com.Dotnet.Cric.Requests.Matches
{
    public class CreateRequest
    {
        public int SeriesId { get; set; }
        public long Team1Id { get; set; }
        public long Team2Id { get; set; }
        public long? TossWinnerId { get; set; }
        public long? BatFirstId { get; set; }
        public int ResultTypeId { get; set; }
        public long? WinnerId { get; set; }
        public int? WinMargin { get; set; }
        public int? WinMarginTypeId { get; set; }
        public long StadiumId { get; set; }
        public DateTime? StartTime { get; set; }
        public bool IsOfficial { get; set; } = true;

        public List<PlayerRequest> Players { get; set; }
        public List<PlayerRequest> Bench { get; set; }
        public List<BattingScoreRequest> BattingScores { get; set; }
        public List<BowlingFigureRequest> BowlingFigures { get; set; }
        public List<ExtrasRequest> Extras { get; set; }
        public List<long> Captains { get; set; }
        public List<long> WicketKeepers { get; set; }
        public List<long> ManOfTheMatchList { get; set; }
        public List<TotalRequestEntry> Totals { get; set; }
        public List<int> Tags { get; set; }

        public void Validate()
        {
            
        }
    }
}