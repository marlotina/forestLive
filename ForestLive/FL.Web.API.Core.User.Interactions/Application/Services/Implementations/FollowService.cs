using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository iFollowRepository;
        private readonly IServiceBusFollowerTopicSender<UserFollowerDto> iServiceBusFollowerTopicSender;

        public FollowService(
            IServiceBusFollowerTopicSender<UserFollowerDto> iServiceBusFollowerTopicSender,
            IFollowRepository iFollowRepository)
        {
            this.iServiceBusFollowerTopicSender = iServiceBusFollowerTopicSender;
            this.iFollowRepository = iFollowRepository;
        }

        public async Task<FollowUser> AddFollow(FollowUser followUser, Guid userSystemId)
        {
            followUser.Id = $"{followUser.UserId}_{followUser.FollowUserId}";
            followUser.CreationDate = DateTime.UtcNow;
            followUser.Type = "follow";

            var result = await this.iFollowRepository.AddFollow(followUser);

            if (result != null) {

                var followerUser = new UserFollowerDto() {
                    Id = followUser.Id,
                    CreationDate = followUser.CreationDate,
                    Type = "follower",
                    UserId = followUser.UserId,
                    FollowUserId = followUser.FollowUserId,
                    SystemUserId = userSystemId
                };

                await this.iServiceBusFollowerTopicSender.SendMessage(followerUser, "CreateFollow");
            }

            return result;
        }

        public async Task<bool> DeleteFollow(string followUserId, string webUser, Guid userSystemId)
        {
            var follow = await this.iFollowRepository.GetFollow(webUser, followUserId);
            if (follow != null && follow.UserId == webUser) 
            {
                if (await this.iFollowRepository.DeleteFollow(follow.Id, webUser))
                {
                    var followerUser = new UserFollowerDto()
                    {
                        Id = follow.Id,
                        Type = "follower",
                        UserId = follow.UserId,
                        FollowUserId = follow.FollowUserId,
                        SystemUserId = userSystemId
                    };

                    await this.iServiceBusFollowerTopicSender.SendMessage(followerUser, "CreateFollow");
                    return true;
                }
            }            

            return false;
        }

        public async Task<FollowUser> GetFollow(string userId, string followUserId)
        {
            return  await this.iFollowRepository.GetFollow(userId, followUserId);
        }
    }
}
