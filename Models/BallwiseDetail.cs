
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class BallwiseDetail
    {
        public int Id { get; set; }
        [Required]
        public int BatsmanMatchPlayerId { get; set; }
        public MatchPlayerMap BatsmanMatchPlayer { get; set; }
        [Required]
        public int BowlerMatchPlayerId { get; set; }
        public MatchPlayerMap BowlerMatchPlayer { get; set; }
        [Required]
        public int Innings { get; set; }
        [Required]
        public int Ball { get; set; }
        [Required]
        public int TotalRuns { get; set; }
        [Required]
        public int BatsmanRuns { get; set; }
        [Required]
        public int BowlerRuns { get; set; }
        [Required]
        public int ExtrasRuns { get; set; }
        [Required]
        public string ExtrasType { get; set; }
        [Required]
        public bool Dismissal { get; set; }
        [Required]
        public long Timestamp { get; set; }

        [JsonConstructor]
        public BallwiseDetail()
        {
            
        }

        public BallwiseDetail(BallwiseDetailRequest ballwiseDetailRequest, Dictionary<long, int> playerToMatchPlayerMap)
        {
            BatsmanMatchPlayerId = playerToMatchPlayerMap[ballwiseDetailRequest.BatsmanPlayerId];
            BowlerMatchPlayerId = playerToMatchPlayerMap[ballwiseDetailRequest.BowlerPlayerId];
            Innings = ballwiseDetailRequest.Innings;
            Ball = ballwiseDetailRequest.Ball;
            TotalRuns = ballwiseDetailRequest.TotalRuns;
            BatsmanRuns = ballwiseDetailRequest.BatsmanRuns;
            BowlerRuns = ballwiseDetailRequest.BowlerRuns;
            ExtrasRuns = ballwiseDetailRequest.ExtrasRuns;
            ExtrasType = ballwiseDetailRequest.ExtrasType;
            Dismissal = ballwiseDetailRequest.Dismissal;
            Timestamp = ballwiseDetailRequest.Timestamp;
        }
    }
}