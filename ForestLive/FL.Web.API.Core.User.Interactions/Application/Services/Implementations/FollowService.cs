using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using FL.Web.API.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository iFollowRepository;
        private readonly IServiceBusFollowTopicSender<UserFollowDto> iServiceBusFollowTopicSender;

        public FollowService(
            IServiceBusFollowTopicSender<UserFollowDto> iServiceBusFollowTopicSender,
            IFollowRepository iFollowRepository)
        {
            this.iServiceBusFollowTopicSender = iServiceBusFollowTopicSender;
            this.iFollowRepository = iFollowRepository;
        }

        public async Task<FollowUser> AddFollow(FollowUser followUser, Guid userSystemId)
        {
            followUser.Id = $"{followUser.UserId}_{followUser.FollowUserId}";
            followUser.CreationDate = DateTime.UtcNow;
            followUser.Type = "follow";

            var result = await this.iFollowRepository.AddFollow(followUser);

            if (result != null) {

                var followerRequest = new UserFollowDto() {
                    Id = followUser.Id,
                    CreationDate = followUser.CreationDate,
                    Type = "follower",
                    UserId = followUser.FollowUserId,
                    FollowUserId = followUser.UserId,
                    SystemUserId = userSystemId
                };

                await this.iServiceBusFollowTopicSender.SendMessage(followerRequest, "createFollow");
            }

            return result;
        }

        public async Task<bool> DeleteFollow(string followId, string followUserId, string webUser, Guid userSystemId)
        {
            var result = await this.iFollowRepository.DeleteFollow(followId, webUser);

            if (result)
            {
                var followerRequest = new UserFollowDto()
                {
                    Id = followId,
                    Type = "follower",
                    UserId = webUser,
                    FollowUserId = followUserId,
                    SystemUserId = userSystemId
                };
                
                await this.iServiceBusFollowTopicSender.SendMessage(followerRequest, "deleteFollow");
            }

            return result;
        }

        public async Task<FollowUser> GetFollow(string userId, string followUserId)
        {
            return  await this.iFollowRepository.GetFollow(userId, followUserId);
        }
    }
}
