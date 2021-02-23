using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutocmpleteController : Controller
    {
        private readonly IAutocompleteService autocompleteService;
        public AutocmpleteController(
            IAutocompleteService autocompleteService)
        {
            this.autocompleteService = autocompleteService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> Get(string text, string languageId)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(languageId)) {
                return this.BadRequest();
            }

            var result = this.autocompleteService.GetSpeciesByKeys(text, languageId);

            if (result.Any()) {

                return this.Ok(result);
            }

            return View();
        }
    }
}
