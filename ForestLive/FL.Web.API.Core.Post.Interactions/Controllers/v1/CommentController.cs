using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.Post.Interactions.Application.Exceptions;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Models.v1.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> logger;
        private readonly ICommentMapper commentMapper;
        private readonly ICommentService commentService;

        public CommentController(ICommentMapper commentMapper,
            ICommentService commentService,
            ILogger<CommentController> logger)
        {
            this.commentService = commentService;
            this.commentMapper = commentMapper;
            this.logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("AddComment", Name = "AddComment")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequest request) 
        {
            try
            {
                var comment = this.commentMapper.Convert(request);
                var result = await this.commentService.AddComment(comment);

                if (result != null)
                {
                    var commentResponse = this.commentMapper.Convert(result);
                    return this.Ok(commentResponse);
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
        [AllowAnonymous]
        [Route("GetCommentsByPost", Name = "GetCommentsByPost")]
        public async Task<IActionResult> GetCommentsByPost(Guid postId)
        {
            try
            {
                if (postId == null && postId != Guid.Empty)
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByPost(postId);

                if (result != null)
                {
                    var response = this.commentMapper.Convert(result);
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
        [Route("DeleteComment", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid postId)
        {
            try
            {
                if (commentId == null || commentId == Guid.Empty || postId == null || postId == Guid.Empty) {
                    return this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.commentService.DeleteComment(commentId, postId, userId);

                if (result)
                    return this.Ok();
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
    }
}
