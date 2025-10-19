using System;
using System.Collections.Generic;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesDetailedResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SeriesTypeResponse Type { get; set; }
        public GameTypeResponse GameType { get; set; }
        public DateTime StartTime { get; set; }
        public List<TeamResponse> Teams { get; set; }

        public List<MatchMiniResponse> Matches { get; set; }
        
        public List<Tag> Tags { get; set; }

        public SeriesDetailedResponse()
        {

        }

        public SeriesDetailedResponse(Series series, SeriesType seriesType, GameType gameType, List<TeamResponse> teams, List<MatchMiniResponse> matches, List<Tag> tags)
        {
            Id = series.Id;
            Name = series.Name;
            Type = new SeriesTypeResponse(seriesType);
            GameType = new GameTypeResponse(gameType);
            StartTime = series.StartTime;
            Teams = teams;
            Matches = matches;
            Tags = tags;
        }
    }
}