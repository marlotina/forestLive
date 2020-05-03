using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserService usersService;
        private readonly IUserMapper userMapper;
        private readonly IUserImageService userImageService;

        public UserController(
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

        [HttpGet, Route("UserGetById", Name = "UserGetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.usersService.GetByIdAsync(id);

            if (result != null)
            {
                var response = this.userMapper.Convert(result);
                return Ok(response);
            }

            return NotFound();
        }

        [HttpGet, Route("UserFindByEmail", Name = "UserFindByEmail")]
        public async Task<IActionResult> Find(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return this.BadRequest();

            var result = await this.usersService.FindByEmailAsync(email);

            if (result != null && result.Any())
            {
                var response = result.Select(x => this.userMapper.Convert(x)).ToList();
                return Ok(response);
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserRequest request)
        {
            try
            {
                var user = this.userMapper.Convert(request);
                if (await this.usersService.UpdateAsync(user))
                    return Ok();
                else
                    return this.BadRequest();
            }
            catch (UserNotFoundException ex)
            {
                this.iLogger.LogError("", ex);
                return this.NotFound();
            }
        }

        [HttpDelete, Route("")]
        public async Task<IActionResult> Delete([FromQuery] Guid userId)
        {
            try
            {
                if (await this.usersService.DeleteAsync(userId))
                    return NoContent();
                else
                    return this.BadRequest();
            }
            catch (UserNotFoundException ex)
            {
                this.iLogger.LogError("", ex);
                return this.NotFound();
            }
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post([FromForm(Name = "file")] IFormFile formFile, Guid id)
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    var fileExtension = Request.Form.Files[0].FileName.Split('.')[1];
                    var name = $"{id}.jpg";
                    var result = await this.userImageService.UploadImageAsync(Request.Form.Files[0].OpenReadStream(), name, id);
                    if (result)
                    {
                        return this.Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest();
            }

            return this.BadRequest();
        }

        [HttpDelete, Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromQuery] Guid userId)
        {
            try
            {
                if (await this.userImageService.DeleteImageAsync(userId))
                    return NoContent();
                else
                    return this.BadRequest();
            }
            catch (UserNotFoundException ex)
            {
                this.iLogger.LogError("", ex);
                return this.NotFound();
            }
        }
    }
}
