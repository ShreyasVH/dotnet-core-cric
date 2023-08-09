using System;
using System.Collections.Generic;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesDetailedResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public SeriesTypeResponse Type { get; set; }
        public GameTypeResponse GameType { get; set; }
        public DateTime StartTime { get; set; }
        public List<TeamResponse> Teams { get; set; }

        public List<MatchMiniResponse> Matches { get; set; }

        public SeriesDetailedResponse()
        {

        }

        public SeriesDetailedResponse(Series series, SeriesType seriesType, GameType gameType, List<TeamResponse> teams, List<MatchMiniResponse> matches)
        {
            Id = series.Id;
            Name = series.Name;
            Type = new SeriesTypeResponse(seriesType);
            GameType = new GameTypeResponse(gameType);
            StartTime = series.StartTime;
            Teams = teams;
            Matches = matches;
        }
    }
}