using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class ExtrasResponse
    {
        public int Id { get; set; }
        public int Runs { get; set; }
        public ExtrasTypeResponse Type { get; set; }
        public TeamResponse BattingTeam { get; set; }
        public TeamResponse BowlingTeam { get; set; }
        public int Innings { get; set; }

        public ExtrasResponse(Extras extras, ExtrasTypeResponse extrasTypeResponse, TeamResponse battingTeam, TeamResponse bowlingTeam)
        {
            Id = extras.Id;
            Runs = extras.Runs;
            Type = extrasTypeResponse;
            BattingTeam = battingTeam;
            BowlingTeam = bowlingTeam;
            Innings = extras.Innings;
        }
    }
}