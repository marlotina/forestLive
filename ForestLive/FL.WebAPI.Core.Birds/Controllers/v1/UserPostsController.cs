using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPostsController : Controller
    {
        private readonly ILogger<UserPostsController> iLogger;
        private readonly IUserPostService iUserPostService;
        private readonly IUserVoteService iUserVoteService;
        private readonly IBirdSpeciePostMapper iBirdPostMapper;

        public UserPostsController(
            ILogger<UserPostsController> iLogger,
            IUserPostService iUserPostService,
            IUserVoteService iUserVoteService,
            IBirdSpeciePostMapper iBirdPostMapper)
        {
            this.iUserPostService = iUserPostService;
            this.iBirdPostMapper = iBirdPostMapper;
            this.iUserVoteService = iUserVoteService;
            this.iLogger = iLogger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUserPosts", Name = "GetUserPosts")]
        public async Task<IActionResult> GetUserPosts(string userId, string type, string label)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return this.BadRequest();

                var result = await this.iUserPostService.GetUserPosts(userId, label, type);

                if (result != null && result.Any())
                {
                    var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);
                    var response = result.Select(x => this.iBirdPostMapper.Convert(x, postVotes));

                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapPoints", Name = "GetMapPoints")]
        public async Task<IActionResult> GetMapPoints(string userId)
        {
            try
            {
                var result = await this.iUserPostService.GetMapPointsByUserId(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.iBirdPostMapper.MapConvert(x));
                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }        
    }
}
