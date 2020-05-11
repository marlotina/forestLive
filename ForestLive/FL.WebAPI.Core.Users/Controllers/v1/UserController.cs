using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserService usersService;
        private readonly IUserMapper userMapper;

        public UserController(
            IUserService usersService,
            IUserMapper userMapper,
            ILogger<UserController> iLogger)
        {
            this.iLogger = iLogger;
            this.usersService = usersService;            
            this.userMapper = userMapper;
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

        [Authorize]
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

        [Authorize]
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
    }
}
