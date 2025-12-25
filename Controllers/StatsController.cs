using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;
using dotnet.Requests;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly StatsService _statsService;
        
        public StatsController(StatsService statsService)
        {
            this._statsService = statsService;
        }

        [HttpPost]
        [Route("/cric/v1/stats")]
        public IActionResult GetStats(FilterRequest filterRequest)
        {
            return Ok(new Response(_statsService.GetStats(filterRequest)));
        }
    }
}