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

        [HttpPost]
        [Route("AddPost", Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] PostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.ImageData))
                    return this.BadRequest();

                var post = this.postMapper.Convert(request);
                var bytes = Convert.FromBase64String(request.ImageData.Split(',')[1]);


                var result = await this.postService.AddBirdPost(post, bytes, request.ImageName, request.isPost);
                
                if (result != null)
                {
                    var postResponse = this.postMapper.Convert(result);
                    return this.CreatedAtRoute("GetPost", new { id = postResponse.Id }, postResponse);
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
        [Route("GetComments", Name = "GetComments")]
        public async Task<IActionResult> GetComments(Guid postId)
        {
            try
            {
                if (postId == null || postId == Guid.Empty)
                {
                    this.BadRequest();
                }

                var result = await this.postService.GetCommentByPost(postId);

                if (result != null)
                {
                    var itemResponse = result.Select(x => this.postMapper.Convert(x));
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

        [HttpDelete]
        [Route("DeletePost", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null) {
                    this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.postService.DeleteBirdPost(postId, userId);

                if (result)
                {
                    return this.Ok();
                }
                else
                    return this.BadRequest();
            }
            catch (UnauthorizedRemove ex)
            {
                this.logger.LogInfo(ex);
                return this.Unauthorized();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
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
        [Route("GetAllPosts", Name = "GetAllPosts")]
        public async Task<IActionResult> GetAllPosts(int orderBy)
        {
            try
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.postService.GetAllPosts(orderBy);

                if (result != null)
                {
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.postService.GetVoteByUserId(postList, webUserId);

                    var itemResponse = result.Select(x => this.postMapper.Convert(x, postVotes));
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

                    var itemResponse = result.Select(x => this.postMapper.Convert(x, postVotes));
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