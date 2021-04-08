using FL.Pereza.Helpers.Standard.Enums;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PendingSearchController : Controller
    {
        private readonly IBirdPendingService iBirdPendingService;
        private readonly IBirdPendingMapper iBirdPendingPostMapper;
        public PendingSearchController(
            IBirdPendingService iBirdPendingService,
            IBirdPendingMapper iBirdPendingPostMapper)
        {
            this.iBirdPendingService = iBirdPendingService;
            this.iBirdPendingPostMapper = iBirdPendingPostMapper;
        }

        [HttpGet, Route("GetPendings", Name = "GetPendings")]
        public async Task<IActionResult> GetPendings(int orderBy)
        {
            var result = default(List<PostDto>);
            
            result = await this.iBirdPendingService.GetBirdBySpecie(orderBy);

            if (result.Any())
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var postList = result.Select(x => x.PostId);
                var postVotes = await this.iBirdPendingService.GetVoteByUserId(postList, webUserId);

                var response = result.Select(x => this.iBirdPendingPostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }

        [HttpGet, Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var result = await this.iBirdPendingService.GetPost(postId);

            if (result != null)
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var postList = new Guid[] { postId };
                var postVotes = await this.iBirdPendingService.GetVoteByUserId(postList, webUserId);

                var response = this.iBirdPendingPostMapper.Convert(result, postVotes);
                return this.Ok(response);
            }

            return this.NoContent();
        }
    }
}
