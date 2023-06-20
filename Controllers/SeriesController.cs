using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Series;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly SeriesService seriesService;
        private readonly CountryService countryService;
        private readonly SeriesTypeService seriesTypeService;
        private readonly GameTypeService gameTypeService;
        private readonly TourService tourService;
        private readonly TeamService teamService;
        private readonly TeamTypeService teamTypeService;
        private readonly SeriesTeamsMapService seriesTeamsMapService;

        public SeriesController(SeriesService seriesService, CountryService countryService, SeriesTypeService seriesTypeService, GameTypeService gameTypeService, TourService tourService, TeamService teamService, TeamTypeService teamTypeService, SeriesTeamsMapService seriesTeamsMapService)
        {
            this.seriesService = seriesService;
            this.countryService = countryService;
            this.seriesTypeService = seriesTypeService;
            this.gameTypeService = gameTypeService;
            this.tourService = tourService;
            this.teamService = teamService;
            this.teamTypeService = teamTypeService;
            this.seriesTeamsMapService = seriesTeamsMapService;
        }

        [HttpPost]
        [Route("/cric/v1/series")]
        public IActionResult Create(CreateRequest createRequest)
        {
            List<Team> teams = teamService.GetByIds(createRequest.Teams);
            if (teams.Count != createRequest.Teams.Distinct().Count())
            {
                throw new NotFoundException("Team");
            }

            List<int> teamTypeIds = new List<int>();
            List<long> countryIds = new List<long>();

            foreach (Team team in teams)
            {
                teamTypeIds.Add(team.TypeId);
                countryIds.Add(team.CountryId);
            }
            
            countryIds.Add(createRequest.HomeCountryId);
            List<Country> countries = countryService.FindByIds(countryIds);
            Dictionary<long, Country> countryMap = countries.ToDictionary(country => country.Id, country => country);
            
            var country = countryMap[createRequest.HomeCountryId];
            if (null == country)
            {
                throw new NotFoundException("Country");
            }

            var tour = tourService.GetById(createRequest.TourId);
            if (null == tour)
            {
                throw new NotFoundException("Tour");
            }

            var seriesType = seriesTypeService.FindById(createRequest.TypeId);
            if (null == seriesType)
            {
                throw new NotFoundException("Series type");
            }
            
            var gameType = gameTypeService.FindById(createRequest.GameTypeId);
            if (null == gameType)
            {
                throw new NotFoundException("Game type");
            }

            Series series;
            using (var scope = new TransactionScope())
            {
                series = seriesService.Create(createRequest);
                seriesTeamsMapService.Add(series.Id, createRequest.Teams);
                scope.Complete();
            }

            List<TeamType> teamTypes = teamTypeService.FindByIds(teamTypeIds);
            Dictionary<int, TeamType> teamTypeMap = teamTypes.ToDictionary(teamType => teamType.Id, teamType => teamType);
            List<TeamResponse> teamResponses = teams.Select(team => new TeamResponse(team, new CountryResponse(countryMap[team.CountryId]), new TeamTypeResponse(teamTypeMap[team.TypeId]))).ToList();
            var seriesResponse = new SeriesResponse(series, new CountryResponse(country), new TourResponse(tour), new SeriesTypeResponse(seriesType), new GameTypeResponse(gameType), teamResponses);
            return Created("", new Response(seriesResponse));
        }
        
        [HttpGet]
        [Route("/cric/v1/series")]
        public IActionResult GetAll(int page, int limit)
        {
            var seriesList = seriesService.GetAll(page, limit);
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = seriesService.GetTotalCount();
            }

            var countryIds = new List<long>();
            var seriesTypeIds = new List<int>();
            var gameTypeIds = new List<int>();
            var tourIds = new List<long>();
            var seriesIds = new List<long>();

            foreach (var series in seriesList)
            {
                countryIds.Add(series.HomeCountryId);
                seriesTypeIds.Add(series.TypeId);
                gameTypeIds.Add(series.GameTypeId);
                tourIds.Add(series.TourId);
                seriesIds.Add(series.Id);
            }

            var seriesTypes = seriesTypeService.FindByIds(seriesTypeIds);
            var seriesTypeMap = seriesTypes.ToDictionary(seriesType => seriesType.Id, seriesType => seriesType);

            var gameTypes = gameTypeService.FindByIds(gameTypeIds);
            var gameTypeMap = gameTypes.ToDictionary(gameType => gameType.Id, gameType => gameType);

            var seriesTeamsMaps = seriesTeamsMapService.GetBySeriesIds(seriesIds);
            var teamIds = seriesTeamsMaps.Select(seriesTeamsMap => seriesTeamsMap.TeamId).ToList();

            var teams = teamService.GetByIds(teamIds);

            var teamTypeIds = new List<int>();
            foreach (var team in teams)
            {
                teamTypeIds.Add(team.TypeId);
                countryIds.Add(team.CountryId);
            }

            var countries = countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);
            
            var teamTypes = teamTypeService.FindByIds(teamTypeIds);
            var teamTypeMap = teamTypes.ToDictionary(tt => tt.Id, tt => tt);
            
            var tours = tourService.GetByIds(tourIds);
            var tourMap = tours.ToDictionary(tour => tour.Id, tour => tour);

            var teamResponses = teams.Select(team => new TeamResponse(team, new CountryResponse(countryMap[team.CountryId]), new TeamTypeResponse(teamTypeMap[team.TypeId]))).ToList();

            var seriesResponses = seriesList.Select(series => new SeriesResponse(series, new CountryResponse(countryMap[series.HomeCountryId]), new TourResponse(tourMap[series.TourId]), new SeriesTypeResponse(seriesTypeMap[series.TypeId]), new GameTypeResponse(gameTypeMap[series.GameTypeId]), teamResponses)).ToList();

            return Ok(new Response(new PaginatedResponse<SeriesResponse>(totalCount, seriesResponses, page, limit)));
        }
    }
}
