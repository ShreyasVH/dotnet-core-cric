using System.Collections.Generic;
using dotnet.Migrations;
using BattingScore = Com.Dotnet.Cric.Models.BattingScore;

namespace Com.Dotnet.Cric.Responses
{
    public class BattingScoreResponse
    {
        public int Id { get; set; }
        public PlayerResponse Player { get; set; }
        public int Runs { get; set; }
        public int Balls { get; set; }
        public int Fours { get; set; }
        public int Sixes { get; set; }
        public DismissalModeResponse DismissalMode { get; set; }
        public PlayerResponse Bowler { get; set; }
        public List<PlayerResponse> Fielders { get; set; }
        public int Innings { get; set; }
        public int? Number { get; set; }

        public BattingScoreResponse(BattingScore battingScore, PlayerResponse player, DismissalModeResponse dismissalMode, PlayerResponse bowler, List<PlayerResponse> fielders)
        {
            Id = battingScore.Id;
            Player = player;
            Runs = battingScore.Runs;
            Balls = battingScore.Balls;
            Fours = battingScore.Fours;
            Sixes = battingScore.Sixes;
            DismissalMode = dismissalMode;
            Bowler = bowler;
            Fielders = fielders;
            Innings = battingScore.Innings;
            Number = battingScore.Number;
        }
    }
}