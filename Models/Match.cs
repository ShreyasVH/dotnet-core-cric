using System;
using System.ComponentModel.DataAnnotations;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Required]
        public int SeriesId { get; set; }
        
        public Series Series { get; set; }
        
        [Required]
        public long Team1Id { get; set; }
        
        public Team Team1 { get; set; }
        
        [Required]
        public long Team2Id { get; set; }
        
        public Team Team2 { get; set; }
        
        public long? TossWinnerId { get; set; }
        
        public Team TossWinner { get; set; }
        
        public long? BatFirstId { get; set; }
        
        public Team BatFirst { get; set; }
        
        [Required]
        public int ResultTypeId { get; set; }
        
        public ResultType ResultType { get; set; }
        
        public long? WinnerId { get; set; }
        
        public Team Winner { get; set; }
        
        public int? WinMargin { get; set; }
        
        public int? WinMarginTypeId { get; set; }
        
        public WinMarginType WinMarginType { get; set; }
        
        [Required]
        public long StadiumId { get; set; }
        
        public Stadium Stadium { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public bool IsOfficial { get; set; }

        public Match()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Match(CreateRequest createRequest)
        {
            SeriesId = createRequest.SeriesId;
            Team1Id = createRequest.Team1Id;
            Team2Id = createRequest.Team2Id;
            TossWinnerId = createRequest.TossWinnerId;
            BatFirstId = createRequest.BatFirstId;
            ResultTypeId = createRequest.ResultTypeId;
            WinnerId = createRequest.WinnerId;
            WinMargin = createRequest.WinMargin;
            WinMarginTypeId = createRequest.WinMarginTypeId;
            StadiumId = createRequest.StadiumId;
            StartTime = createRequest.StartTime.Value;
            IsOfficial = createRequest.IsOfficial;
        }
    }
}