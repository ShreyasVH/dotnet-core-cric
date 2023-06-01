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
    }
}
