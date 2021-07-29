using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Pereza.Helpers.Standard.Language;
using FL.WebAPI.Core.Searchs.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPostsController : Controller
    {
        private readonly ILogger<UserPostsController> iLogger;
        private readonly IUserPostService iUserPostService;
        private readonly IUserVoteService iUserVoteService;
        private readonly IPostMapper iBirdPostMapper;
        private readonly ISpecieInfoService iSpecieInfoService;

        public UserPostsController(
            ILogger<UserPostsController> iLogger,
            IUserPostService iUserPostService,
            ISpecieInfoService iSpecieInfoService,
            IUserVoteService iUserVoteService,
            IPostMapper iBirdPostMapper)
        {
            this.iUserPostService = iUserPostService;
            this.iBirdPostMapper = iBirdPostMapper;
            this.iUserVoteService = iUserVoteService;
            this.iSpecieInfoService = iSpecieInfoService;
            this.iLogger = iLogger;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId, string languageCode, string userId)
        {
            try
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var languageId = LanguageHelper.GetLanguageByCode("en");
                if (postId == Guid.Empty)
                {
                    this.BadRequest();
                }

                var result = await this.iUserPostService.GetPost(postId, userId);

                if (result != null)
                {
                    var postList = new Guid[] { postId };

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);
                    var itemResponse = this.iBirdPostMapper.ConvertPost(result, postVotes);

                    if (itemResponse.SpecieId.HasValue)
                    {
                        var specieInfo = await this.iSpecieInfoService.GetSpecieById(result.SpecieId.Value, languageId);
                        itemResponse.SpecieUrl = specieInfo?.UrlSpecie;
                        itemResponse.BirdSpecie = specieInfo?.NameComplete;
                    }

                    return this.Ok(itemResponse);
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
        [AllowAnonymous]
        [Route("GetUserPosts", Name = "GetUserPosts")]
        public async Task<IActionResult> GetUserPosts(string userId, string type, string label, string languageCode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                    return this.BadRequest();

                var languageId = LanguageHelper.GetLanguageByCode(languageCode);

                var result = await this.iUserPostService.GetUserPosts(userId, label, type, languageId);

                if (result != null && result.Any())
                {
                    var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                    var postList = result.Select(x => x.PostId);

                    var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);
                    var response = result.Select(x => this.iBirdPostMapper.Convert(x, postVotes));

                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetMapPoints", Name = "GetMapPoints")]
        public async Task<IActionResult> GetMapPoints(string userId)
        {
            try
            {
                var result = await this.iUserPostService.GetMapPointsByUserId(userId);

                if (result != null && result.Any())
                {
                    var response = result.Select(x => this.iBirdPostMapper.Convert(x));
                    return this.Ok(response);
                }
                else
                    return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetModalInfo", Name = "GetModalInfo")]
        public async Task<IActionResult> GetModalInfo(Guid postId, string languageCode, string userId)
        {
            try
            {
                var result = await this.iUserPostService.GetPost(postId, userId);
                var languageId = LanguageHelper.GetLanguageByCode(languageCode);

                if (result != null)
                {
                    var itemResponse = this.iBirdPostMapper.ModalConvert(result);
                    if (itemResponse.SpecieId.HasValue)
                    {
                        var specieInfo = await this.iSpecieInfoService.GetSpecieById(result.SpecieId.Value, languageId);
                        itemResponse.SpecieUrl = specieInfo?.UrlSpecie;
                        itemResponse.BirdSpecie = specieInfo?.NameComplete;
                    }

                    return this.Ok(itemResponse);
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
    }
}
