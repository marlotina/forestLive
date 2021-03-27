using System;
using System.Linq;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdPostController : ControllerBase
    {
        private readonly ILogger<BirdPostController> iLogger;
        private readonly IPostService iPostService;
        private readonly IPostMapper iPostMapper;

        public BirdPostController(IPostService iPostService,
            IPostMapper iPostMapper,
            ILogger<BirdPostController> iLogger)
        {
            this.iLogger = iLogger;
            this.iPostService = iPostService ?? throw new ArgumentNullException(nameof(iPostService));
            this.iPostMapper = iPostMapper ?? throw new ArgumentNullException(nameof(iPostMapper));
        } 

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            try
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var result = await this.iPostService.GetBirdPost(postId);

                if (result != null)
                {
                    var postList = new Guid[] { postId };

                    var postVotes = await this.iPostService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = this.iPostMapper.Convert(result, postVotes);
                    return this.Ok(itemResponse);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPosts", Name = "GetPosts")]
        public async Task<IActionResult> GetPosts(int orderBy)
        {
            try
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.iPostService.GetPosts(orderBy);

                if (result != null)
                {
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.iPostService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = result.Select(x => this.iPostMapper.ConvertToList(x, postVotes));
                    return this.Ok(itemResponse);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}