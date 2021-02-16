using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Pendings.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Pendings.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PendingBirdsController : Controller
    {
        private readonly ILogger<PendingBirdsController> logger;
        private readonly IPendingPostMapper pendingPostMapper;
        private readonly IPendingPostService pendingPostService;

        public PendingBirdsController(
            IPendingPostMapper pendingPostMapper,
            IPendingPostService pendingPostService,
            ILogger<PendingBirdsController> logger)
        {
            this.pendingPostService = pendingPostService;
            this.pendingPostMapper = pendingPostMapper;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetToConfirm", Name = "GetToConfirm")]
        public async Task<IActionResult> GetToConfirm()
        {
            try
            {
                var result = await this.pendingPostService.GetPostByStatus(StatusSpecie.Pending);

                if (result != null)
                {
                    var itemResponse = result.Select(x => this.pendingPostMapper.Convert(x));
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
        [Route("GetWithiutSpecie", Name = "GetWithiutSpecie")]
        public async Task<IActionResult> GetWithiutSpecie()
        {
            try
            {
                var result = await this.pendingPostService.GetPostByStatus(StatusSpecie.NoSpecie);

                if (result != null)
                {
                    var itemResponse = result.Select(x => this.pendingPostMapper.Convert(x));
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
