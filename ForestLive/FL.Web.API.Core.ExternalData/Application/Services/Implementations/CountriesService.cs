using FL.Cache.Standard.Contracts;
using FL.Web.API.Core.ExternalData.Application.Services.Contracts;
using FL.Web.API.Core.ExternalData.Domain.Dto;
using FL.Web.API.Core.ExternalData.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Application.Services.Implementations
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository iCountriesRepository;
        private readonly ICustomMemoryCache<IEnumerable<CountryItem>> iCustomMemoryCache;

        public CountriesService(
            ICountriesRepository iCountriesRepository,
            ICustomMemoryCache<IEnumerable<CountryItem>> iCustomMemoryCache)
        {
            this.iCountriesRepository = iCountriesRepository;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<IEnumerable<CountryItem>> GetCountryByLanguage(Guid languageId)
        {
            var itemCache = this.iCustomMemoryCache.Get(languageId);

            if (itemCache == null || !itemCache.Any()) {
                itemCache = await this.iCountriesRepository.GetCountryByLanguage(languageId);

                this.iCustomMemoryCache.Add(languageId, itemCache);
            }
            
            return itemCache;
        }
    }
}
