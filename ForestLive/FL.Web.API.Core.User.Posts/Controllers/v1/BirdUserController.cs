using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdUserController : Controller
    {
        private readonly ILogger<BirdUserController> logger;
        private readonly IUserPostService userPostService;
        private readonly IBirdPostMapper birdPostMapper;

        public BirdUserController(
            ILogger<BirdUserController> logger,
            IUserPostService userPostService,
            IBirdPostMapper birdPostMapper)
        {
            this.userPostService = userPostService;
            this.birdPostMapper = birdPostMapper;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPosts", Name = "GetPosts")]
        public async Task<IActionResult> GetPosts(string userId)
        {
            try
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                
                var result = await this.userPostService.GetPostsByUserId(userId);
                

                if (result != null && result.Any())
                {
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.userPostService.GetVoteByUserId(postList, webUserId);
                    var response = result.Select(x => this.birdPostMapper.Convert(x, postVotes));

                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
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
                var result = await this.userPostService.GetPostsByUserId(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.birdPostMapper.MapConvert(x));
                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
