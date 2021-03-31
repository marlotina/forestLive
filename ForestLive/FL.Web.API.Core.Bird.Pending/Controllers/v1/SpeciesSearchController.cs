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
    public class SpeciesSearchController : Controller
    {
        private readonly IBirdSpeciesService iBirdSpeciesService;
        private readonly IBirdSpeciePostMapper iBirdSpeciePostMapper;
        public SpeciesSearchController(
            IBirdSpeciesService iBirdSpeciesService,
            IBirdSpeciePostMapper iBirdSpeciePostMapper)
        {
            this.iBirdSpeciesService = iBirdSpeciesService;
            this.iBirdSpeciePostMapper = iBirdSpeciePostMapper;
        }

        [HttpGet, Route("GetBirds", Name = "GetBirds")]
        public async Task<IActionResult> GetBirds(int orderBy, string specieId)
        {
            var result = default(List<PostDto>);

            if (!string.IsNullOrWhiteSpace(specieId) && specieId != "null")
            {
                var specieIdGuid = Guid.Parse(specieId);
                result = await this.iBirdSpeciesService.GetBirdBySpecie(specieIdGuid, orderBy);
            }
            else 
            {
                result = await this.iBirdSpeciesService.GetBirds(orderBy);
            }

            if (result.Any())
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var postList = result.Select(x => x.PostId);
                var postVotes = await this.iBirdSpeciesService.GetVoteByUserId(postList, webUserId);

                var response = result.Select(x => this.iBirdSpeciePostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }

        [HttpGet, Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId, Guid specieId)
        {
            var result = await this.iBirdSpeciesService.GetPost(postId, specieId);

            if (result != null)
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);

                var postList = new Guid[] { postId };
                var postVotes = await this.iBirdSpeciesService.GetVoteByUserId(postList, webUserId);

                var response = this.iBirdSpeciePostMapper.ConvertPost(result, postVotes);
                return this.Ok(response);
            }

            return this.NoContent();
        }
    }
}
