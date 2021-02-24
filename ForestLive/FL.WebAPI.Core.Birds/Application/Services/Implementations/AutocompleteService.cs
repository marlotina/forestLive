using FL.Cache.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Extensions;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class AutocompleteService : IAutocompleteService
    {
        private readonly ISpeciesRepository speciesRepository;
        private readonly IAutocompleteMapper autocompleteMapper;
        private readonly ICustomMemoryCache<IEnumerable<AutocompleteResponse>> customMemoryCache;

        public AutocompleteService(
            ISpeciesRepository speciesRepository,
            IAutocompleteMapper autocompleteMapper,
            ICustomMemoryCache<IEnumerable<AutocompleteResponse>> customMemoryCache)
        {
            this.speciesRepository = speciesRepository;
            this.autocompleteMapper = autocompleteMapper;
            this.customMemoryCache = customMemoryCache;
        }

        public IEnumerable<AutocompleteResponse> GetSpeciesByKeys(string keys, string languageId)
        {

            var itemCache = this.customMemoryCache.Get(languageId);

            if (itemCache == null || !itemCache.Any()) {
                var species = this.speciesRepository.GetSpeciesByLanguage(languageId);
                itemCache = species.Select(x => this.autocompleteMapper.Convert(x));

                this.customMemoryCache.Add(languageId, itemCache);
            }

            var filter = itemCache.Where(x => x.NormalizeNameComplete.Contains(keys.NormalizeName()));

            
            return filter;
        }

    }
}
