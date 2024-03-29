﻿using FL.Cache.Standard.Contracts;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Implementations
{
    public class SpecieInfoService : ISpecieInfoService
    {
        private readonly ISpecieRestRepository iSpecieRestRepository;
        private readonly ICustomMemoryCache<IEnumerable<SpecieResponse>> iCustomMemoryCache;
        private const string CACHE_SPECIE_LANGUAGE_ID = "CACHE_LANGUAGE_ID_";

        public SpecieInfoService(
            ICustomMemoryCache<IEnumerable<SpecieResponse>> iCustomMemoryCache,
            ISpecieRestRepository iSpecieRestRepository)
        {
            this.iSpecieRestRepository = iSpecieRestRepository;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<SpecieResponse> GetSpecieById(Guid specieId, Guid languageId)
        {
            var itemCache = this.iCustomMemoryCache.Get(CACHE_SPECIE_LANGUAGE_ID + languageId.ToString());

            if (itemCache == null || !itemCache.Any())
            {
                itemCache = await this.iSpecieRestRepository.GetAllSpeciesByLanguageId(languageId);
                this.iCustomMemoryCache.Add(CACHE_SPECIE_LANGUAGE_ID + languageId.ToString(), itemCache);
            }

            if (itemCache != null)
            {
                return itemCache.FirstOrDefault(x => x.SpecieId == specieId);
            }

            return null;
        }

    }
}
