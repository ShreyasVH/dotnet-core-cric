using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Dotnet.Cric.Models
{
    public class SeriesTeamsMap
    {
        public long Id { get; set; }
        [Required]
        [ForeignKey("Series")]
        public int SeriesId { get; set; }
        
        public Series Series { get; set; }
        
        [Required]
        [ForeignKey("Team")]
        public long TeamId { get; set; }

        public Team Team { get; set; }

        [JsonConstructor]
        public SeriesTeamsMap()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public SeriesTeamsMap(int seriesId, long teamId)
        {
            SeriesId = seriesId;
            TeamId = teamId;
        }
    }
}