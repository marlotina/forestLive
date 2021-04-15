using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository iFollowRepository;

        public FollowService(
            IFollowRepository iFollowRepository)
        {
            this.iFollowRepository = iFollowRepository;
        }

        public async Task<FollowUser> AddFollow(FollowUser followUser)
        {
            followUser.Id = $"{followUser.UserId}_{followUser.FollowUserId}";
            followUser.CreationDate = DateTime.UtcNow;
            followUser.Type = "follow";

            var result = await this.iFollowRepository.AddFollow(followUser);
            if (result != null) {
                result = await this.iFollowRepository.AddFollow(new FollowUser
                {
                    UserId = followUser.FollowUserId,
                    FollowUserId = followUser.UserId,
                    CreationDate = followUser.CreationDate,
                    Type = "follower",
                    Id = followUser.Id
                });
            }

            return result;
        }

        public async Task<bool> DeleteFollow(DeleteFollowUserResquest followUser)
        {
            var result = await this.iFollowRepository.DeleteFollow(followUser.Id, followUser.UserId);
            if (result)
            {
                result = await this.iFollowRepository.DeleteFollow(followUser.Id, followUser.FollowUserId);
            }

            return result;
        }
    }
}
