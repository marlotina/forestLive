using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;
using FL.LogTrace.Contracts.Standard;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using FL.Pereza.Helpers.Standard.JwtToken;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPageController : ControllerBase
    {
        private readonly ILogger<ManageUserController> logger;
        private readonly IUserService usersService;
        private readonly IUserPageMapper userPageMapper;

        public UserPageController(
            IUserService usersService,
            ILogger<ManageUserController> logger,
            IUserPageMapper userPageMapper)
        {
            this.logger = logger;
            this.usersService = usersService;
            this.userPageMapper = userPageMapper;
        }

        [HttpGet, Route("GetUsers", Name = "GetUsers")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await this.usersService.GetUsersAsync();

                if (result != null)
                {

                    var response = result.Select(x => this.userPageMapper.ConvertList(x));
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
                    var response = this.userPageMapper.ConvertList(result);
                    
                    var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                    if (!string.IsNullOrWhiteSpace(webUserId)) {

                        if (await this.usersService.IsFollow(webUserId, userName)) {
                            response.FollowId = $"{webUserId}Follow{userName}";
                            response.HasFollow = true;
                        }
                    }
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

        [HttpGet, Route("AutocompleteByUserName", Name = "AutocompleteByUserName")]
        [AllowAnonymous]
        public async Task<IActionResult> AutocompleteByUserName(string keys)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keys))
                    return this.BadRequest();

                var result = await this.usersService.AutocompleteByUserName(keys);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return NotFound();
        }

        [HttpGet, Route("GetUsersByKey", Name = "GetUsersByKey")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsersByKey(string keys)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keys))
                    return this.BadRequest();

                var result = await this.usersService.GetUsersByKey(keys);

                if (result != null)
                {
                    var response = result.Select(x => this.userPageMapper.Convert(x));
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return NotFound();
        }
    }
}
