using FL.Web.API.Core.Species.Api.Models.v1.Response;
using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.Species.Application.Services.Contracts
{
    public interface IAutocompleteService
    {
        IEnumerable<AutocompleteResponse> GetSpeciesByKeys(string keys, Guid languageId);
    }
}
