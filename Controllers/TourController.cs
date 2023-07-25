using System.Linq;
using Com.Dotnet.Cric.Exceptions;
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
        private readonly SeriesService _serviceService;
        private readonly GameTypeService _gameTypeService;

        public TourController(TourService tourService, SeriesService seriesService, GameTypeService gameTypeService)
        {
            this.tourService = tourService;
            _serviceService = seriesService;
            _gameTypeService = gameTypeService;
        }

        [HttpPost]
        [Route("/cric/v1/tours")]
        public IActionResult Create(CreateRequest createRequest)
        {
            var tour = tourService.Create(createRequest);
            var tourResponse = new TourMiniResponse(tour);
            return Created("", new Response(tourResponse));
        }
        
        [HttpGet]
        [Route("/cric/v1/tours/year/{year:int}")]
        public IActionResult GetAll(int year, int page, int limit)
        {
            var tours = tourService.GetAllForYear(year, page, limit);
            var tourResponses = tours.Select(tour => new TourMiniResponse(tour)).ToList();
            var totalCount = 0;
            if (page == 1)
            {
                totalCount = tourService.GetTotalCountForYear(year);
            }

            return Ok(new Response(new PaginatedResponse<TourMiniResponse>(totalCount, tourResponses, page, limit)));
        }

        [HttpGet]
        [Route("/cric/v1/tours/years")]
        public IActionResult GetAllYears()
        {
            var years = tourService.GetAllYears();
            return Ok(new Response(years));
        }
        
        [HttpGet]
        [Route("/cric/v1/tours/{id:long}")]
        public IActionResult GetById(long id)
        {
            var tour = tourService.GetById(id);
            if (null == tour)
            {
                throw new NotFoundException("Tour");
            }
            
            var tourResponse = new TourResponse(tour);
            var seriesList = _serviceService.GetByTourId(id);

            var gameTypeIds = seriesList.Select(s => s.GameTypeId).ToList();
            var gameTypes = _gameTypeService.FindByIds(gameTypeIds);
            var gameTypeMap = gameTypes.ToDictionary(gt => gt.Id, gt => gt);

            var seriesMiniResponses = seriesList.Select(series => new SeriesMiniResponse(series, gameTypeMap[series.GameTypeId])).ToList();
            tourResponse.SeriesList = seriesMiniResponses;

            return Ok(new Response(tourResponse));
        }
    }
}
