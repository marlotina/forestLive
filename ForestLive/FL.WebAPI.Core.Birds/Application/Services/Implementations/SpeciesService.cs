using FL.Cache.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Enums;
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
        private readonly ICustomMemoryCache<IEnumerable<SpecieInfoResponse>> iCustomMemoryCache;
        private readonly ISpecieInfoService iSpecieInfoService;
        private const string CACHE_SPECIEID = "CACHE_SPECIES";

        public SpeciesService(
            ISpecieRestRepository iSpecieRestRepository,
            ISpecieInfoService iSpecieInfoService,
            ICustomMemoryCache<IEnumerable<SpecieInfoResponse>> iCustomMemoryCache,
            ISpeciesRepository iBirdSpeciesRepository,

            IUserInfoService iUserInfoService)
        {
            this.iSpecieRestRepository = iSpecieRestRepository;
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
            this.iUserInfoService = iUserInfoService;
            this.iCustomMemoryCache = iCustomMemoryCache;
            this.iSpecieInfoService = iSpecieInfoService;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy, Guid? languageId)
        {
            var orderCondition = this.GerOrderCondition(orderBy);

            var result = await this.iBirdSpeciesRepository.GetPostsBySpecieAsync(birdSpecieId, orderCondition);

            var specieName = string.Empty;
            var specieUrl = string.Empty;

            if (birdSpecieId != Guid.Parse(StatusSpecie.NoSpecieId)) {
                var specie = await this.iSpecieInfoService.GetSpecieById(birdSpecieId, languageId.Value);
                if (specie != null)
                {
                    specieName = specie?.NameComplete;
                    specieUrl = specie?.UrlSpecie;
                }
            }

            foreach (var post in result)
            {
                post.SpecieName = specieName;
                post.SpecieUrl = specieUrl;
                post.UserImage = await this.iUserInfoService.GetUserImageById(post.UserId);
            }

            return result;
        }

        public async Task<List<PostDto>> GetBirdBySpecieName(string urlSpecie, int orderBy, Guid languageId)
        {
            var itemCache = this.iCustomMemoryCache.Get(CACHE_SPECIEID);

            if (itemCache == null || !itemCache.Any())
            {
                itemCache = await this.iSpecieRestRepository.GetAllSpecies();
                this.iCustomMemoryCache.Add(CACHE_SPECIEID, itemCache);
            }

            if (itemCache != null)
            {
                var filter = itemCache.FirstOrDefault(x => x.UrlSpecie == urlSpecie);
                return await this.GetBirdBySpecie(filter.SpecieId, orderBy, languageId);
            }

            return new List<PostDto>();
        }

        public async Task<List<PostDto>> GetBirds(int orderBy, Guid languageId)
        {
            var result = await this.iBirdSpeciesRepository.GetAllSpecieAsync(this.GerOrderCondition(orderBy));

            foreach (var post in result) 
            {
                if (post.SpecieId.HasValue)
                {
                    var specie = await this.iSpecieInfoService.GetSpecieById(post.SpecieId.Value, languageId);
                    if (specie != null)
                    {
                        post.SpecieName = specie?.NameComplete;
                        post.SpecieUrl = specie?.UrlSpecie;
                    }
                }

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
