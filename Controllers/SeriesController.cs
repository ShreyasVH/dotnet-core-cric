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
    }
}
