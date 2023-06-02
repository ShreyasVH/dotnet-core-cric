using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Models;
using Com.Dotnet.Cric.Requests.Tours;
using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly TourService tourService;

        public TourController(TourService tourService)
        {
            this.tourService = tourService;
        }

        [HttpPost]
        [Route("/cric/v1/tours")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var tour = tourService.Create(createRequest);
            var tourResponse = new TourResponse(tour);
            return Created("", new Response(tourResponse));
        }
    }
}
