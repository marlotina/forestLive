using FL.LogTrace.Contracts.Standard;
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
    public class FollowUserController : ControllerBase
    {
        private readonly IFollowMapper voteMapper;
        private readonly ILogger<FollowUserController> logger;
        private readonly IFollowService iFollowService;

        public FollowUserController(
            IFollowMapper voteMapper,
            IFollowService iFollowService,
            ILogger<FollowUserController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.iFollowService = iFollowService ?? throw new ArgumentNullException(nameof(iFollowService));
        }

        [HttpPost]
        [Route("FollowUser", Name = "FollowUser")]
        public async Task<IActionResult> FollowUser([FromBody] FollowUserRequest request)
        {
            try
            {
                if (request == null)
                    return null;


                var follow = this.voteMapper.Convert(request);

                var result = await this.iFollowService.AddFollow(follow);

                if (result != null)
                {
                    var response = this.voteMapper.Convert(result);
                    return this.Ok(response);
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

        [HttpDelete]
        [Route("DeleteFollowUser", Name = "DeleteFollowUser")]
        public async Task<IActionResult> DeleteFollowUser([FromBody] DeleteFollowUserResquest request)
        {
            try
            {
                if (request == null)
                    return null;

                var result = await this.iFollowService.DeleteFollow(request);

                if (result)
                {
                    return this.NoContent();
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
