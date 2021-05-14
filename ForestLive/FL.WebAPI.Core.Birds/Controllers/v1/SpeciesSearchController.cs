using FL.Pereza.Helpers.Standard.Enums;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpeciesSearchController : Controller
    {
        private readonly ISpeciesService iBirdSpeciesService;
        private readonly IPostMapper iBirdSpeciePostMapper;
        private readonly IUserVoteService iUserVoteService;
        public SpeciesSearchController(
            IUserVoteService iUserVoteService,
            ISpeciesService iBirdSpeciesService,
            IPostMapper iBirdSpeciePostMapper)
        {
            this.iUserVoteService = iUserVoteService;
            this.iBirdSpeciesService = iBirdSpeciesService;
            this.iBirdSpeciePostMapper = iBirdSpeciePostMapper;
        }

        [HttpGet, Route("GetLastBirds", Name = "GetLastBirds")]
        public async Task<IActionResult> GetLastBirds()
        {
            var result = default(List<PostHomeDto>);
            result = await this.iBirdSpeciesService.GetLastBirds();

            if (result.Any())
            {
                var response = result.Select(x => this.iBirdSpeciePostMapper.Convert(x));
                return this.Ok(response);
            }

            return this.NoContent();
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
                var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);

                var response = result.Select(x => this.iBirdSpeciePostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }

        [HttpGet, Route("GetPendings", Name = "GetPendings")]
        public async Task<IActionResult> GetPendings(int orderBy)
        {
            var result = default(List<PostDto>);
            
            result = await this.iBirdSpeciesService.GetBirdBySpecie(Guid.Parse(StatusSpecie.NoSpecieId), orderBy);

            if (result.Any())
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var postList = result.Select(x => x.PostId);
                var postVotes = await this.iUserVoteService.GetVoteByUserId(postList, webUserId);

                var response = result.Select(x => this.iBirdSpeciePostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }
    }
}
