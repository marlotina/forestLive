using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.LogTrace.Contracts.Standard;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly ILogger<AccountController> logger;
        readonly IAccountService accountService;
        readonly IRegisterMapper registerMapper;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService,
            IRegisterMapper registerMapper)
        {
            this.logger = logger;
            this.accountService = accountService;
            this.registerMapper = registerMapper;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register", Name = "Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = this.registerMapper.Convert(request);
                if (user == null)
                    return BadRequest();
                var token = await this.accountService.RegisterAsync(user, request.Password);

                if (!string.IsNullOrEmpty(token))
                {
                    var userResponse = this.registerMapper.Convert(user);
                    return this.CreatedAtRoute("UserGetById", new { id = userResponse.Id }, userResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (UserDuplicatedException ex)
            {
                this.logger.LogInfo(ex);
                return this.Conflict("CONFLICT_USERNAME");
            }
            catch (EmailDuplicatedException ex)
            {
                this.logger.LogInfo(ex);
                return this.Conflict("CONFLICT_EMAIL");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            try
            {
                Guid userId;
                var isValid = Guid.TryParse(request.UserId, out userId);
                if (!string.IsNullOrWhiteSpace(request.Code) || isValid)
                {
                    await this.accountService.ConfirmEmailAsync(userId, request.Code);
                    return this.Ok();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginRequest model)
        {
            try
            {
                var user = this.accountService.Authenticate(model.Email, model.Password);

                if (user == null)
                    return BadRequest("EMAIL_PASS");

                return Ok(user);
            }
            catch (UserNotEmailConfirm ex)
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

        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword", Name = "ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            try
            {
                await this.accountService.ForgotPasswordAsync(request.Email);
                return this.Ok();
            }
            catch (UserNotFoundException ex)
            {
                this.logger.LogInfo(ex);
                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Code) 
                    || string.IsNullOrWhiteSpace(request.NewPassword) || request.UserId == null)
                    return this.BadRequest();

                await this.accountService.ResetPasswordAsync(request.UserId, request.Code, request.NewPassword);
                return this.Ok();
            }
            catch (UserNotFoundException ex)
            {
                this.logger.LogInfo(ex);
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
