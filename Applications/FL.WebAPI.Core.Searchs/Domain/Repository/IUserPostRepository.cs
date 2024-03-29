﻿using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Domain.Repositories
{
    public interface IUserPostRepository
    {
        Task<List<PointPostDto>> GetMapPointsByUserAsync(string userId);

        Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type);

        Task<BirdPost> GetPostAsync(Guid birdPostId, string userId);
    }
}
