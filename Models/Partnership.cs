using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Com.Dotnet.Cric.Requests.Matches;

namespace Com.Dotnet.Cric.Models
{
    public class Partnership
    {
        public int Id { get; set; }
        [Required]
        public int Innings { get; set; }
        [Required]
        public int Wicket { get; set; }
        [Required]
        public int Runs { get; set; }
        [Required]
        public int Balls { get; set; }
        [Required]
        public bool Ended { get; set; }
        [Required]
        public int MatchPlayerId1 { get; set; }
        public MatchPlayerMap MatchPlayerMap1 { get; set; }
        [Required]
        public int Runs1 { get; set; }
        [Required]
        public int Balls1 { get; set; }
        [Required]
        public int MatchPlayerId2 { get; set; }
        public MatchPlayerMap MatchPlayerMap2 { get; set; }
        [Required]
        public int Runs2 { get; set; }
        [Required]
        public int Balls2 { get; set; }
        [Required]
        public bool PrimaryEntry { get; set; }
        

        [JsonConstructor]
        public Partnership()
        {
            
        }

        public Partnership(PartnershipRequest partnershipRequest, Dictionary<long, int> playerToMatchPlayerMap,  bool primary)
        {
            Innings = partnershipRequest.Innings;
            Wicket = partnershipRequest.Wicket;
            Runs = partnershipRequest.Runs;
            Balls = partnershipRequest.Balls;
            Ended = partnershipRequest.Ended;
            if (primary)
            {
                MatchPlayerId1 = playerToMatchPlayerMap[partnershipRequest.PlayerId1];
                Runs1 = partnershipRequest.Runs1;
                Balls1 = partnershipRequest.Balls1;
                MatchPlayerId2 = playerToMatchPlayerMap[partnershipRequest.PlayerId2];
                Runs2 = partnershipRequest.Runs2;
                Balls2 = partnershipRequest.Balls2;                
            }
            else
            {
                MatchPlayerId1 = playerToMatchPlayerMap[partnershipRequest.PlayerId2];
                Runs1 = partnershipRequest.Runs2;
                Balls1 = partnershipRequest.Balls2;
                MatchPlayerId2 = playerToMatchPlayerMap[partnershipRequest.PlayerId1];
                Runs2 = partnershipRequest.Runs1;
                Balls2 = partnershipRequest.Balls1;
            }
            PrimaryEntry = primary;
        }
    }
}