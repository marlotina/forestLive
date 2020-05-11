using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserService usersService;
        private readonly IUserMapper userMapper;
        private readonly IUserImageService userImageService;

        public UserImageController(
            IUserService usersService,
            IUserImageService userImageService,
            IUserMapper userMapper,
            ILogger<UserController> iLogger)
        {
            this.iLogger = iLogger;
            this.usersService = usersService;
            this.userMapper = userMapper;
            this.userImageService = userImageService;
        }

        [Authorize]
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post([FromForm(Name = "file")] IFormFile formFile, Guid userId)
        {
            if (userId == null || userId == Guid.Empty || !Request.Form.Files.Any())
                return this.BadRequest();

            try
            {
                if (Request.Form.Files.Any())
                {
                    var fileExtension = Request.Form.Files[0].FileName.Split('.')[1];
                    var name = $"{userId}.jpg";
                    var result = await this.userImageService.UploadImageAsync(Request.Form.Files[0].OpenReadStream(), name, userId);
                    if (result)
                    {
                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError("", ex);
                return this.BadRequest();
            }

            return this.BadRequest();
        }

        [Authorize]
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