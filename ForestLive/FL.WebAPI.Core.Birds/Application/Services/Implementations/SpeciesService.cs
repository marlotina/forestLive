using FL.Cache.Standard.Contracts;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using FL.WebAPI.Core.Birds.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ISpeciesRepository iBirdSpeciesRepository;
        private readonly IUserInfoService iUserInfoService;
        private readonly ISpecieRestRepository iSpecieRestRepository;
        private readonly ICustomMemoryCache<IEnumerable<SpecieResponse>> iCustomMemoryCache;
        private const string CACHE_SPECIEID = "CACHE_SPECIES";

        public SpeciesService(
            ISpecieRestRepository iSpecieRestRepository,
            ICustomMemoryCache<IEnumerable<SpecieResponse>> iCustomMemoryCache,
            ISpeciesRepository iBirdSpeciesRepository,

            IUserInfoService iUserInfoService)
        {
            this.iSpecieRestRepository = iSpecieRestRepository;
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
            this.iUserInfoService = iUserInfoService;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy)
        {
            var orderCondition = this.GerOrderCondition(orderBy);

            var result = await this.iBirdSpeciesRepository.GetPostsBySpecieAsync(birdSpecieId, orderCondition);

            foreach (var post in result)
            {
                post.UserImage = await this.iUserInfoService.GetUserImageById(post.UserId);
            }

            return result;
        }

        public async Task<List<PostDto>> GetBirdBySpecieName(string urlSpecie, int orderBy)
        {
            var itemCache = this.iCustomMemoryCache.Get(CACHE_SPECIEID);

            if (itemCache == null || !itemCache.Any())
            {
                itemCache = await this.iSpecieRestRepository.GetAllSpecies();
                this.iCustomMemoryCache.Add(CACHE_SPECIEID, itemCache);
            }

            var filter = itemCache.FirstOrDefault(x => x.UrlSpecie == urlSpecie);

            return await this.GetBirdBySpecie(filter.SpecieId, orderBy);
        }

        public async Task<List<PostDto>> GetBirds(int orderBy)
        {
            var result = await this.iBirdSpeciesRepository.GetAllSpecieAsync(this.GerOrderCondition(orderBy));

            foreach (var post in result) 
            {
                post.UserImage = await this.iUserInfoService.GetUserImageById(post.UserId);
            }

            return result;
        }

        public async Task<List<PostHomeDto>> GetLastBirds()
        {
            var result = await this.iBirdSpeciesRepository.GetLastSpecieAsync();

            foreach (var post in result)
            {
                post.UserImage = await this.iUserInfoService.GetUserImageById(post.UserId);
            }

            return result;
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
