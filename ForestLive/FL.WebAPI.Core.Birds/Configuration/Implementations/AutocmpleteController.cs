using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Configuration.Implementations
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutocmpleteController : Controller
    {
        private readonly IAutocompleteMapper autocompleteMapper;
        private readonly IAutocompleteService autocompleteService;
        public AutocmpleteController(
            IAutocompleteMapper autocompleteMapper,
            IAutocompleteService autocompleteService)
        {
            this.autocompleteMapper = autocompleteMapper;
            this.autocompleteService = autocompleteService;
        }

        [HttpGet, Route("GetNames", Name = "GetNames")]
        public async Task<IActionResult> Get(string text)
        {
            return View();
        }
    }
}
