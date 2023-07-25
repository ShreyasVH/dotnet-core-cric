using System;
using System.Collections.Generic;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CountryResponse HomeCountry { get; set; }
        public TourMiniResponse Tour { get; set; }
        public SeriesTypeResponse Type { get; set; }
        public GameTypeResponse GameType { get; set; }
        public DateTime StartTime { get; set; }
        public List<TeamResponse> Teams { get; set; }

        public List<PlayerMiniResponse> ManOfTheSeriesList { get; set; }

        public SeriesResponse()
        {

        }

        public SeriesResponse(Series series, CountryResponse countryResponse, TourMiniResponse tourResponse, SeriesTypeResponse seriesTypeResponse, GameTypeResponse gameTypeResponse, List<TeamResponse> teams, List<PlayerMiniResponse> playerResponses)
        {
            this.Id = series.Id;
            this.Name = series.Name;
            this.HomeCountry = countryResponse;
            this.Tour = tourResponse;
            this.Type = seriesTypeResponse;
            this.GameType = gameTypeResponse;
            this.StartTime = series.StartTime;
            this.Teams = teams;
            this.ManOfTheSeriesList = playerResponses;
        }
    }
}