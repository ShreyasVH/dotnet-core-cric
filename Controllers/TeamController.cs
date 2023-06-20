using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Teams;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService teamService;
        private readonly CountryService countryService;
        private readonly TeamTypeService teamTypeService;

        public TeamController(TeamService teamService, CountryService countryService, TeamTypeService teamTypeService)
        {
            this.teamService = teamService;
            this.countryService = countryService;
            this.teamTypeService = teamTypeService;
        }

        [HttpPost]
        [Route("/cric/v1/teams")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var country = countryService.FindById(createRequest.CountryId);
            if (null == country)
            {
                throw new NotFoundException("Country");
            }

            var teamType = teamTypeService.FindById(createRequest.TypeId);
            if (null == teamType)
            {
                throw new NotFoundException("Team type");
            }

            var team = teamService.Create(createRequest);
            var teamResponse = new TeamResponse(team, new CountryResponse(country), new TeamTypeResponse(teamType));
            return Created("", new Response(teamResponse));
        }

        [HttpGet]
        [Route("/cric/v1/teams")]
        public IActionResult GetAll(int page, int limit)
        {
            var teams = teamService.GetAll(page, limit);
            var countryIds = teams.Select(t => t.CountryId).ToList();
            var countries = countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);
            var teamTypeIds = teams.Select(t => t.TypeId).ToList();
            var teamTypes = teamTypeService.FindByIds(teamTypeIds);
            var teamTypeMap = teamTypes.ToDictionary(tt => tt.Id, tt => tt);

            var teamResponses = teams.Select(team => new TeamResponse(team, new CountryResponse(countryMap[team.CountryId]), new TeamTypeResponse(teamTypeMap[team.TypeId]))).ToList();
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = teamService.GetTotalCount();
            }

            return Ok(new Response(new PaginatedResponse<TeamResponse>(totalCount, teamResponses, page, limit)));
        }
    }
}
