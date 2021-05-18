using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository iBirdSpeciesRepository;

        public SpeciesService(
            ISpeciesRepository iBirdSpeciesRepository)
        {
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy)
        {
            var orderCondition = this.GerOrderCondition(orderBy);
            return await this.iBirdSpeciesRepository.GetPostsBySpecieAsync(birdSpecieId, orderCondition);
        }

        public async Task<List<PostDto>> GetBirds(int orderBy)
        {
            return await this.iBirdSpeciesRepository.GetAllSpecieAsync(this.GerOrderCondition(orderBy));
        }

        public async Task<List<PostHomeDto>> GetLastBirds()
        {
            return await this.iBirdSpeciesRepository.GetLastSpecieAsync();
        }

        private string GerOrderCondition(int orderBy)
        {
            switch (orderBy)
            {
                case 1: return "creationDate DESC";
                case 2: return "voteCount DESC";
                case 3: return "commentCount DESC";
                default: return "creationDate DESC";
            }
        }
    }
}
