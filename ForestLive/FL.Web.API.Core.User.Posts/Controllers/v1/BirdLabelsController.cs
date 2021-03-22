using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdLabelsController : Controller
    {
        private readonly ILogger<BirdLabelsController> logger;
        private readonly IUserLabelService userLabelService;

        public BirdLabelsController(
            ILogger<BirdLabelsController> logger,
            IUserLabelService userLabelService)
        {
            this.userLabelService = userLabelService;
            this.logger = logger;
        }

        [HttpGet]
        [Route("GetUserLabels", Name = "GetUserLabels")]
        public async Task<IActionResult> GetUserLabels(string userId)
        {
            try
            {
                var result = await this.userLabelService.GetLabelsByUser(userId);

                if (result != null && result.Any())
                {
                    return this.Ok(result);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
