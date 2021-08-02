using FL.Cache.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Extensions;
using FL.Pereza.Helpers.Standard.Language;
using FL.Web.API.Core.ExternalData.Application.Mappers.Contracts;
using FL.Web.API.Core.ExternalData.Application.Services.Contracts;
using FL.Web.API.Core.ExternalData.Domain.Dto;
using FL.Web.API.Core.ExternalData.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Application.Services.Implementations
{
    public class AutocompleteService : IAutocompleteService
    {
        private readonly ISpeciesRepository iSpeciesRepository;
        private readonly ISpecieCacheMapper iSpecieCacheMapper;
        private readonly ICustomMemoryCache<IEnumerable<SpeciesCacheItem>> iCustomMemoryCache;

        public AutocompleteService(
            ISpeciesRepository iSpeciesRepository,
            ISpecieCacheMapper iSpecieCacheMapper,
            ICustomMemoryCache<IEnumerable<SpeciesCacheItem>> iCustomMemoryCache)
        {
            this.iSpeciesRepository = iSpeciesRepository;
            this.iSpecieCacheMapper = iSpecieCacheMapper;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<IEnumerable<SpeciesCacheItem>> GetSpeciesByName(string keys, string languageCode)
        {
            var itemCache = await GetSpecies(languageCode);
            return itemCache.Where(x => x.NormalizeName.Contains(keys.NormalizeName()));
        }

        public async Task<IEnumerable<SpeciesCacheItem>> GetSpeciesByScienceName(string keys, string languageCode)
        {
            var itemCache = await GetSpecies(languageCode);
            return itemCache.Where(x => x.NormalizeScienceName.Contains(keys.NormalizeName()));
        }

        private async Task<IEnumerable<SpeciesCacheItem>> GetSpecies(string languageCode)
        {
            var languageId = LanguageHelper.GetLanguageByCode(languageCode);

            var itemCache = this.iCustomMemoryCache.Get(languageId);

            if (itemCache == null || !itemCache.Any())
            {
                var species = await this.iSpeciesRepository.GetSpeciesByLanguage(languageId);
                itemCache = species.Select(x => this.iSpecieCacheMapper.Convert(x));

                this.iCustomMemoryCache.Add(languageId, itemCache);
            }

            return itemCache;
        }
    }
}
