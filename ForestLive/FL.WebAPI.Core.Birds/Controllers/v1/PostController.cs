using System;
using System.Linq;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> iLogger;
        private readonly IPostService iPostService;
        private readonly IUserVoteService iUserVoteService;
        private readonly IPostMapper iPostMapper;

        public PostController(
            IUserVoteService iUserVoteService,
            IPostService iPostService,
            IPostMapper iPostMapper,
            ILogger<PostController> iLogger)
        {
            this.iUserVoteService = iUserVoteService;
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
                
                if (postId == Guid.Empty)
                {
                    this.BadRequest();
                }

                var result = await this.iPostService.GetPost(postId);

                if (result != null)
                {
                    var postList = new Guid[] { postId };

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = this.iPostMapper.ConvertPost(result, postVotes);
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
        [Route("GetModalInfo", Name = "GetModalInfo")]
        public async Task<IActionResult> GetModalInfo(Guid postId)
        {
            try
            {
                var result = await this.iPostService.GetPost(postId);

                if (result != null)
                {
                    var itemResponse = this.iPostMapper.ModalConvert(result);
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

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = result.Select(x => this.iPostMapper.Convert(x, postVotes));
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