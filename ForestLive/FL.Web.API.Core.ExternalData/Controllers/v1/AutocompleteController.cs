using FL.Web.API.Core.ExternalData.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.ExternalData.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteService iAutocompleteSpeciesService;
        private readonly IAutocompleteMapper iAutocompleteMapper;

        public AutocompleteController(
            IAutocompleteMapper iAutocompleteMapper,
            IAutocompleteService iAutocompleteSpeciesService)
        {
            this.iAutocompleteSpeciesService = iAutocompleteSpeciesService;
            this.iAutocompleteMapper = iAutocompleteMapper;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> GetNames(string text, string languageCode)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageCode)) {
                return this.BadRequest();
            }

            var result = await this.iAutocompleteSpeciesService.GetSpeciesByName(text, languageCode); 

            if (result.Any()) {

                return this.Ok(result.Select(x => this.iAutocompleteMapper.Convert(x)));
            }

            return this.NoContent();
        }

        [HttpGet, Route("GetScienceNames", Name = "GetScienceNames")]
        public async Task<IActionResult> GetScienceNames(string text, string languageCode)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageCode))
            {
                return this.BadRequest();
            }

            var result = await this.iAutocompleteSpeciesService.GetSpeciesByScienceName(text, languageCode);

            if (result.Any())
            {
                return this.Ok(result.Select(x => this.iAutocompleteMapper.Convert(x)));
            }

            return this.NoContent();
        }
    }
}
