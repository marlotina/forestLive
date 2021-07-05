using FL.Pereza.Helpers.Standard.Language;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteSpeciesService iAutocompleteSpeciesService;
        public AutocompleteController(
            IAutocompleteSpeciesService iAutocompleteSpeciesService)
        {
            this.iAutocompleteSpeciesService = iAutocompleteSpeciesService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> GetNames(string text, string languageCode)
        {
         if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageCode)) {
                return this.BadRequest();
            }

            var languageId = LanguageHelper.GetLanguageByCode(languageCode);

            var result = await this.iAutocompleteSpeciesService.GetSpeciesByName(text, languageId); 

            if (result.Any()) {

                return this.Ok(result);
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

            var languageId = LanguageHelper.GetLanguageByCode(languageCode);

            var result = await this.iAutocompleteSpeciesService.GetSpeciesByScienceName(text, languageId);

            if (result.Any())
            {
                return this.Ok(result);
            }

            return this.NoContent();
        }
    }
}
