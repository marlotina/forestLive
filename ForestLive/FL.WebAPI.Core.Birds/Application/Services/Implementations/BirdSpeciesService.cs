using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class BirdSpeciesService : IBirdSpeciesService
    {
        private readonly IBirdSpeciesRepository birdSpeciesRepository;

        public BirdSpeciesService(
            IBirdSpeciesRepository birdSpeciesRepository)
        {
            this.birdSpeciesRepository = birdSpeciesRepository;
        }

        public async Task<List<BirdPost>> GetBirdBySpecie(Guid birdSpecieId)
        {
            return await this.birdSpeciesRepository.GetBirdsPostsBySpecieId(birdSpecieId.ToString());
        }
    }
}
