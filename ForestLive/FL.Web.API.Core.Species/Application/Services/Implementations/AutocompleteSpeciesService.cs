using FL.Cache.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Extensions;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using FL.Web.API.Core.Species.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Implementations
{
    public class AutocompleteSpeciesService : IAutocompleteSpeciesService
    {
        private readonly ISpeciesRepository iSpeciesRepository;
        private readonly IAutocompleteMapper iAutocompleteMapper;
        private readonly ICustomMemoryCache<IEnumerable<AutocompleteResponse>> iCustomMemoryCache;

        public AutocompleteSpeciesService(
            ISpeciesRepository iSpeciesRepository,
            IAutocompleteMapper iAutocompleteMapper,
            ICustomMemoryCache<IEnumerable<AutocompleteResponse>> iCustomMemoryCache)
        {
            this.iSpeciesRepository = iSpeciesRepository;
            this.iAutocompleteMapper = iAutocompleteMapper;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<IEnumerable<AutocompleteResponse>> GetSpeciesByKeys(string keys, Guid languageId)
        {
            var itemCache = this.iCustomMemoryCache.Get(languageId);

            if (itemCache == null || !itemCache.Any()) {
                var species = await this.iSpeciesRepository.GetSpeciesByLanguage(languageId);
                itemCache = species.Select(x => this.iAutocompleteMapper.Convert(x));

                this.iCustomMemoryCache.Add(languageId, itemCache);
            }

            var filter = itemCache.Where(x => x.NormalizeNameComplete.Contains(keys.NormalizeName()));
            
            return filter;
        }
    }
}
