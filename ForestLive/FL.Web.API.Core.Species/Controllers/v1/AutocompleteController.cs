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
    public class AutocompleteController : Controller
    {
        private readonly IAutocompleteService autocompleteService;
        public AutocompleteController(
            IAutocompleteService autocompleteService)
        {
            this.autocompleteService = autocompleteService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public IActionResult GetNames(string text, string languageCode)
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
