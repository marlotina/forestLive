using FL.Pereza.Helpers.Standard.Language;
using FL.Web.API.Core.ExternalData.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : Controller
    {
        private readonly ICountriesService iCountriesService;
        public CountryController(
            ICountriesService iCountriesService)
        {
            this.iCountriesService = iCountriesService;
        }

        [HttpGet, Route("GetCountries", Name = "GetCountries")]
        public async Task<IActionResult> GetCountries(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                return this.BadRequest();
            }

            var languageId = LanguageHelper.GetLanguageByCode(languageCode);

            var result = await this.iCountriesService.GetCountryByLanguage(languageId);

            if (result.Any())
            {
                return this.Ok(result);
            }

            return this.NoContent();
        }
    }
}
