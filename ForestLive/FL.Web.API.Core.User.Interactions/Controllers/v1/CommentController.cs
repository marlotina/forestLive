using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Mapper.v1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Controllers.v1
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

        [HttpGet]
        [Route("GetCommentsByUser", Name = "GetCommentsByUser")]
        public async Task<IActionResult> GetCommentsByUser(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByUserId(userId);
                    
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
    }
}
