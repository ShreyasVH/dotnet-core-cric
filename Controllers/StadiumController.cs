using System.Linq;
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

        [HttpGet]
        [Route("/cric/v1/stadiums")]
        public IActionResult GetAll(int page, int limit)
        {
            var stadiums = stadiumService.GetAll(page, limit);
            var countryIds = stadiums.Select(s => s.CountryId).ToList();
            var countries = countryService.FindByIds(countryIds);
            var countryMap = countries.ToDictionary(c => c.Id, c => c);

            var stadiumResponses = stadiums.Select(stadium=> new StadiumResponse(stadium, new CountryResponse(countryMap[stadium.CountryId]))).ToList();
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = stadiumService.GetTotalCount();
            }

            return Ok(new Response(new PaginatedResponse<StadiumResponse>(totalCount, stadiumResponses, page, limit)));
        }
    }
}
