﻿using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface IFollowRepository
    {
        Task<FollowUser> AddFollow(FollowUser followUser);

        Task<bool> DeleteFollow(string followId, string userId);

        Task<FollowUser> GetFollow(string userId, string followUserId);
    }
}
