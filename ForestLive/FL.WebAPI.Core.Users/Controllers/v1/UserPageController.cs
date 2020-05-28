﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;
using FL.LogTrace.Contracts.Standard;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPageController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService usersService;
        private readonly IUserPageMapper userPageMapper;

        public UserPageController(
            IUserService usersService,
            ILogger<UserController> logger,
            IUserPageMapper userPageMapper)
        {
            this.logger = logger;
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
                this.logger.LogError( ex);
                return this.Problem();
            }
        }
    }
}