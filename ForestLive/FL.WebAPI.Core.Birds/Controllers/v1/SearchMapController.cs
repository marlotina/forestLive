using System;
using System.Linq;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchMapController : ControllerBase
    {
        private readonly ILogger<SearchMapController> logger;
        private readonly ISearchMapService searchMapService;
        private readonly IBirdSpeciePostMapper birdSpeciePostMapper;

        public SearchMapController(ISearchMapService searchMapService,
            IBirdSpeciePostMapper birdSpeciePostMapper,
            ILogger<SearchMapController> logger)
        {
            this.logger = logger;
            this.searchMapService = searchMapService ?? throw new ArgumentNullException(nameof(searchMapService));
            this.birdSpeciePostMapper = birdSpeciePostMapper ?? throw new ArgumentNullException(nameof(birdSpeciePostMapper));
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
                    var itemResponse = result.Select(x => this.birdSpeciePostMapper.MapConvert(x));
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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetModalInfo", Name = "GetModalInfo")]
        public async Task<IActionResult> GetModalInfo(string postId, string userId)
        {
            try
            {
                var result = await this.searchMapService.GetPostByPostId(postId, userId);

                if (result != null)
                {
                    var itemResponse = this.birdSpeciePostMapper.ModalConvert(result);
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