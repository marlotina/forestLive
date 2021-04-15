﻿using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentVoteUserController : ControllerBase
    {
        private readonly IVoteMapper voteMapper;
        private readonly ILogger<CommentVoteUserController> logger;
        private readonly IVoteCommentService voteCommentService;

        public CommentVoteUserController(
            IVoteMapper voteMapper,
            IVoteCommentService voteCommentService,
            ILogger<CommentVoteUserController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.voteCommentService = voteCommentService ?? throw new ArgumentNullException(nameof(voteCommentService));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetCommentVote", Name = "GetCommentVote")]
        public async Task<IActionResult> GetCommentVote([FromBody] VoteCommentRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (request == null || !request.ListComments.Any())
                    return this.BadRequest();

                var result = await this.voteCommentService.GetVoteUserByComment(request.ListComments, request.UserId);

                if (result != null)
                {
                    return this.Ok(result);
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
        [Route("GetCommentVoteByUser", Name = "GetCommentVoteByUser")]
        public async Task<IActionResult> GetCommentVoteByUser(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return null;

                var result = await this.voteCommentService.GetCommentVotesByUserId(userId);

                if (result != null)
                {
                    var response = result.Select(x => this.voteMapper.Convert(x));
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
    }
}
