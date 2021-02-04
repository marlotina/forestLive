using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
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
        [Route("GetComment", Name = "GetComment")]
        public async Task<IActionResult> GetComment(Guid itemId)
        {
            try
            {
                if (itemId == Guid.Empty || itemId == null)
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByItem(itemId);
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
        public async Task<IActionResult> DeleteComment(Guid idComment, Guid itemId)
        {
            try
            {
                var result = await this.commentService.DeleteComment(idComment, itemId);
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
