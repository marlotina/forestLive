﻿using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repositories
{
    public interface IUserPostRepository
    {
        Task<List<PointPostDto>> GetMapPointsByUserAsync(string userId);

        Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type);

    }
}