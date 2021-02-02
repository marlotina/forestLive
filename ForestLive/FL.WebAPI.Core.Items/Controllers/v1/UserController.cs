using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserItemService userItemService;
        private readonly IBirdPostMapper birdPostMapper;

        public UserController(
            ILogger<UserController> logger,
            IUserItemService userItemService,
            IBirdPostMapper birdPostMapper)
        {
            this.userItemService = userItemService;
            this.birdPostMapper = birdPostMapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("GetPosts", Name = "GetPosts")]
        public async Task<IActionResult> GetPosts(Guid userId)
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
