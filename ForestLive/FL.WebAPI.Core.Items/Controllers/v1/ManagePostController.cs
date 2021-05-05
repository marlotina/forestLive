using System;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManagePostController : ControllerBase
    {
        private readonly ILogger<ManagePostController> iLogger;
        private readonly IManagePostService iManagePostService;
        private readonly IPostMapper iPostMapper;

        public ManagePostController(IManagePostService iManagePostService,
            IPostMapper iPostMapper,
            ILogger<ManagePostController> iLogger)
        {
            this.iLogger = iLogger;
            this.iManagePostService = iManagePostService ?? throw new ArgumentNullException(nameof(iManagePostService));
            this.iPostMapper = iPostMapper ?? throw new ArgumentNullException(nameof(iPostMapper));
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

                var post = this.iPostMapper.Convert(request);
                var bytes = Convert.FromBase64String(request.ImageData.Split(',')[1]);


                var result = await this.iManagePostService.AddBirdPost(post, bytes, request.ImageName, request.isPost);
                
                if (result != null)
                {
                    var postResponse = this.iPostMapper.Convert(result);
                    return this.CreatedAtRoute("GetPost", new { id = postResponse.Id }, postResponse);
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

        [HttpDelete]
        [Route("DeletePost", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty) {
                    this.BadRequest();
                }

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var result = await this.iManagePostService.DeleteBirdPost(postId, userId);

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