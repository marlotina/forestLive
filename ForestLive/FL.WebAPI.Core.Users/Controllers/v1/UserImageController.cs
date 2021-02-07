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

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserImageService userImageService;

        public UserImageController(
            IUserImageService userImageService,
            ILogger<UserController> logger)
        {
            this.logger = logger;
            this.userImageService = userImageService;
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(ImageProfileRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserName) || request.UserId == null || request.UserId == Guid.Empty 
                    || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                    return this.BadRequest();

                var fileExtension = request.ImageName.Split('.')[1];
                var imageBase64 = request.ImageBase64.Split(',')[1];

                var image = fileExtension == ImageHelper.PNG_EXTENSION ? ImageHelper.FromBase64PNGToBase64JPG(imageBase64) : imageBase64;
                var nameFile = $"{request.UserName}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}";
                var bytes = Convert.FromBase64String(request.ImageBase64.Split(',')[1]);

                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var result = await this.userImageService.UploadImageAsync(imageStream, nameFile, request.UserId);
                if (result)
                {
                    return this.Ok();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return this.BadRequest();
        }

        [HttpDelete, Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid userId)
        {
            try
            {
                if (userId == null || userId == Guid.Empty)
                    return this.BadRequest();

                if (await this.userImageService.DeleteImageAsync(userId))
                    return NoContent();
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}