using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class Extras
    {
        public int Id { get; set; }
        [Required]
        public int MatchId { get; set; }
        public Match Match { get; set; }
        [Required]
        public int TypeId { get; set; }
        public ExtrasType Type { get; set; }
        [Required] public int Runs { get; set; }

        [Required]
        public long BattingTeamId { get; set; }
        public Team BattingTeam { get; set; }
        [Required]
        public long BowlingTeamId { get; set; }
        public Team BowlingTeam { get; set; }
        [Required]
        public int Innings { get; set; }

        [JsonConstructor]
        public Extras()
        {
            
        }

        public Extras(int matchId, ExtrasRequest extrasRequest)
        {
            MatchId = matchId;
            TypeId = extrasRequest.TypeId;
            Runs = extrasRequest.Runs;
            BattingTeamId = extrasRequest.BattingTeamId;
            BowlingTeamId = extrasRequest.BowlingTeamId;
            Innings = extrasRequest.Innings;
        }
    }
}