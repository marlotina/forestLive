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
using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPageController : ControllerBase
    {
        private readonly ILogger<UserController> iLogger;
        private readonly IUserService usersService;
        private readonly IUserPageMapper userPageMapper;

        public UserPageController(
            IUserService usersService,
            IUserMapper userMapper,
            ILogger<UserController> iLogger,
            IUserPageMapper userPageMapper)
        {
            this.iLogger = iLogger;
            this.usersService = usersService;
            this.userPageMapper = userPageMapper;
        }

        [HttpGet, Route("UserByUsername", Name = "UserByUsername")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                    return this.BadRequest();

                var result = await this.usersService.GetByUserNameAsync(username);

                if (result != null)
                {
                    var response = this.userPageMapper.Convert(result);
                    return Ok(response);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError("", ex);
                return this.BadRequest();
            }
            
        }
    }
}
