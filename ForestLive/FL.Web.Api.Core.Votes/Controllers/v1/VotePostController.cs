using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.Api.Core.Votes.Api.Mapper.v1.Contracts;
using FL.Web.Api.Core.Votes.Api.Models.v1.Request;
using FL.Web.Api.Core.Votes.Application.Exceptions;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VotePostController : ControllerBase
    {
        private readonly IVoteMapper voteMapper;
        private readonly ILogger<VotePostController> logger;
        private readonly IVotePostService votePostService;

        public VotePostController(
            IVoteMapper voteMapper,
            IVotePostService votePostService,
            ILogger<VotePostController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.votePostService = votePostService ?? throw new ArgumentNullException(nameof(votePostService));
        }

        [HttpPost]
        [Route("AddVote", Name = "AddVote")]
        public async Task<IActionResult> AddVote([FromBody] VoteRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || request.PostId == Guid.Empty || request.PostId == null)
                    return this.BadRequest();

                var votePost = this.voteMapper.Convert(request);

                var result = await this.votePostService.AddVotePost(votePost);

                if (result != null)
                {
                    var response = this.voteMapper.Convert(result);
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
        [Route("DeleteVote", Name = "DeleteVote")]
        public async Task<IActionResult> DeleteVote(Guid voteId, Guid postId)
        {
            try
            {
                if (voteId == null || voteId == Guid.Empty || postId == null || postId == Guid.Empty)
                {
                    return this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.votePostService.DeleteVotePost(voteId, userId);

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
