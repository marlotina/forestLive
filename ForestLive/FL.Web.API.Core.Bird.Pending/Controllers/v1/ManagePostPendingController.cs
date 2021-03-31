using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Request;
using FL.Web.API.Core.Bird.Pending.Application.Exceptions;
using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManagePostPendingController : Controller
    {
        private readonly IManagePostSpeciesService iManagePostSpeciesService;
        private readonly IBirdPendingMapper iBirdSpeciePostMapper;
        private readonly ILogger<ManagePostPendingController> iLogger;

        public ManagePostPendingController(
            IManagePostSpeciesService iManagePostSpeciesService,
            ILogger<ManagePostPendingController> iLogger,
            IBirdPendingMapper iBirdSpeciePostMapper)
        {
            this.iManagePostSpeciesService = iManagePostSpeciesService;
            this.iBirdSpeciePostMapper = iBirdSpeciePostMapper;
            this.iLogger = iLogger;
        }

        [HttpPost]
        [Route("AddPost", Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] PostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.ImageData))
                    return this.BadRequest();

                var post = this.iBirdSpeciePostMapper.Convert(request);
                var bytes = Convert.FromBase64String(request.ImageData.Split(',')[1]);


                var result = await this.iManagePostSpeciesService.AddBirdPost(post, bytes, request.ImageName, request.isPost);

                if (result != null)
                {
                    var postResponse = this.iBirdSpeciePostMapper.Convert(result);
                    return this.CreatedAtRoute("GetPost", new { id = postResponse.PostId }, postResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpDelete, Route("DeletePost", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.iManagePostSpeciesService.DeleteBirdPost(postId, userId);

                if (result)
                {
                    return this.Ok();
                }
                else
                    return this.BadRequest();
            }
            catch (UnauthorizedRemove ex)
            {
                this.iLogger.LogInfo(ex);
                return this.Unauthorized();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
