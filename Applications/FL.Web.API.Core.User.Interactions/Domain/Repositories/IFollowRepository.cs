﻿using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface IFollowRepository
    {
        Task<Follow> AddFollow(Follow followUser);

        Task<bool> DeleteFollow(string followId, string userId);

        Task<FollowUser> GetFollow(string userId, string followUserId);

        Task<List<FollowUser>> GetFollowByUserId(string userId);
    }
}
