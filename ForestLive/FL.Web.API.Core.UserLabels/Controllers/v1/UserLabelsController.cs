﻿using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.User.Posts.Api.Models.v1.Request;
using FL.Web.API.Core.User.Posts.Application.Exceptions;
using FL.Web.API.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.UserLabels.Api.Mapper.v1.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.UserLabels.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserLabelsController : Controller
    {
        private readonly ILogger<UserLabelsController> logger;
        private readonly IUserLabelService userLabelService;
        private readonly IBirdPostMapper birdPostMapper;

        public UserLabelsController(
            ILogger<UserLabelsController> logger,
            IBirdPostMapper birdPostMapper,
            IUserLabelService userLabelService)
        {
            this.userLabelService = userLabelService;
            this.birdPostMapper = birdPostMapper;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetUserLabels", Name = "GetLabels")]
        public async Task<IActionResult> GetLabels(string userId)
        {
            try
            {
                var result = await this.userLabelService.GetLabelsByUser(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.birdPostMapper.Convert(x));
                    return this.Ok(result);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [Route("GetUserLabelsDetails", Name = "GetLabelsDetails")]
        public async Task<IActionResult> GetLabelsDetails(string userId)
        {
            try
            {
                var result = await this.userLabelService.GetUserLabelsDetails(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.birdPostMapper.Convert(x));
                    return this.Ok(response);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpPost]
        [Route("AddLabel", Name = "AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] UserLabelRequest request)
        {
            try
            {
                var userLabel = this.birdPostMapper.Convert(request);
                var result = await this.userLabelService.AddLabel(userLabel);

                if (result != null)
                {
                    var response = this.birdPostMapper.Convert(result);
                    return this.Ok(response);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpDelete]
        [Route("DeleteUserLabel", Name = "DeleteLabel")]
        public async Task<IActionResult> DeleteUserLabel(string label, string userId)
        {
            try
            {
                var userWebSite = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.userLabelService.DeleteLabel(label, userId, userWebSite);

                if (result)
                {
                    return this.Ok(result);
                }
                else
                    return this.NoContent();
            }
            catch (UnauthorizedRemove ex)
            {
                this.logger.LogInfo(ex);
                return this.Unauthorized();
            }
            catch (UnauthorizedHasPostRemove ex)
            {
                this.logger.LogInfo(ex);
                return this.BadRequest("HasPost");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
