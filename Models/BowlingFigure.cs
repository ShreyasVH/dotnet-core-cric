using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class BowlingFigure
    {
        public int Id { get; set; }
        [Required]
        public int MatchPlayerId { get; set; }
        public MatchPlayerMap MatchPlayerMap { get; set; }
        [Required]
        public int Balls { get; set; }
        [Required]
        [Column(TypeName = "tinyint")]
        public byte Maidens { get; set; }
        [Required]
        public int Runs { get; set; }
        [Required]
        public int Wickets { get; set; }
        [Required]
        public int Innings { get; set; }

        [JsonConstructor]
        public BowlingFigure()
        {
            
        }

        public BowlingFigure(BowlingFigureRequest bowlingFigureRequest, Dictionary<long, int> playerToMatchPlayerMap)
        {
            Balls = bowlingFigureRequest.Balls;
            Maidens = bowlingFigureRequest.Maidens;
            Runs = bowlingFigureRequest.Runs;
            Wickets = bowlingFigureRequest.Wickets;
            Innings = bowlingFigureRequest.Innings;
            MatchPlayerId = playerToMatchPlayerMap[bowlingFigureRequest.PlayerId];
        }
    }
}