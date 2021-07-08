using FL.Cache.Standard.Contracts;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Application.Services.Contracts;
using FL.Web.API.Core.Species.Domain.Dto;
using FL.Web.API.Core.Species.Domain.Model;
using FL.Web.API.Core.Species.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Implementations
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository iSpeciesRepository;
        private readonly IAutocompleteMapper iAutocompleteMapper;
        private readonly ICustomMemoryCache<IEnumerable<SpeciesCacheItem>> iCustomMemoryCache;

        public SpeciesService(
            ISpeciesRepository iSpeciesRepository,
            IAutocompleteMapper iAutocompleteMapper,
            ICustomMemoryCache<IEnumerable<SpeciesCacheItem>> iCustomMemoryCache)
        {
            this.iSpeciesRepository = iSpeciesRepository;
            this.iAutocompleteMapper = iAutocompleteMapper;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<List<SpecieItem>> GetAllSpecies()
        {
            return await this.iSpeciesRepository.GetAllSpecies();
        }
    }
}
