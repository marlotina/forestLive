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
    public class VoteUserController : ControllerBase
    {
        private readonly IVoteMapper voteMapper;
        private readonly ILogger<VoteUserController> logger;
        private readonly IVotePostService votePostService;

        public VoteUserController(
            IVoteMapper voteMapper,
            IVotePostService votePostService,
            ILogger<VoteUserController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.votePostService = votePostService ?? throw new ArgumentNullException(nameof(votePostService));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetPostVote", Name = "GetPostVote")]
        public async Task<IActionResult> GetPostVote([FromBody] VotePostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (request == null || !request.ListPosts.Any())
                    return this.BadRequest();

                var result = await this.votePostService.GetVoteUserByPost(request.ListPosts, request.UserId);

                if (result != null)
                {
                    var response = result.Select(x => this.voteMapper.ConvertUserVote(x));
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

        [HttpGet]
        [AllowAnonymous]
        [Route("GetVoteByUser", Name = "GetVoteByUser")]
        public async Task<IActionResult> GetVoteByUser(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return null;

                var result = await this.votePostService.GetVotesByUserId(userId);

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
