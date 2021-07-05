using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Contracts
{
    public interface IAutocompleteSpeciesService
    {
        Task<IEnumerable<AutocompleteResponse>> GetSpeciesByScienceName(string keys, Guid languageId);

        Task<IEnumerable<AutocompleteResponse>> GetSpeciesByName(string keys, Guid languageId);
    }
}
