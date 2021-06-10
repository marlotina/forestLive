using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.IO;
using FL.WebAPI.Core.Users.Api.Models.v1.Request;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Images;
using FL.Pereza.Helpers.Standard.JwtToken;
using System.Drawing;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserImageService iUserImageService;

        public UserImageController(
            IUserImageService iUserImageService,
            ILogger<UserController> iLogger)
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

                if (string.IsNullOrWhiteSpace(request.UserName) || request.UserId == Guid.Empty 
                    || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                    return this.BadRequest();

                
                var userWebId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

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
        public async Task<IActionResult> DeleteImage([FromQuery] Guid userId, string imageName)
        {
            try
            {
                if (userId == Guid.Empty || string.IsNullOrEmpty(imageName))
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