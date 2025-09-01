using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Com.Dotnet.Cric.Data;
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
        private readonly GameTypeService _gameTypeService;
        private readonly TotalsService _totalsService;
        private readonly AppDbContext _dbContext;

        public MatchController(MatchService matchService, SeriesService seriesService, TeamService teamService, ResultTypeService resultTypeService, WinMarginTypeService winMarginTypeService, StadiumService stadiumService, TeamTypeService teamTypeService, CountryService countryService, MatchPlayerMapService matchPlayerMapService, PlayerService playerService, BattingScoreService battingScoreService, DismissalModeService dismissalModeService, BowlingFigureService bowlingFigureService, FielderDismissalService fielderDismissalService, ExtrasTypeService extrasTypeService, ExtrasService extrasService, CaptainService captainService, WicketKeeperService wicketKeeperService, ManOfTheMatchService manOfTheMatchService, GameTypeService gameTypeService, TotalsService totalsService, AppDbContext dbContext)
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
            _gameTypeService = gameTypeService;
            _totalsService = totalsService;
            _dbContext = dbContext;
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

            var gameType = _gameTypeService.FindById(series.GameTypeId);

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

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                match = _matchService.Create(createRequest);
                _dbContext.SaveChanges();
                var matchPlayerMapList = _matchPlayerMapService.Add(match.Id, allPlayerIds, playerTeamMap);
                _dbContext.SaveChanges();
                var playerToMatchPlayerMap = matchPlayerMapList.ToDictionary(mpm => mpm.PlayerId, mpm => mpm.Id);
                var battingScores = _battingScoreService.Add(createRequest.BattingScores, playerToMatchPlayerMap);
                _dbContext.SaveChanges();
                var dismissalModes = _dismissalModeService.GetAll();
                var dismissalModeMap = dismissalModes.ToDictionary(dm => dm.Id, dm => dm);
                var battingScoreMap = battingScores.ToDictionary(bs => bs.MatchPlayerId + "_" + bs.Innings,bs => bs);
                var scoreFielderMap = new Dictionary<int, List<long>>();

                foreach (var battingScoreRequest in createRequest.BattingScores)
                {
                    var key = playerToMatchPlayerMap[battingScoreRequest.PlayerId] + "_" + battingScoreRequest.Innings;
                    var battingScoreFromDb = battingScoreMap[key];

                    DismissalModeResponse dismissalModeResponse = null;
                    var fielders = new List<PlayerMiniResponse>();
                    PlayerMiniResponse bowler = null;

                    if (battingScoreRequest.DismissalModeId.HasValue)
                    {
                        dismissalModeResponse =
                            new DismissalModeResponse(dismissalModeMap[battingScoreRequest.DismissalModeId.Value]);
                        if (battingScoreRequest.BowlerId.HasValue)
                        {
                            var bowlerPlayer = playerMap[battingScoreRequest.BowlerId.Value];
                            bowler = new PlayerMiniResponse(bowlerPlayer, new CountryResponse(countryMap[bowlerPlayer.CountryId]));
                        }

                        if (!battingScoreRequest.FielderIds.IsNullOrEmpty())
                        {
                            fielders = battingScoreRequest.FielderIds.Select(playerId =>
                            {
                                var fielderPlayer = playerMap[playerId];
                                return new PlayerMiniResponse(fielderPlayer, new CountryResponse(countryMap[fielderPlayer.CountryId]));
                            }).ToList();
                            scoreFielderMap.Add(battingScoreFromDb.Id, battingScoreRequest.FielderIds);
                        }
                    }

                    var batsmanPlayer = playerMap[battingScoreRequest.PlayerId];
                    battingScoreResponses.Add(new BattingScoreResponse(
                        battingScoreFromDb,
                        new PlayerMiniResponse(batsmanPlayer, new CountryResponse(countryMap[batsmanPlayer.CountryId])),
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

                    bowlingFigureResponses.Add(new BowlingFigureResponse(bowlingFigure, new PlayerMiniResponse(bowlerPlayer, new CountryResponse(countryMap[bowlerPlayer.CountryId]))));
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
                _totalsService.Add(createRequest.Totals.Select(t => new Total(match.Id, t)).ToList());

                _dbContext.SaveChanges();
                
                transaction.Commit();
            }

            var teamPlayerMap = new Dictionary<long, List<PlayerMiniResponse>>();
            foreach (var player in allPlayers)
            {
                var playerMiniResponse =
                    new PlayerMiniResponse(player, new CountryResponse(countryMap[player.CountryId]));
                var teamId = playerTeamMap[player.Id];
                if (!teamPlayerMap.ContainsKey(teamId))
                {
                    teamPlayerMap.Add(teamId, new List<PlayerMiniResponse>());
                }
                teamPlayerMap[teamId].Add(playerMiniResponse);
            }

            var matchResponse = new MatchResponse(
                match,
                series,
                gameType,
                new TeamResponse(team1, new CountryResponse(countryMap[team1.CountryId]), new TeamTypeResponse(teamTypeMap[team1.TypeId])),
                new TeamResponse(team2, new CountryResponse(countryMap[team2.CountryId]), new TeamTypeResponse(teamTypeMap[team2.TypeId])),
                new ResultTypeResponse(resultType),
                winMarginTypeResponse,
                new StadiumResponse(stadium, new CountryResponse(countryMap[stadium.CountryId])),
                teamPlayerMap,
                battingScoreResponses,
                bowlingFigureResponses,
                extrasResponses,
                createRequest.ManOfTheMatchList,
                createRequest.Captains,
                createRequest.WicketKeepers
            );

            return Created("", new Response(matchResponse));
        }

        [HttpGet]
        [Route("/cric/v1/matches/{id:int}")]
        public IActionResult GetById(int id)
        {
            Match match = _matchService.GetById(id);
            if (null == match)
            {
                throw new NotFoundException("Match");
            }

            Series series = _seriesService.GetById(match.SeriesId);
            if (null == series)
            {
                throw new NotFoundException("Series");
            }

            GameType gameType = _gameTypeService.FindById(series.GameTypeId);

            var countryIds = new List<long>();

            var teamIds = new List<long>
            {
                match.Team1Id,
                match.Team2Id
            };
            var teams = _teamService.GetByIds(teamIds);
            var teamMap = new Dictionary<long, Team>();
            foreach (var team in teams)
            {
                teamMap.Add(team.Id, team);
                countryIds.Add(team.CountryId);
            }

            var team1 = teamMap.GetValueOrDefault(match.Team1Id, null);
            if (null == team1)
            {
                throw new NotFoundException("Team 1");
            }
            
            var team2 = teamMap.GetValueOrDefault(match.Team2Id, null);
            if (null == team2)
            {
                throw new NotFoundException("Team 2");
            }

            var resultType = _resultTypeService.FindById(match.ResultTypeId);
            if (null == resultType)
            {
                throw new NotFoundException("Result type");
            }

            WinMarginTypeResponse winMarginTypeResponse = null;
            if (match.WinMarginTypeId.HasValue)
            {
                var winMarginType = _winMarginTypeService.FindById(match.WinMarginTypeId.Value);
                if (null == winMarginType)
                {
                    throw new NotFoundException("Win margin type");
                }

                winMarginTypeResponse = new WinMarginTypeResponse(winMarginType);
            }

            var stadium = _stadiumService.GetById(match.StadiumId);
            if (null == stadium)
            {
                throw new NotFoundException("Stadium");
            }

            var matchPlayerMaps = _matchPlayerMapService.GetByMatchId(id);
            var playerIds = new List<long>();
            var matchPlayerToPlayerMap = new Dictionary<int, long>();
            var matchPlayerIds = new List<int>();
            var playerToTeamMap = new Dictionary<long, long>();
            foreach(var matchPlayerMap in matchPlayerMaps)
            {
                playerIds.Add(matchPlayerMap.PlayerId);
                matchPlayerToPlayerMap.Add(matchPlayerMap.Id, matchPlayerMap.PlayerId);
                matchPlayerIds.Add(matchPlayerMap.Id);
                playerToTeamMap.Add(matchPlayerMap.PlayerId, matchPlayerMap.TeamId);
            }

            var players = _playerService.GetByIds(playerIds);
            var playerCountryIds = players.Select(p => p.CountryId).ToList();
            
            countryIds.Add(stadium.CountryId);
            countryIds.AddRange(playerCountryIds);
            var teamTypeIds = new List<int> { team1.TypeId, team2.TypeId };
            var teamTypes = _teamTypeService.FindByIds(teamTypeIds);
            var teamTypeMap = teamTypes.ToDictionary(tt => tt.Id, tt => tt);

            var countries = _countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);

            var playerMap = new Dictionary<long, PlayerMiniResponse>();
            var teamPlayerMap = new Dictionary<long, List<PlayerMiniResponse>>();
            foreach (var player in players)
            {
                var playerMiniResponse =
                    new PlayerMiniResponse(player, new CountryResponse(countryMap[player.CountryId]));
                playerMap.Add(player.Id, playerMiniResponse);
                var teamId = playerToTeamMap[player.Id];
                if (!teamPlayerMap.ContainsKey(teamId))
                {
                    teamPlayerMap.Add(teamId, new List<PlayerMiniResponse>());
                }
                teamPlayerMap[teamId].Add(playerMiniResponse);
            }

            var manOfTheMatchList = _manOfTheMatchService.GetByMatchPlayerIds(matchPlayerIds);
            var captains = _captainService.GetByMatchPlayerIds(matchPlayerIds);
            var wicketKeepers = _wicketKeeperService.GetByMatchPlayerIds(matchPlayerIds);
            var battingScores = _battingScoreService.GetByMatchPlayerIds(matchPlayerIds);
            var dismissalModes = _dismissalModeService.GetAll();
            var dismissalModeMap = dismissalModes.ToDictionary(dm => dm.Id, dm => dm);
            var fielderDismissals = _fielderDismissalService.GetByMatchPlayerIds(matchPlayerIds);
            var fielderDismissalMap = fielderDismissals
                .GroupBy(fd => fd.ScoreId)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToList()
                );

            var battingScoreResponses = new List<BattingScoreResponse>();
            foreach (var battingScore in battingScores)
            {
                var batsmanPlayer = playerMap[matchPlayerToPlayerMap[battingScore.MatchPlayerId]];


                DismissalModeResponse dismissalModeResponse = null;
                if (battingScore.DismissalModeId.HasValue)
                {
                    dismissalModeResponse =
                        new DismissalModeResponse(dismissalModeMap[battingScore.DismissalModeId.Value]);
                }

                PlayerMiniResponse bowlerPlayer = null;
                if (battingScore.BowlerId.HasValue)
                {
                    bowlerPlayer = playerMap[matchPlayerToPlayerMap[battingScore.BowlerId.Value]];
                }

                var fielders = new List<PlayerMiniResponse>();
                if (fielderDismissalMap.ContainsKey(battingScore.Id))
                {
                    var fielderDismissalList = fielderDismissalMap[battingScore.Id];
                    fielders = fielderDismissalList.Select(fd => playerMap[matchPlayerToPlayerMap[fd.MatchPlayerId]]).ToList();
                }
                
                battingScoreResponses.Add(new BattingScoreResponse(
                    battingScore,
                    batsmanPlayer,
                    dismissalModeResponse,
                    bowlerPlayer,
                    fielders                    
                ));
            }

            var bowlingFigures = _bowlingFigureService.GetByMatchPlayerIds(matchPlayerIds);
            var bowlingFigureResponses = bowlingFigures.Select(bowlingFigure =>
            {
                var bowlerPlayer = playerMap[matchPlayerToPlayerMap[bowlingFigure.MatchPlayerId]];
                return new BowlingFigureResponse(bowlingFigure, bowlerPlayer);
            }).ToList();

            var extrasTypes = _extrasTypeService.GetAll();
            var extrasTypeMap = extrasTypes.ToDictionary(et => et.Id, et => et);
            var extrasList = _extrasService.GetByMatchId(id);
            var extrasResponses = extrasList.Select(extras =>
            {
                var extrasTypeResponse = new ExtrasTypeResponse(extrasTypeMap[extras.TypeId]);
                var battingTeam = teamMap[extras.BattingTeamId];
                var bowlingTeam = teamMap[extras.BowlingTeamId];
                return new ExtrasResponse(
                    extras,
                    extrasTypeResponse,
                    new TeamResponse(battingTeam, new CountryResponse(countryMap[battingTeam.CountryId]), new TeamTypeResponse(teamTypeMap[battingTeam.TypeId])),
                    new TeamResponse(bowlingTeam, new CountryResponse(countryMap[bowlingTeam.CountryId]), new TeamTypeResponse(teamTypeMap[bowlingTeam.TypeId]))
                );
            }).ToList();

            var matchResponse = new MatchResponse(
                match,
                series,
                gameType,
                new TeamResponse(team1, new CountryResponse(countryMap[team1.CountryId]), new TeamTypeResponse(teamTypeMap[team1.TypeId])),
                new TeamResponse(team2, new CountryResponse(countryMap[team2.CountryId]), new TeamTypeResponse(teamTypeMap[team2.TypeId])),
                new ResultTypeResponse(resultType),
                winMarginTypeResponse,
                new StadiumResponse(stadium, new CountryResponse(countryMap[stadium.CountryId])),
                teamPlayerMap,
                battingScoreResponses,
                bowlingFigureResponses,
                extrasResponses,
                manOfTheMatchList.Select(motm => matchPlayerToPlayerMap[motm.MatchPlayerId]).ToList(),
                captains.Select(c => matchPlayerToPlayerMap[c.MatchPlayerId]).ToList(),
                wicketKeepers.Select(wk => matchPlayerToPlayerMap[wk.MatchPlayerId]).ToList()
            );

            return Ok(new Response(matchResponse));
        }

        [HttpDelete]
        [Route("/cric/v1/matches/{id:int}")]
        public IActionResult Remove(int id)
        {
            Match match = _matchService.GetById(id);
            if (null == match)
            {
                throw new NotFoundException("Match");
            }

            using (var scope = new TransactionScope())
            {
                var matchPlayerMaps = _matchPlayerMapService.GetByMatchId(id);
                var matchPlayerIds = matchPlayerMaps.Select(mpm => mpm.Id).ToList();
                _extrasService.Remove(id);
                _captainService.Remove(matchPlayerIds);
                _wicketKeeperService.Remove(matchPlayerIds);
                _manOfTheMatchService.Remove(matchPlayerIds);
                _fielderDismissalService.Remove(matchPlayerIds);
                _battingScoreService.Remove(matchPlayerIds);
                _bowlingFigureService.Remove(matchPlayerIds);
                _matchPlayerMapService.Remove(id);
                _matchService.Remove(id);
                
                scope.Complete();
            }

            return Ok(new Response("Deleted successfully", true));
        }
    }
}