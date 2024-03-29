using System;
using System.Collections.Generic;
using System.Linq;
using Com.Dotnet.Cric.Models;

namespace Com.Dotnet.Cric.Responses
{
    public class MatchResponse
    {
        public int Id { get; set; }
        public SeriesMiniResponse Series { get; set; }
        public TeamResponse Team1 { get; set; }
        public TeamResponse Team2 { get; set; }
        public TeamResponse TossWinner { get; set; }
        public TeamResponse BatFirst { get; set; }
        public ResultTypeResponse ResultType { get; set; }
        public TeamResponse Winner { get; set; }
        public int? WinMargin { get; set; }
        public WinMarginTypeResponse WinMarginType { get; set; }
        public StadiumResponse Stadium { get; set; }
        public DateTime StartTime { get; set; }
        
        public Dictionary<long, List<PlayerMiniResponse>> Players { get; set; }
        public List<BattingScoreResponse> BattingScores { get; set; }
        
        public List<BowlingFigureResponse> BowlingFigures { get; set; }
        public List<ExtrasResponse> Extras { get; set; }
        public List<PlayerMiniResponse> Captains { get; set; }
        public List<PlayerMiniResponse> WicketKeepers { get; set; }
        public List<PlayerMiniResponse> ManOfTheMatchList { get; set; }

        public MatchResponse(Match match, Series series, GameType gameType, TeamResponse team1, TeamResponse team2, ResultTypeResponse resultType, WinMarginTypeResponse winMarginType, StadiumResponse stadium, Dictionary<long, List<PlayerMiniResponse>> players, List<BattingScoreResponse> battingScores, List<BowlingFigureResponse> bowlingFigures, List<ExtrasResponse> extras, List<long> manOfTheMatchList, List<long> captainIds, List<long> wicketKeeperIds)
        {
            Id = match.Id;
            Series = new SeriesMiniResponse(series, gameType);
            Team1 = team1;
            Team2 = team2;

            var teamMap = (new List<TeamResponse> {team1, team2}).ToDictionary(t => t.Id, t => t);

            if (match.TossWinnerId.HasValue)
            {
                TossWinner = teamMap[match.TossWinnerId.Value];
                BatFirst = teamMap[match.BatFirstId.Value];
            }

            if (match.WinnerId.HasValue)
            {
                Winner = teamMap[match.WinnerId.Value];
                WinMargin = match.WinMargin;
                WinMarginType = winMarginType;
            }

            ResultType = resultType;
            Stadium = stadium;
            StartTime = match.StartTime;
            Players = players;
            var playerMap = new Dictionary<long, PlayerMiniResponse>();
            foreach (var entry in players)
            {
                foreach (var player in entry.Value)
                {
                    playerMap.Add(player.Id, player);
                }
            }
            BattingScores = battingScores;
            BowlingFigures = bowlingFigures;
            Extras = extras;
            Captains = captainIds.Select(playerId => playerMap[playerId]).ToList();
            WicketKeepers = wicketKeeperIds.Select(playerId => playerMap[playerId]).ToList();
            ManOfTheMatchList = manOfTheMatchList.Select(playerId => playerMap[playerId]).ToList();
        }
    }
}