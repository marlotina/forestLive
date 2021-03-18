using FL.Pereza.Helpers.Standard.Language;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteService autocompleteService;
        public AutocompleteController(
            IAutocompleteService autocompleteService)
        {
            this.autocompleteService = autocompleteService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> GetNames(string text, string languageCode)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageCode)) {
                return this.BadRequest();
            }

            var languageId = LanguageHelper.GetLanguageByCode(languageCode);

            var result = this.autocompleteService.GetSpeciesByKeys(text, languageId); 

            if (result.Any()) {

                return this.Ok(result);
            }

            return this.NoContent();
        }
    }
}
