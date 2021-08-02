using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly ILogger<ManageUserController> logger;
        private readonly IUserManagedService usersManagedService;
        private readonly IUserMapper userMapper;

        public ManageUserController(
            IUserManagedService usersManagedService,
            IUserMapper userMapper,
            ILogger<ManageUserController> logger)
        {
            this.logger = logger;
            this.usersManagedService = usersManagedService;            
            this.userMapper = userMapper;
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserRequest request)
        {
            try
            {
                var user = this.userMapper.Convert(request);

                var userId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                if (userId != request.Id)
                    return this.Unauthorized();

                if (await this.usersManagedService.UpdateAsync(user, userId))
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
        public async Task<IActionResult> Delete([FromQuery] string userId)
        {
            try
            {
                var userWebId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                if (await this.usersManagedService.DeleteAsync(userId, userWebId))
                    return NoContent();

                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }


        [HttpGet]
        [Route("GetUserProfile", Name = "GetUserProfile")]
        public async Task<IActionResult> GetUserProfile([FromQuery] string userId)
        {
            try
            {
                var userWebId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                if (userId == userWebId) {
                    var result = await this.usersManagedService.GetUserAsync(userId, userWebId);

                    if (result != null)
                    {
                        var response = this.userMapper.Convert(result);
                        return Ok(response);
                    }

                    return this.NotFound();
                }

                return this.Unauthorized();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
