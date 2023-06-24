using System.Linq;
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
        
        [HttpGet]
        [Route("/cric/v1/tours/year/{year:int}")]
        public IActionResult GetAll(int year, int page, int limit)
        {
            var tours = tourService.GetAllForYear(year, page, limit);
            var tourResponses = tours.Select(tour => new TourResponse(tour)).ToList();
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = tourService.GetTotalCountForYear(year);
            }

            return Ok(new Response(new PaginatedResponse<TourResponse>(totalCount, tourResponses, page, limit)));
        }
    }
}
