using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FollowerUserController : ControllerBase
    {
        private readonly IFollowMapper voteMapper;
        private readonly ILogger<FollowerUserController> logger;
        private readonly IFollowerService iFollowerService;

        public FollowerUserController(
            IFollowMapper voteMapper,
            IFollowerService iFollowerService,
            ILogger<FollowerUserController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.iFollowerService = iFollowerService ?? throw new ArgumentNullException(nameof(iFollowerService));
        }

        [HttpGet]
        [Route("GetFollowerByUserId", Name = "GetFollowerByUserId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFollowerByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return null;

                var result = await this.iFollowerService.GetFollowerByUserId(userId);

                if (result != null)
                {
                    var response = result.Select(x => this.voteMapper.ConvertList(x));
                    return this.Ok(response);
                }
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

    }
}
