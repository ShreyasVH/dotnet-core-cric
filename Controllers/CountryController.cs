using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Countries;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryService countryService;

        public CountryController(CountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpPost]
        [Route("/cric/v1/countries")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var country = countryService.Create(createRequest);
            var countryResponse = new CountryResponse(country);
            return Created("", new Response(countryResponse));
        }

        [HttpGet]
        [Route("/cric/v1/countries/name/{name}")]
        public IActionResult SearchByName(string name)
        {
            var countries = countryService.SearchByName(name);
            var countryResponses = countries.Select(country => new CountryResponse(country)).ToList();
            return Ok(new Response(countryResponses));
        }
    }
}
