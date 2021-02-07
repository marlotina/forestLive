using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdCommentController : Controller
    {
        private readonly ILogger<BirdCommentController> logger;
        private readonly IBirdCommentMapper commentMapper;
        private readonly IBirdCommentService commentService;

        public BirdCommentController(IBirdCommentMapper commentMapper,
            IBirdCommentService commentService,
            ILogger<BirdCommentController> logger)
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
        [Route("GetComments", Name = "GetComments")]
        public async Task<IActionResult> GetComments(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByItem(postId);
                var itemResponse = result.Select(x => this.commentMapper.Convert(x));
                    
                if (itemResponse != null)
                {
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
        public async Task<IActionResult> DeleteComment(Guid idComment, Guid postId)
        {
            try
            {
                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.commentService.DeleteComment(idComment, postId, userId);

                if (result)
                    return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return this.BadRequest();
        }
    }
}
