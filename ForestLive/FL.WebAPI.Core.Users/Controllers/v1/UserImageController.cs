using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.IO;
using FL.WebAPI.Core.Users.Api.Models.v1.Request;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserImageService userImageService;

        public UserImageController(
            IUserImageService userImageService,
            ILogger<UserController> iLogger)
        {
            this.iLogger = iLogger;
            this.userImageService = userImageService;
        }


        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(ImageProfileRequest request)
        {
            if (request == null)
                return null;

            if (request.UserId == null || request.UserId == Guid.Empty || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                return this.BadRequest();

            try
            {
                var fileExtension = request.ImageName.Split('.')[1];
                var name = $"{request.UserId}.{fileExtension}";

                var bytes = Convert.FromBase64String(request.ImageBase64.Split(',')[1]);
                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var result = await this.userImageService.UploadImageAsync(imageStream, name, request.UserId);
                if (result)
                {
                    return this.Ok();
                }
                
            }
            catch (Exception ex)
            {
                this.iLogger.LogError("", ex);
                return this.BadRequest();
            }

            return this.BadRequest();
        }

        [HttpDelete, Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid userId)
        {
            if (userId == null || userId == Guid.Empty)
                return this.BadRequest();

            try
            {
                if (await this.userImageService.DeleteImageAsync(userId))
                    return NoContent();
                else
                    return this.NotFound();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError("", ex);
                return this.BadRequest();
            }
        }
    }
}