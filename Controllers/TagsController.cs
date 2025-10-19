using System.Linq;
using Com.Dotnet.Cric.Models;
using Microsoft.AspNetCore.Mvc;

using Com.Dotnet.Cric.Responses;
using Com.Dotnet.Cric.Services;

namespace Com.Dotnet.Cric.Controllers
{
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TagsService tagsService;

        public TagsController(TagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        [HttpGet]
        [Route("/cric/v1/tags")]
        public IActionResult GetAll(int page, int limit)
        {
            var tags = tagsService.GetAll(page, limit);
            var totalCount = 0;
            if(page == 1)
            {
                totalCount = tagsService.GetTotalCount();
            }

            return Ok(new Response(new PaginatedResponse<Tag>(totalCount, tags, page, limit)));
        }
    }
}