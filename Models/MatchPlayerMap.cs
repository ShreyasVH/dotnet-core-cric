using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Com.Dotnet.Cric.Models
{
    public class MatchPlayerMap
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Match")]
        public int MatchId;

        public Match Match;
        [Required]
        [ForeignKey("Player")]
        public long PlayerId { get; set; }

        public Player Player;
        [Required]
        [ForeignKey("Team")]
        public long TeamId { get; set; }

        public Team Team;

        [JsonConstructor]
        public MatchPlayerMap()
        {
            
        }
    }
}