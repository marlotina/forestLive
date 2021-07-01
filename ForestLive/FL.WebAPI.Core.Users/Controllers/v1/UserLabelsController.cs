using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserLabelsController : Controller
    {
        private readonly ILogger<UserLabelsController> iLogger;
        private readonly IUserLabelService iUserLabelService;
        private readonly IUserLabelMapper iUserLabelMapper;

        public UserLabelsController(
            ILogger<UserLabelsController> iLogger,
            IUserLabelMapper iUserLabelMapper,
            IUserLabelService iUserLabelService)
        {
            this.iUserLabelService = iUserLabelService;
            this.iUserLabelMapper = iUserLabelMapper;
            this.iLogger = iLogger;
        }

        [HttpGet]
        [Route("GetLabelsAutocomplete", Name = "GetLabelsAutocomplete")]
        public async Task<IActionResult> GetLabelsAutocomplete(string userId)
        {
            try
            {
                var result = await this.iUserLabelService.GetUserLabelsDetails(userId);
                var response = new List<string>();
                if (result != null && result.Any())
                {
                    response = result.Select(x => x.Id).ToList();
                }

                return this.Ok(response);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetLabels", Name = "GetLabels")]
        public async Task<IActionResult> GetLabels(string userId)
        {
            try
            {
                var result = await this.iUserLabelService.GetUserLabelsDetails(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.iUserLabelMapper.Convert(x));
                    return this.Ok(result);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [Route("GetLabelsDetails", Name = "GetLabelsDetails")]
        public async Task<IActionResult> GetLabelsDetails(string userId)
        {
            try
            {
                var result = await this.iUserLabelService.GetUserLabelsDetails(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.iUserLabelMapper.ConvertDetails(x));
                    return this.Ok(response);
                }
                else
                    return this.NoContent();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpPost]
        [Route("AddLabel", Name = "AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] UserLabelRequest request)
        {
            try
            {
                var userLabel = this.iUserLabelMapper.Convert(request);
                var result = await this.iUserLabelService.AddLabel(userLabel);

                if (result != null)
                {
                    var response = this.iUserLabelMapper.Convert(result);
                    return this.Ok(response);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpDelete]
        [Route("DeleteLabel", Name = "DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(string label, string userId)
        {
            try
            {
                var userWebSite = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var result = await this.iUserLabelService.DeleteLabel(label, userId, userWebSite);

                if (result)
                {
                    return this.Ok(result);
                }
                else
                    return this.NoContent();
            }
            catch (UnauthorizedRemove ex)
            {
                this.iLogger.LogInfo(ex);
                return this.Unauthorized();
            }
            catch (UnauthorizedHasPostRemove ex)
            {
                this.iLogger.LogInfo(ex);
                return this.Forbid();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }
    }
}
