using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class Total
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("Match")] 
        public int MatchId { get; set; }

        public Match Match;
        [Required]
        [ForeignKey("Team")]
        public long TeamId { get; set; }

        public Team Team;
        
        [Required]
        public int Runs { get; set; }
        [Required]
        public int Wickets { get; set; }
        [Required]
        public int Balls { get; set; }
        [Required]
        public int Innings { get; set; }

        [JsonConstructor]
        public Total()
        {
            
        }
        
        public Total(int matchId, TotalRequestEntry totalRequestEntry)
        {
            this.MatchId = matchId;
            TeamId = totalRequestEntry.TeamId;
            Runs = totalRequestEntry.Runs;
            Wickets = totalRequestEntry.Wickets;
            Balls = totalRequestEntry.Balls;
            Innings = totalRequestEntry.Innings;
        }
    }
}