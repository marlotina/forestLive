﻿using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
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
    public class FollowUserController : ControllerBase
    {
        private readonly IFollowMapper voteMapper;
        private readonly ILogger<FollowUserController> logger;
        private readonly IFollowService iFollowService;

        public FollowUserController(
            IFollowMapper voteMapper,
            IFollowService iFollowService,
            ILogger<FollowUserController> logger)
        {
            this.logger = logger;
            this.voteMapper = voteMapper;
            this.iFollowService = iFollowService ?? throw new ArgumentNullException(nameof(iFollowService));
        }

        [HttpGet]
        [Route("GetFollowUser", Name = "GetFollowUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFollowUser(string userId, string followUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return null;

                var result = await this.iFollowService.GetFollow(userId, followUserId);

                if (result != null)
                {
                    var response = this.voteMapper.Convert(result);
                    return this.Ok(response);
                }
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpPost]
        [Route("AddFollowUser", Name = "AddFollowUser")]
        public async Task<IActionResult> AddFollowUser([FromBody] FollowerUserRequest request)
        {
            try
            {
                if (request == null)
                    return this.BadRequest();

                var follow = this.voteMapper.Convert(request);

                var result = await this.iFollowService.AddFollow(follow);

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
        [Route("DeleteFollowUser", Name = "DeleteFollowUser")]
        public async Task<IActionResult> DeleteFollowUser(string followId)
        {
            try
            {
                if (string.IsNullOrEmpty(followId))
                    return this.BadRequest();


                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.iFollowService.DeleteFollow(followId, userId);

                if (result)
                {
                    return this.NoContent();
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
        [Route("GetFollowByUserId", Name = "GetFollowByUserId")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFollowByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                    return null;

                var result = await this.iFollowService.GetFollowByUserId(userId);

                if (result != null)
                {
                    var response = result.Select(x => this.voteMapper.ConvertList(x));
                    return this.Ok(response);
                }
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

    }
}
