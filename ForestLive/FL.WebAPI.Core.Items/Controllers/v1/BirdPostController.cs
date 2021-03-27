using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Application.Exceptions;
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
        private readonly ILogger<BirdPostController> logger;
        private readonly IPostService postService;
        private readonly IPostMapper postMapper;

        public BirdPostController(IPostService postService,
            IPostMapper postMapper,
            ILogger<BirdPostController> logger)
        {
            this.logger = logger;
            this.postService = postService ?? throw new ArgumentNullException(nameof(postService));
            this.postMapper = postMapper ?? throw new ArgumentNullException(nameof(postMapper));
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

                var result = await this.postService.GetBirdPost(postId);

                if (result != null)
                {
                    var postList = new Guid[] { postId };

                    var postVotes = await this.postService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = this.postMapper.Convert(result, postVotes);
                    return this.Ok(itemResponse);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
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

                var result = await this.postService.GetPosts(orderBy);

                if (result != null)
                {
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.postService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = result.Select(x => this.postMapper.ConvertToList(x, postVotes));
                    return this.Ok(itemResponse);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}