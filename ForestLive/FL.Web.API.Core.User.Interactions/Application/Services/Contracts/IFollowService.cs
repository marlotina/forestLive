using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Contracts
{
    public interface IFollowService
    {
        Task<FollowUser> AddFollow(FollowUser followUser, Guid userSystemId);

        Task<bool> DeleteFollow(string followUserId, string webUser, Guid userSystemId);

        Task<FollowUser> GetFollow(string userId, string followUserId);

        Task<List<FollowUser>> GetFollowByUserId(string userId);
    }
}
