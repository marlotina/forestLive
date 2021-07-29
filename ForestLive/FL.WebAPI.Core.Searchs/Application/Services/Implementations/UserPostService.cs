using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using FL.WebAPI.Core.Searchs.Domain.Repositories;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Implementations
{
    public class UserPostService : IUserPostService
    {
        private readonly IUserPostRepository iUserRepository;
        private readonly IUserInfoService iUserInfoService;
        private readonly ISpecieInfoService iSpecieInfoService;

        public UserPostService(
            IUserInfoService iUserInfoService,
            ISpecieInfoService iSpecieInfoService,
            IUserPostRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
            this.iUserInfoService = iUserInfoService;
            this.iSpecieInfoService = iSpecieInfoService;
        }

        public async Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId)
        {
            return await this.iUserRepository.GetMapPointsByUserAsync(userId);
        }

        public async Task<BirdPost> GetPost(Guid birdPostId, string userId)
        {
            return await this.iUserRepository.GetPostAsync(birdPostId, userId);
        }

        public async Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type, Guid langugeId)
        {
            var result = await this.iUserRepository.GetUserPosts(userId, label, type);

            foreach (var post in result)
            {
                if (post.SpecieId.HasValue)
                {
                    var specie = await this.iSpecieInfoService.GetSpecieById(post.SpecieId.Value, langugeId);
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
    }
}
