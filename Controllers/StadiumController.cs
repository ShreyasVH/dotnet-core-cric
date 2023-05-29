using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Stadiums;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using Com.Dotnet.Cric.Exceptions;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly StadiumService stadiumService;
        private readonly CountryService countryService;

        public StadiumController(StadiumService stadiumService, CountryService countryService)
        {
            this.stadiumService = stadiumService;
            this.countryService = countryService;
        }

        [HttpPost]
        [Route("/cric/v1/stadiums")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var country = countryService.FindById(createRequest.CountryId);
            if(null == country)
            {
                throw new NotFoundException("Country");
            }

            var stadium = stadiumService.Create(createRequest);
            var stadiumResponse = new StadiumResponse(stadium, new CountryResponse(country));
            return Created("", new Response(stadiumResponse));
        }
    }
}
