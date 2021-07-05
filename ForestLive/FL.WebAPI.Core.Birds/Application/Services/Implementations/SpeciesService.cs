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
        private readonly IUserInfoService iUserInfoService;

        public SpeciesService(
            ISpeciesRepository iBirdSpeciesRepository,
            IUserInfoService iUserInfoService)
        {
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
            this.iUserInfoService = iUserInfoService;
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
