using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Com.Dotnet.Cric.Models
{
    public class ManOfTheMatch
    {
        public int Id { set; get; }
        [Required]
        public int MatchPlayerId { get; set; }
        public MatchPlayerMap MatchPlayerMap { get; set; }

        [JsonConstructor]
        public ManOfTheMatch()
        {
            
        }
    }
}