using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Requests.Players;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService playerService;
        private readonly CountryService countryService;
        private readonly BattingScoreService _battingScoreService;
        private readonly BowlingFigureService _bowlingFigureService;
        private readonly FielderDismissalService _fielderDismissalService;

        public PlayerController(PlayerService playerService, CountryService countryService, BattingScoreService battingScoreService, BowlingFigureService bowlingFigureService, FielderDismissalService fielderDismissalService)
        {
            this.playerService = playerService;
            this.countryService = countryService;
            _battingScoreService = battingScoreService;
            _bowlingFigureService = bowlingFigureService;
            _fielderDismissalService = fielderDismissalService;
        }

        [HttpPost]
        [Route("/cric/v1/players")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var country = countryService.FindById(createRequest.CountryId);
            if (null == country)
            {
                throw new NotFoundException("Country");
            }

            var player = playerService.Create(createRequest);
            var playerResponse = new PlayerMiniResponse(player, new CountryResponse(country));
            return Created("", new Response(playerResponse));
        }
        
        [HttpGet]
        [Route("/cric/v1/players")]
        public IActionResult GetAll(int page, int limit)
        {
            var players = playerService.GetAll(page, limit);
            var countryIds = players.Select(t => t.CountryId).ToList();
            var countries = countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);

            var playerResponses = players.Select(player => new PlayerMiniResponse(player, new CountryResponse(countryMap[player.CountryId]))).ToList();
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = playerService.GetTotalCount();
            }

            return Ok(new Response(new PaginatedResponse<PlayerMiniResponse>(totalCount, playerResponses, page, limit)));
        }

        [HttpGet]
        [Route("/cric/v1/players/{id:long}")]
        public IActionResult GetById(long id)
        {
            var player = playerService.GetById(id);
            if (null == player)
            {
                throw new NotFoundException("Player");
            }

            var playerResponse = new PlayerResponse(player);
            var country = countryService.FindById(player.CountryId);
            playerResponse.Country = new CountryResponse(country);

            var dismissalStats = _battingScoreService.GetDismissalStats(id);
            playerResponse.DismissalStats = dismissalStats;

            var dismissalCountMap = new Dictionary<string, int>();
            foreach (var dismissalStatsEntry in dismissalStats)
            {
                var gameType = dismissalStatsEntry.Key;
                var counts = dismissalStatsEntry.Value;
                var dismissalCount = 0;
                foreach (var (_, count) in counts)
                {
                    dismissalCount += count;
                }

                dismissalCountMap[gameType] = dismissalCount;
            }

            var basicBattingStats = _battingScoreService.GetBattingStats(id);
            if (basicBattingStats.Any())
            {
                var battingStatsMap = new Dictionary<string, BattingStats>();
                foreach (var battingStatsEntry in basicBattingStats)
                {
                    var gameType = battingStatsEntry.Key;
                    var battingStats = new BattingStats(battingStatsEntry.Value);
                    battingStats.NotOuts = battingStats.Innings - dismissalCountMap.GetValueOrDefault(gameType, 0);

                    if (dismissalCountMap.GetValueOrDefault(gameType, 0) > 0)
                    {
                        battingStats.Average = battingStats.Runs * 1.0 / dismissalCountMap[gameType];
                    }

                    if (battingStats.Balls > 0)
                    {
                        battingStats.StrikeRate = battingStats.Runs * 100.0 / battingStats.Balls;
                    }

                    battingStatsMap[gameType] = battingStats;
                }

                playerResponse.BattingStats = battingStatsMap;
            }

            var basicBowlingStatsMap = _bowlingFigureService.GetBowlingStats(id);
            if (basicBowlingStatsMap.Any())
            {
                var bowlingStatsFinal = new Dictionary<string, BowlingStats>();

                foreach (var (gameType, gameTypeBowlingStats) in basicBowlingStatsMap)
                {
                    var bowlingStats = new BowlingStats(gameTypeBowlingStats);

                    if (bowlingStats.Balls > 0)
                    {
                        bowlingStats.Economy = bowlingStats.Runs * 6.0 / bowlingStats.Balls;
                        if (bowlingStats.Wickets > 0)
                        {
                            bowlingStats.Average = bowlingStats.Runs * 1.0 / bowlingStats.Wickets;
                            bowlingStats.StrikeRate = bowlingStats.Balls * 1.0 / bowlingStats.Wickets;
                        }
                    }
                    
                    bowlingStatsFinal.Add(gameType, bowlingStats);
                }
                
                playerResponse.BowlingStats = bowlingStatsFinal;
            }

            var fieldingStatsMap = _fielderDismissalService.GetFieldingStats(id);
            if (fieldingStatsMap.Any())
            {
                var fieldingStatsMapFinal = new Dictionary<string, FieldingStats>();
                
                foreach(var (gameType, gameTypeFieldingStats) in fieldingStatsMap)
                {
                    var fieldingStats = new FieldingStats(gameTypeFieldingStats);
                    fieldingStatsMapFinal[gameType] = fieldingStats;
                }

                playerResponse.FieldingStats = fieldingStatsMapFinal;
            }

            return Ok(new Response(playerResponse));
        }
    }
}
