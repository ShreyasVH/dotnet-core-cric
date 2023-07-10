using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Exceptions;
using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Matches;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using Microsoft.IdentityModel.Tokens;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class MatchController: ControllerBase
    {
        private readonly MatchService _matchService;
        private readonly SeriesService _seriesService;
        private readonly TeamService _teamService;
        private readonly ResultTypeService _resultTypeService;
        private readonly WinMarginTypeService _winMarginTypeService;
        private readonly StadiumService _stadiumService;
        private readonly TeamTypeService _teamTypeService;
        private readonly CountryService _countryService;
        private readonly MatchPlayerMapService _matchPlayerMapService;
        private readonly PlayerService _playerService;
        private readonly BattingScoreService _battingScoreService;
        private readonly DismissalModeService _dismissalModeService;
        private readonly BowlingFigureService _bowlingFigureService;
        private readonly FielderDismissalService _fielderDismissalService;
        private readonly ExtrasTypeService _extrasTypeService;
        private readonly ExtrasService _extrasService;
        private readonly CaptainService _captainService;
        private readonly WicketKeeperService _wicketKeeperService;
        private readonly ManOfTheMatchService _manOfTheMatchService;

        public MatchController(MatchService matchService, SeriesService seriesService, TeamService teamService, ResultTypeService resultTypeService, WinMarginTypeService winMarginTypeService, StadiumService stadiumService, TeamTypeService teamTypeService, CountryService countryService, MatchPlayerMapService matchPlayerMapService, PlayerService playerService, BattingScoreService battingScoreService, DismissalModeService dismissalModeService, BowlingFigureService bowlingFigureService, FielderDismissalService fielderDismissalService, ExtrasTypeService extrasTypeService, ExtrasService extrasService, CaptainService captainService, WicketKeeperService wicketKeeperService, ManOfTheMatchService manOfTheMatchService)
        {
            _matchService = matchService;
            _seriesService = seriesService;
            _teamService = teamService;
            _resultTypeService = resultTypeService;
            _winMarginTypeService = winMarginTypeService;
            _stadiumService = stadiumService;
            _teamTypeService = teamTypeService;
            _countryService = countryService;
            _matchPlayerMapService = matchPlayerMapService;
            _playerService = playerService;
            _battingScoreService = battingScoreService;
            _dismissalModeService = dismissalModeService;
            _bowlingFigureService = bowlingFigureService;
            _fielderDismissalService = fielderDismissalService;
            _extrasTypeService = extrasTypeService;
            _extrasService = extrasService;
            _captainService = captainService;
            _wicketKeeperService = wicketKeeperService;
            _manOfTheMatchService = manOfTheMatchService;
        }

        [HttpPost]
        [Route("/cric/v1/matches")]
        public IActionResult Create(CreateRequest createRequest)
        {
            Series series = _seriesService.GetById(createRequest.SeriesId);
            if (null == series)
            {
                throw new NotFoundException("Series");
            }

            List<long> countryIds = new List<long>();

            List<long> teamIds = new List<long>{ createRequest.Team1Id, createRequest.Team2Id };
            List<Team> teams = _teamService.GetByIds(teamIds);
            Dictionary<long, Team> teamMap = new Dictionary<long, Team>();
            foreach (var team in teams)
            {
                teamMap.Add(team.Id, team);
                countryIds.Add(team.CountryId);
            }

            var team1 = teamMap.GetValueOrDefault(createRequest.Team1Id, null);
            if (null == team1)
            {
                throw new NotFoundException("Team 1");
            }
            
            var team2 = teamMap.GetValueOrDefault(createRequest.Team2Id, null);
            if (null == team2)
            {
                throw new NotFoundException("Team 2");
            }

            var resultType = _resultTypeService.FindById(createRequest.ResultTypeId);
            if (null == resultType)
            {
                throw new NotFoundException("Result type");
            }

            WinMarginTypeResponse winMarginTypeResponse = null;
            if (createRequest.WinMarginTypeId.HasValue)
            {
                WinMarginType winMarginType = _winMarginTypeService.FindById(createRequest.WinMarginTypeId.Value);
                if (null == winMarginType)
                {
                    throw new NotFoundException("Win margin type");
                }

                winMarginTypeResponse = new WinMarginTypeResponse(winMarginType);
            }

            var stadium = _stadiumService.GetById(createRequest.StadiumId);
            if (null == stadium)
            {
                throw new NotFoundException("Stadium");
            }

            Dictionary<long, long> playerTeamMap = new Dictionary<long, long>();
            List<long> allPlayerIds = new List<long>();
            foreach (var playerRequest in createRequest.Players)
            {
                playerTeamMap.Add(playerRequest.Id, playerRequest.TeamId);
                allPlayerIds.Add(playerRequest.Id);
            }
            
            foreach (var playerRequest in createRequest.Bench)
            {
                playerTeamMap.Add(playerRequest.Id, playerRequest.TeamId);
                allPlayerIds.Add(playerRequest.Id);
            }

            List<Player> allPlayers = _playerService.GetByIds(allPlayerIds);
            Dictionary<long, Player> playerMap = new Dictionary<long, Player>();
            foreach (var player in allPlayers)
            {
                countryIds.Add(player.CountryId);
                playerMap.Add(player.Id, player);
            }
            
            countryIds.Add(stadium.CountryId);
            var teamTypeIds = teams.Select(t => t.TypeId).ToList();
            var teamTypes = _teamTypeService.FindByIds(teamTypeIds);
            var teamTypeMap = teamTypes.ToDictionary(tt => tt.Id, tt => tt);

            var countries = _countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);

            Match match = null;
            var battingScoreResponses = new List<BattingScoreResponse>();
            var bowlingFigureResponses = new List<BowlingFigureResponse>();
            var extrasResponses = new List<ExtrasResponse>();

            using (var scope = new TransactionScope())
            {
                match = _matchService.Create(createRequest);
                var matchPlayerMapList = _matchPlayerMapService.Add(match.Id, allPlayerIds, playerTeamMap);
                var playerToMatchPlayerMap = matchPlayerMapList.ToDictionary(mpm => mpm.PlayerId, mpm => mpm.Id);
                var battingScores = _battingScoreService.Add(createRequest.BattingScores, playerToMatchPlayerMap);
                var dismissalModes = _dismissalModeService.GetAll();
                var dismissalModeMap = dismissalModes.ToDictionary(dm => dm.Id, dm => dm);
                var battingScoreMap = battingScores.ToDictionary(bs => bs.MatchPlayerId + "_" + bs.Innings,bs => bs);
                var scoreFielderMap = new Dictionary<int, List<long>>();

                foreach (var battingScoreRequest in createRequest.BattingScores)
                {
                    var key = playerToMatchPlayerMap[battingScoreRequest.PlayerId] + "_" + battingScoreRequest.Innings;
                    var battingScoreFromDb = battingScoreMap[key];

                    DismissalModeResponse dismissalModeResponse = null;
                    var fielders = new List<PlayerResponse>();
                    PlayerResponse bowler = null;

                    if (battingScoreRequest.DismissalModeId.HasValue)
                    {
                        dismissalModeResponse =
                            new DismissalModeResponse(dismissalModeMap[battingScoreRequest.DismissalModeId.Value]);
                        if (battingScoreRequest.BowlerId.HasValue)
                        {
                            var bowlerPlayer = playerMap[battingScoreRequest.BowlerId.Value];
                            bowler = new PlayerResponse(bowlerPlayer, new CountryResponse(countryMap[bowlerPlayer.CountryId]));
                        }

                        if (!battingScoreRequest.FielderIds.IsNullOrEmpty())
                        {
                            fielders = battingScoreRequest.FielderIds.Select(playerId =>
                            {
                                var fielderPlayer = playerMap[playerId];
                                return new PlayerResponse(fielderPlayer, new CountryResponse(countryMap[fielderPlayer.CountryId]));
                            }).ToList();
                            scoreFielderMap.Add(battingScoreFromDb.Id, battingScoreRequest.FielderIds);
                        }
                    }

                    var batsmanPlayer = playerMap[battingScoreRequest.PlayerId];
                    battingScoreResponses.Add(new BattingScoreResponse(
                        battingScoreFromDb,
                        new PlayerResponse(batsmanPlayer, new CountryResponse(countryMap[batsmanPlayer.CountryId])),
                        dismissalModeResponse,
                        bowler,
                        fielders
                    ));
                }

                _fielderDismissalService.Add(scoreFielderMap, playerToMatchPlayerMap);
                
                var bowlingFigures =
                    _bowlingFigureService.Add(createRequest.BowlingFigures, playerToMatchPlayerMap);
                var bowlingFigureMap = bowlingFigures.ToDictionary(bf => bf.MatchPlayerId + "_" + bf.Innings, bf => bf);
                foreach (var bowlingFigureRequest in createRequest.BowlingFigures)
                {
                    var bowlerKey = playerToMatchPlayerMap[bowlingFigureRequest.PlayerId] + "_" +
                                    bowlingFigureRequest.Innings;
                    var bowlingFigure = bowlingFigureMap[bowlerKey];

                    var bowlerPlayer = playerMap[bowlingFigureRequest.PlayerId];

                    bowlingFigureResponses.Add(new BowlingFigureResponse(bowlingFigure, new PlayerResponse(bowlerPlayer, new CountryResponse(countryMap[bowlerPlayer.CountryId]))));
                }

                var extrasTypes = _extrasTypeService.GetAll();
                var extrasTypeMap = extrasTypes.ToDictionary(et => et.Id, et => et);
                var extrasList = _extrasService.Add(match.Id, createRequest.Extras);
                extrasResponses = extrasList.Select(extras =>
                {
                    var battingTeam = teamMap[extras.BattingTeamId];
                    var bowlingTeam = teamMap[extras.BowlingTeamId];

                    return new ExtrasResponse(
                        extras,
                        new ExtrasTypeResponse(extrasTypeMap[extras.TypeId]),
                        new TeamResponse(battingTeam, new CountryResponse(countryMap[battingTeam.CountryId]), new TeamTypeResponse(teamTypeMap[battingTeam.TypeId])),
                        new TeamResponse(bowlingTeam, new CountryResponse(countryMap[bowlingTeam.CountryId]), new TeamTypeResponse(teamTypeMap[bowlingTeam.TypeId]))
                    );
                }).ToList();

                _captainService.Add(createRequest.Captains, playerToMatchPlayerMap);
                _wicketKeeperService.Add(createRequest.WicketKeepers, playerToMatchPlayerMap);
                _manOfTheMatchService.Add(createRequest.ManOfTheMatchList, playerToMatchPlayerMap);
                
                scope.Complete();
            }

            var playerResponses = allPlayers.Select(player => new PlayerResponse(player, new CountryResponse(countryMap[player.CountryId]))).ToList();

            var matchResponse = new MatchResponse(
                match,
                series,
                new TeamResponse(team1, new CountryResponse(countryMap[team1.CountryId]), new TeamTypeResponse(teamTypeMap[team1.TypeId])),
                new TeamResponse(team2, new CountryResponse(countryMap[team2.CountryId]), new TeamTypeResponse(teamTypeMap[team2.TypeId])),
                new ResultTypeResponse(resultType),
                winMarginTypeResponse,
                new StadiumResponse(stadium, new CountryResponse(countryMap[stadium.CountryId])),
                playerResponses,
                battingScoreResponses,
                bowlingFigureResponses,
                extrasResponses,
                createRequest.ManOfTheMatchList,
                createRequest.Captains,
                createRequest.WicketKeepers
            );

            return Created("", new Response(matchResponse));
        }
    }
}