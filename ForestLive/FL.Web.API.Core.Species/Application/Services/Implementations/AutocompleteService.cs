using FL.Cache.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Extensions;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using FL.Web.API.Core.Species.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.Web.API.Core.Species.Application.Services.Implementations
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

        public IEnumerable<AutocompleteResponse> GetSpeciesByKeys(string keys, Guid languageId)
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
