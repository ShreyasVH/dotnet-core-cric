using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Com.Dotnet.Cric.Requests.Series;

namespace Com.Dotnet.Cric.Models
{
    public class Series
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [Required]
        [ForeignKey("HomeCountry")]
        public long HomeCountryId { get; set; }

        public Country HomeCountry { get; set; }
        
        [Required]
        [ForeignKey("Tour")]
        public long TourId { get; set; }

        public Tour Tour { get; set; }

        [Required]
        [ForeignKey("SeriesType")]
        public int TypeId { get; set; }

        public SeriesType Type { get; set; }
        
        [Required]
        [ForeignKey("GameType")]
        public int GameTypeId { get; set; }

        public GameType GameType { get; set; }
        
        [Required]
        public DateTime StartTime { get; set; }

        [JsonConstructor]
        public Series()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public Series(CreateRequest createRequest)
        {
            Name = createRequest.Name;
            HomeCountryId = createRequest.HomeCountryId;
            TourId = createRequest.TourId;
            TypeId = createRequest.TypeId;
            GameTypeId = createRequest.GameTypeId;
            StartTime = createRequest.StartTime.Value;
        }
    }
}