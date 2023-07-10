using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Com.Dotnet.Cric.Models
{
    public class FielderDismissal
    {
        public int Id { get; set; }
        [Required]
        public int ScoreId { get; set; }
        public BattingScore BattingScore { get; set; }
        [Required]
        public int MatchPlayerId { get; set; }
        public MatchPlayerMap MatchPlayerMap { get; set; }

        [JsonConstructor]
        public FielderDismissal()
        {
            
        }
    }
}