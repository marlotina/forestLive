using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IAutocompleteService
    {
        IEnumerable<AutocompleteResponse> GetSpeciesByKeys(string keys, string languageId);
    }
}
