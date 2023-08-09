using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class MatchMiniResponse
    {
        public int Id { get; set; }
        public TeamResponse Team1 { get; set; }
        public TeamResponse Team2 { get; set; }
        public ResultTypeResponse ResultType { get; set; }
        public TeamResponse Winner { get; set; }
        public int? WinMargin { get; set; }
        public WinMarginTypeResponse WinMarginType { get; set; }
        public StadiumResponse Stadium { get; set; }
        public DateTime StartTime { get; set; }
        
        public MatchMiniResponse(Match match, TeamResponse team1, TeamResponse team2, ResultType resultType, WinMarginType winMarginType, StadiumResponse stadium)
        {
            Id = match.Id;
            Team1 = team1;
            Team2 = team2;

            var teamMap = (new List<TeamResponse> {team1, team2}).ToDictionary(t => t.Id, t => t);

            if (match.WinnerId.HasValue)
            {
                Winner = teamMap[match.WinnerId.Value];
                WinMargin = match.WinMargin;
                WinMarginType = new WinMarginTypeResponse(winMarginType);
            }

            ResultType = new ResultTypeResponse(resultType);
            Stadium = stadium;
            StartTime = match.StartTime;
        }
    }
}