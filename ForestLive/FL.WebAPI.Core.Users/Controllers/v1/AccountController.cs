﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class AccountController : ControllerBase
    {
        readonly ILogger<AccountController> logger;
        readonly IAccountService accountService;
        readonly IRegisterMapper registerMapper;

        public AccountController(
            ILogger<AccountController> iLogger,
            IAccountService accountService,
            IRegisterMapper registerMapper)
        {
            this.logger = iLogger;
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
                this.logger.LogError("", ex);
                return this.Conflict();
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
            catch (UserNotFoundException ex)
            {
                this.logger.LogError("User not exist.", ex);
                return this.NotFound();
            }
            catch (Exception ex)
            {
                this.logger.LogError("Internal error.", ex);
                return this.BadRequest();
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
                this.logger.LogError("", ex);
                return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError("", ex);
                return this.BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword", Name = "ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            try
            {
                await this.accountService.ResetPasswordAsync(request.UserId, request.Code, request.NewPassword);
                return this.Ok();
            }
            catch (UserNotFoundException ex)
            {
                this.logger.LogError("", ex);
                return this.NotFound();
            }
        }
    }
}
