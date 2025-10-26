using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Com.Dotnet.Cric.Requests.Teams;

namespace Com.Dotnet.Cric.Models
{
    public class ManOfTheSeries
    {
        public long Id { get; set; }
        [Required]
        [ForeignKey("Series")]
        public int SeriesId { get; set; }

        public Series Series { get; set; }

        [Required]
        [ForeignKey("Player")]
        public long PlayerId { get; set; }

        public Player Player { get; set; }

        [JsonConstructor]
        public ManOfTheSeries()
        {
            // Parameterless constructor required by Entity Framework Core
        }

        public ManOfTheSeries(int seriesId, long playerId)
        {
            SeriesId = seriesId;
            PlayerId = playerId;
        }
    }
}