using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Contracts
{
    public interface IAutocompleteService
    {
        Task<IEnumerable<SpeciesCacheItem>> GetSpeciesByScienceName(string keys, string languageCode);

        Task<IEnumerable<SpeciesCacheItem>> GetSpeciesByName(string keys, string languageCode);
    }
}
