using FL.Web.API.Core.Species.Api.Models.v1.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Contracts
{
    public interface IAutocompleteSpeciesService
    {
        Task<IEnumerable<AutocompleteResponse>> GetSpeciesByKeys(string keys, Guid languageId);
    }
}
