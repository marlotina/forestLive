using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Request;
using FL.WebAPI.Core.Birds.Application.Exceptions;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManagePostSpecieController : Controller
    {
        private readonly IManagePostSpeciesService iManagePostSpeciesService;
        private readonly IBirdSpeciePostMapper iBirdSpeciePostMapper;
        private readonly ILogger<ManagePostSpecieController> iLogger;

        public ManagePostSpecieController(
            IManagePostSpeciesService iManagePostSpeciesService,
            ILogger<ManagePostSpecieController> iLogger,
            IBirdSpeciePostMapper iBirdSpeciePostMapper)
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

        [HttpPut]
        [Route("UpdateSpecieId", Name = "UpdateSpecieId")]
        public async Task<IActionResult> UpdateSpecieId([FromBody] UpdateSpecieRequest request)
        {
            try
            {
                if (request == null)
                    return this.BadRequest();

                if (request.SpecieId == null
                    || string.IsNullOrWhiteSpace(request.SpecieName))
                    return this.BadRequest();

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.iManagePostSpeciesService.UpdateSpecieToPost(request, userId);

                if (result)
                {
                    return this.Ok();
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
        public async Task<IActionResult> DeletePost(Guid postId, Guid specieId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.iManagePostSpeciesService.DeleteBirdPost(postId, specieId, userId);

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
