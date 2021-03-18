using System;
using System.Linq;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchMapController : ControllerBase
    {
        private readonly ILogger<SearchMapController> logger;
        private readonly ISearchMapService searchMapService;
        private readonly IBirdPostMapper birdPostMapper;

        public SearchMapController(ISearchMapService searchMapService,
            IBirdPostMapper birdPostMapper,
            ILogger<SearchMapController> logger)
        {
            this.logger = logger;
            this.searchMapService = searchMapService ?? throw new ArgumentNullException(nameof(searchMapService));
            this.birdPostMapper = birdPostMapper ?? throw new ArgumentNullException(nameof(birdPostMapper));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPoints", Name = "GetPoints")]
        public async Task<IActionResult> GetPoints(double latitude, double longitude, int zoom)
        {
            try
            {
                var result = await this.searchMapService.GetPostByRadio(latitude, longitude, zoom);

                if (result != null)
                {
                    var itemResponse = result.Select(x => this.birdPostMapper.MapConvert(x));
                    return this.Ok(itemResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}