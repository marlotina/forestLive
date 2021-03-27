﻿using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPostsController : Controller
    {
        private readonly ILogger<UserPostsController> iLogger;
        private readonly IUserPostService iUserPostService;
        private readonly IUserVoteService iUserVoteService;
        private readonly IBirdPostMapper iBirdPostMapper;

        public UserPostsController(
            ILogger<UserPostsController> iLogger,
            IUserPostService iUserPostService,
            IUserVoteService iUserVoteService,
            IBirdPostMapper iBirdPostMapper)
        {
            this.iUserPostService = iUserPostService;
            this.iBirdPostMapper = iBirdPostMapper;
            this.iUserVoteService = iUserVoteService;
            this.iLogger = iLogger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPosts", Name = "GetPosts")]
        public async Task<IActionResult> GetPosts(string userId, Guid? specieId, string label, bool isPost)
        {
            try
            {
                var result = default(IEnumerable<PostDto>);
                if (isPost)
                {
                    result = await this.iUserPostService.GetUserPost(label, userId);
                }
                else 
                {
                    result = await this.iUserPostService.GetUserBirds(userId, specieId);
                }

                if (result != null && result.Any())
                {
                    var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);
                    var response = result.Select(x => this.iBirdPostMapper.Convert(x, postVotes));

                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapPoints", Name = "GetMapPoints")]
        public async Task<IActionResult> GetMapPoints(string userId)
        {
            try
            {
                var result = await this.iUserPostService.GetMapPointsByUserId(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.iBirdPostMapper.MapConvert(x));
                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("GetModalInfo", Name = "GetModalInfo")]
        public async Task<IActionResult> GetModalInfo(string postId, string userId)
        {
            try
            {
                var result = await this.iUserPostService.GetPostByPostId(postId, userId);

                if (result != null)
                {
                    var itemResponse = this.iBirdPostMapper.ModalConvert(result);
                    return this.Ok(itemResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
