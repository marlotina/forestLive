using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.Comments.Application.Exceptions;
using FL.Web.API.Core.Comments.Application.Services.Contracts;
using FL.Web.API.Core.Comments.Mapper.v1.Contracts;
using FL.Web.API.Core.Comments.Models.v1.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> logger;
        private readonly IBirdCommentMapper commentMapper;
        private readonly ICommentService commentService;

        public CommentController(IBirdCommentMapper commentMapper,
            ICommentService commentService,
            ILogger<CommentController> logger)
        {
            this.commentService = commentService;
            this.commentMapper = commentMapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("AddComment", Name = "AddComment")]
        public async Task<IActionResult> AddComment([FromBody] BirdCommentRequest request) 
        {
            try
            {
                var comment = this.commentMapper.Convert(request);
                var result = await this.commentService.AddComment(comment, request.SpecieId);

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
        [Route("GetComments", Name = "GetComments")]
        public async Task<IActionResult> GetComments(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByPost(userId);
                    
                if (result != null)
                {
                    var itemResponse = result.Select(x => this.commentMapper.Convert(x));
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
        [Route("DeleteComment", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(Guid commentId, Guid postId, Guid specieId)
        {
            try
            {
                if (commentId == null || commentId == Guid.Empty || postId == null || postId == Guid.Empty) {
                    return this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.commentService.DeleteComment(commentId, userId, specieId);

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
