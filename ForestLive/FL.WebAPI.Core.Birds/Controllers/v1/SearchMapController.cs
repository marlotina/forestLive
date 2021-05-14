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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SearchMapController : ControllerBase
    {
        private readonly ILogger<SearchMapController> iLogger;
        private readonly ISearchMapService iSearchMapService;
        private readonly IPostMapper iBirdSpeciePostMapper;

        public SearchMapController(ISearchMapService iSearchMapService,
            IPostMapper iBirdSpeciePostMapper,
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
                    var itemResponse = result.Select(x => this.iBirdSpeciePostMapper.Convert(x));
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