﻿using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdUserController : Controller
    {
        private readonly ILogger<BirdUserController> logger;
        private readonly IBirdUserPostService userItemService;
        private readonly IBirdPostMapper birdPostMapper;

        public BirdUserController(
            ILogger<BirdUserController> logger,
            IBirdUserPostService userItemService,
            IBirdPostMapper birdPostMapper)
        {
            this.userItemService = userItemService;
            this.birdPostMapper = birdPostMapper;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetPosts", Name = "GetPosts")]
        public async Task<IActionResult> GetPosts(string userId)
        {
            try
            {
                var result = await this.userItemService.GetBlogPostsForUserId(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.birdPostMapper.Convert(x));
                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            this.BadRequest();
        }
    }
}