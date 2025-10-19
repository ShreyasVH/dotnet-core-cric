using System;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesMiniResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long HomeCountryId { get; set; }
        public long TourId { get; set; }
        public int TypeId { get; set; }
        public GameTypeResponse GameType { get; set; }
        public DateTime StartTime { get; set; }

        public SeriesMiniResponse(Series series, GameType gameType)
        {
            Id = series.Id;
            Name = series.Name;
            HomeCountryId = series.HomeCountryId;
            TourId = series.TourId;
            TypeId = series.TypeId;
            GameType = new GameTypeResponse(gameType);
            StartTime = series.StartTime;
        }
    }
}