using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.User.Posts.Api.Models.v1.Request;
using FL.Web.API.Core.User.Posts.Application.Exceptions;
using FL.Web.API.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
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
        private readonly IBirdPostMapper birdPostMapper;

        public BirdLabelsController(
            ILogger<BirdLabelsController> logger,
            IBirdPostMapper birdPostMapper,
            IUserLabelService userLabelService)
        {
            this.userLabelService = userLabelService;
            this.birdPostMapper = birdPostMapper;
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

        [HttpGet]
        [Route("GetUserLabelsDetails", Name = "GetUserLabelsDetails")]
        public async Task<IActionResult> GetUserLabelsDetails(string userId)
        {
            try
            {
                var result = await this.userLabelService.GetUserLabelsDetails(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.birdPostMapper.Convert(x));
                    return this.Ok(response);
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

        [HttpPost]
        [Route("AddLabel", Name = "AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] UserLabelRequest request)
        {
            try
            {
                var userLabel = this.birdPostMapper.Convert(request);
                var result = await this.userLabelService.AddLabel(userLabel);

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

        [HttpDelete]
        [Route("DeleteUserLabel", Name = "DeleteUserLabel")]
        public async Task<IActionResult> DeleteUserLabel(string label)
        {
            try
            {
                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.userLabelService.DeleteLabel(label, userId);

                if (result)
                {
                    return this.Ok();
                }
                else
                    return this.NoContent();
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
