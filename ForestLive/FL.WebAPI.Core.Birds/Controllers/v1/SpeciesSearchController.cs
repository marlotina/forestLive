using FL.Pereza.Helpers.Standard.Enums;
using FL.Pereza.Helpers.Standard.JwtToken;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpeciesSearchController : Controller
    {
        private readonly IBirdSpeciesService birdSpeciesService;
        private readonly IBirdSpeciePostMapper birdSpeciePostMapper;
        public SpeciesSearchController(
            IBirdSpeciesService birdSpeciesService,
            IBirdSpeciePostMapper birdSpeciePostMapper)
        {
            this.birdSpeciesService = birdSpeciesService;
            this.birdSpeciePostMapper = birdSpeciePostMapper;
        }

        [HttpGet, Route("GetBirds", Name = "GetBirds")]
        public async Task<IActionResult> Get(Guid birdSpecieId, int orderBy)
        {
            if (birdSpecieId == null || birdSpecieId == Guid.Empty)
            {
                return this.BadRequest();
            }

            var result = await this.birdSpeciesService.GetBirdBySpecie(birdSpecieId, orderBy);

            if (result.Any())
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var postList = result.Select(x => x.PostId);
                var postVotes = await this.birdSpeciesService.GetVoteByUserId(postList, webUserId);

                var response = result.Select(x => this.birdSpeciePostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }

        [HttpGet, Route("GetPendingBirds", Name = "GetPendingBirds")]
        public async Task<IActionResult> GetPendingBirds(int orderBy)
        {
            var result = await this.birdSpeciesService.GetBirdBySpecie(Guid.Parse(StatusSpecie.NoSpecieId), orderBy);

            if (result.Any())
            {
                var webUserId = JwtTokenHelper.GetClaim(HttpContext.Request.Headers[JwtTokenHelper.TOKEN_HEADER]);
                var postList = result.Select(x => x.PostId);
                var postVotes = await this.birdSpeciesService.GetVoteByUserId(postList, webUserId);


                var response = result.Select(x => this.birdSpeciePostMapper.Convert(x, postVotes));
                return this.Ok(response);
            }

            return this.NoContent();
        }
    }
}
