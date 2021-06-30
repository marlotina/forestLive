using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using FL.WebAPI.Core.Users.Api.Models.v1.Request;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManageUserImageController : ControllerBase
    {
        private readonly ILogger<ManageUserController> iLogger;
        private readonly IUserImageService iUserImageService;

        public ManageUserImageController(
            IUserImageService iUserImageService,
            ILogger<ManageUserController> iLogger)
        {
            this.iLogger = iLogger;
            this.iUserImageService = iUserImageService;
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(ImageProfileRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                    return this.BadRequest();

                
                var userWebId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                if (userWebId != request.UserId)
                    return this.Unauthorized();

                var result = await this.iUserImageService.UploadImageAsync(request, userWebId);
                if (result)
                {
                    return this.Ok();
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }

            return this.BadRequest();
        }

        [HttpDelete, Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] string userId, string imageName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrEmpty(imageName))
                    return this.BadRequest();

                var userWebId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                if (await this.iUserImageService.DeleteImageAsync(userId, userWebId, imageName))
                    return this.Ok();
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}