using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Contracts
{
    public interface IFollowerService
    {

        Task<List<FollowerUser>> GetFollowerByUserId(string userId);
    }
}
