﻿using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
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
        private readonly IServiceBusFollowTopicSender<FollowerUser> iServiceBusFollowTopicSender;

        public FollowService(
            IServiceBusFollowTopicSender<FollowerUser> iServiceBusFollowTopicSender,
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

                var followerRequest = new FollowerUser() {
                    Id = followUser.Id,
                    CreationDate = followUser.CreationDate,
                    Type = "follower",
                    UserId = followUser.FollowUserId,
                    FollowerUserId = followUser.UserId,
                };

                await this.iServiceBusFollowTopicSender.SendMessage(followerRequest, "createFollow");
            }

            return result;
        }

        public async Task<bool> DeleteFollow(DeleteFollowUserResquest followUser)
        {
            var result = await this.iFollowRepository.DeleteFollow(followUser.Id, followUser.UserId);

            if (result)
            {
                var followerRequest = new FollowerUser()
                {
                    Id = followUser.Id,
                    Type = "follower",
                    UserId = followUser.FollowUserId,
                    FollowerUserId = followUser.UserId,
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
