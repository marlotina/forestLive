﻿using FL.LogTrace.Contracts.Standard;
using FL.Web.Api.Core.Votes.Api.Mapper.v1.Contracts;
using FL.Web.Api.Core.Votes.Api.Models.v1.Request;
using FL.Web.Api.Core.Votes.Application.Services.Contracts;
using FL.Web.Api.Core.Votes.Configuration.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.Api.Core.Votes.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IPostConfiguration itemConfiguration;
        private readonly IVoteMapper voteMapper;
        private readonly ILogger<VoteController> logger;
        private readonly IVotePostService votePostService;

        public VoteController(
            IVoteMapper voteMapper,
            IVotePostService votePostService,
            IPostConfiguration itemConfiguration,
            ILogger<VoteController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.votePostService = votePostService ?? throw new ArgumentNullException(nameof(votePostService));
            this.itemConfiguration = itemConfiguration;
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
    }
}
