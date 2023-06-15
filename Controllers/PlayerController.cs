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

        public PlayerController(PlayerService playerService, CountryService countryService)
        {
            this.playerService = playerService;
            this.countryService = countryService;
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
            var playerResponse = new PlayerResponse(player, new CountryResponse(country));
            return Created("", new Response(playerResponse));
        }
    }
}
