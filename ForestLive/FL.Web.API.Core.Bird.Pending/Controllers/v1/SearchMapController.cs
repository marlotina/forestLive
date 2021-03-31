using System;
using System.Linq;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.Web.API.Core.Bird.Pending.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchMapController : ControllerBase
    {
        private readonly ILogger<SearchMapController> iLogger;
        private readonly ISearchMapService iSearchMapService;
        private readonly IBirdSpeciePostMapper iBirdSpeciePostMapper;

        public SearchMapController(ISearchMapService iSearchMapService,
            IBirdSpeciePostMapper iBirdSpeciePostMapper,
            ILogger<SearchMapController> iLogger)
        {
            this.iLogger = iLogger;
            this.iSearchMapService = iSearchMapService ?? throw new ArgumentNullException(nameof(iSearchMapService));
            this.iBirdSpeciePostMapper = iBirdSpeciePostMapper ?? throw new ArgumentNullException(nameof(iBirdSpeciePostMapper));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPoints", Name = "GetPoints")]
        public async Task<IActionResult> GetPoints(double latitude, double longitude, int zoom, Guid? specieId)
        {
            try
            {
                var result = await this.iSearchMapService.GetPostsByRadio(latitude, longitude, zoom, specieId);

                if (result != null)
                {
                    var itemResponse = result.Select(x => this.iBirdSpeciePostMapper.MapConvert(x));
                    return this.Ok(itemResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetModalInfo", Name = "GetModalInfo")]
        public async Task<IActionResult> GetModalInfo(Guid postId, Guid specieId)
        {
            try
            {
                var result = await this.iSearchMapService.GetPostModalInfo(postId, specieId);

                if (result != null)
                {
                    var itemResponse = this.iBirdSpeciePostMapper.ModalConvert(result);
                    return this.Ok(itemResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}