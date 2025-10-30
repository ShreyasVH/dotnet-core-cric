using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Responses;
using dotnet.Requests;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class StatsController : ControllerBase
    {
        public StatsController()
        {
            
        }

        [HttpPost]
        [Route("/cric/v1/stats")]
        public IActionResult GetStats(FilterRequest filterRequest)
        {
            return Ok(new Response(new StatsResponse()));
        }
    }
}