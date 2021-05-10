﻿using FL.Web.API.Core.User.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Contracts
{
    public interface IFollowService
    {
        Task<FollowUser> AddFollow(FollowUser followUser, Guid userSystemId);

        Task<bool> DeleteFollow(string followId, string FollowUserId, string webUser, Guid userSystemId);

        Task<FollowUser> GetFollow(string userId, string followUserId);
    }
}
