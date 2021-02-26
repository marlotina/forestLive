using FL.WebAPI.Core.Birds.Application.Services.Contracts;
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
        private readonly IBirdSpeciesService birdSpeciesService;

        public SpeciesSearchController(
            IBirdSpeciesService birdSpeciesService)
        {
            this.birdSpeciesService = birdSpeciesService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> Get(Guid birdSpecieId)
        {
            if (birdSpecieId == null || birdSpecieId == Guid.Empty)
            {
                return this.BadRequest();
            }

            var result = this.birdSpeciesService.GetBirdBySpecie(birdSpecieId);

            if (result.Any())
            {

                return this.Ok(result);
            }

            return this.NoContent();
        }
    }
}
