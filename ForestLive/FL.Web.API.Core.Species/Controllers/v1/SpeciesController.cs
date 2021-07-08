using FL.Pereza.Helpers.Standard.Language;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SpeciesController : Controller
    {
        private readonly ISpeciesService iSpeciesService;
        private readonly IAutocompleteMapper iAutocompleteMapper;
        public SpeciesController(
            IAutocompleteMapper iAutocompleteMapper,
            ISpeciesService iSpeciesService)
        {
            this.iSpeciesService = iSpeciesService;
            this.iAutocompleteMapper = iAutocompleteMapper;
        }

        [HttpGet, Route("GetAllSpecies", Name = "GetAllSpecies")]
        public async Task<IActionResult> GetNames()
        {
            var result = await this.iSpeciesService.GetAllSpecies(); 

            if (result.Any()) {

                return this.Ok(result.Select(x => this.iAutocompleteMapper.ConvertSpecie(x)));
            }

            return this.NoContent();
        }
    }
}
