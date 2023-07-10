using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class BattingScore
    {
        public int Id { get; set; }
        [Required]
        public int MatchPlayerId { get; set; }
        public MatchPlayerMap BatsmanMatchPlayer { get; set; }
        [Required]
        public int Runs { get; set; }
        [Required]
        public int Balls { get; set; }
        [Required]
        public int Fours { get; set; }
        [Required]
        public int Sixes { get; set; }
        public int? DismissalModeId { get; set; }
        public DismissalMode DismissalMode { get; set; }
        public int? BowlerId { get; set; }
        public MatchPlayerMap BowlerMatchPlayer { get; set; }
        [Required]
        public int Innings { get; set; }
        public int? Number { get; set; }

        [JsonConstructor]
        public BattingScore()
        {
            
        }

        public BattingScore(BattingScoreRequest battingScoreRequest, Dictionary<long, int> playerToMatchPlayerMap)
        {
            Runs = battingScoreRequest.Runs;
            Balls = battingScoreRequest.Balls;
            Fours = battingScoreRequest.Fours;
            Sixes = battingScoreRequest.Sixes;
            DismissalModeId = battingScoreRequest.DismissalModeId;
            Innings = battingScoreRequest.Innings;
            Number = battingScoreRequest.Number;
            MatchPlayerId = playerToMatchPlayerMap[battingScoreRequest.PlayerId];
            if (battingScoreRequest.BowlerId.HasValue)
            {
                BowlerId = playerToMatchPlayerMap[battingScoreRequest.BowlerId.Value];
            }
        }
    }
}