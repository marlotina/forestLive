using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using FL.WebAPI.Core.User.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
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

        public async Task<FollowUser> AddFollow(FollowUser followUser)
        {
            followUser.Type = "follow";
            followUser.CreationDate = DateTime.UtcNow;

            var result = await this.iFollowRepository.AddFollow(followUser);

            if (result != null) {

                var followerUser = new FollowerUser()
                {
                    Id = $"{followUser.FollowUserId}Follower{followUser.UserId}",
                    CreationDate = followUser.CreationDate,
                    Type = "follower",
                    UserId = followUser.FollowUserId,
                    FollowerUserId = followUser.UserId
                };

                await this.iFollowRepository.AddFollow(followerUser);

                var followerUserDto = new UserFollowerDto()
                {
                    UserId = followUser.FollowUserId
                };

                await this.iServiceBusFollowerTopicSender.SendMessage(followerUserDto, "createFollow");
            }

            return followUser;
        }

        public async Task<bool> DeleteFollow(string followUserId, string webUser)
        {
            var follow = await this.iFollowRepository.GetFollow(webUser, followUserId);
            if (follow != null && follow.UserId == webUser) 
            {
                if (await this.iFollowRepository.DeleteFollow(follow.Id, webUser))
                {
                    await this.iFollowRepository.DeleteFollow($"{follow.FollowUserId}Follower{follow.UserId}", follow.FollowUserId);
                    
                    var followerUser = new UserFollowerDto()
                    {
                        UserId = follow.FollowUserId
                    };

                    await this.iServiceBusFollowerTopicSender.SendMessage(followerUser, "deleteFollow");
                    return true;
                }
            }            

            return false;
        }

        public async Task<FollowUser> GetFollow(string userId, string followUserId)
        {
            return  await this.iFollowRepository.GetFollow(userId, followUserId);
        }

        public async Task<List<FollowUser>> GetFollowByUserId(string userId)
        {
            return await this.iFollowRepository.GetFollowByUserId(userId);
        }
    }
}
