using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using FL.LogTrace.Contracts.Standard;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService usersService;
        private readonly IUserMapper userMapper;

        public UserController(
            IUserService usersService,
            IUserMapper userMapper,
            ILogger<UserController> logger)
        {
            this.logger = logger;
            this.usersService = usersService;            
            this.userMapper = userMapper;
        }

        [HttpGet, Route("UserGetById", Name = "UserGetById")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                    return this.BadRequest();

                var result = await this.usersService.GetByIdAsync(id);

                if (result != null)
                {
                    var response = this.userMapper.Convert(result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return NotFound();
        }

        [HttpGet, Route("UserGetByUserName", Name = "UserGetByUserName")]
        [AllowAnonymous]
        public async Task<IActionResult> UserGetByUserName(string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                    return this.BadRequest();

                var result = await this.usersService.GetByUserNameAsync(userName);

                if (result != null)
                {
                    var response = this.userMapper.ConvertUserInfo(result);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return NotFound();
        }

        [HttpGet, Route("UserFindByEmail", Name = "UserFindByEmail")]
        public async Task<IActionResult> Find(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return this.BadRequest();

                var result = await this.usersService.FindByEmailAsync(email);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.userMapper.Convert(x)).ToList();
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
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
                    return this.NotFound(); ;
            }
            catch (UserDuplicatedException ex)
            {
                this.logger.LogInfo(ex);
                return this.Conflict();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
        
        [HttpDelete, Route("")]
        public async Task<IActionResult> Delete([FromQuery] Guid userId)
        {
            try
            {
                if (await this.usersService.DeleteAsync(userId))
                    return NoContent();

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
