using System;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class SeriesMiniResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long HomeCountryId { get; set; }
        public long TourId { get; set; }
        public int TypeId { get; set; }
        public int GameTypeId { get; set; }
        public DateTime StartTime { get; set; }

        public SeriesMiniResponse(Series series)
        {
            Id = series.Id;
            Name = series.Name;
            HomeCountryId = series.HomeCountryId;
            TourId = series.TourId;
            TypeId = series.TypeId;
            GameTypeId = series.GameTypeId;
            StartTime = series.StartTime;
        }
    }
}