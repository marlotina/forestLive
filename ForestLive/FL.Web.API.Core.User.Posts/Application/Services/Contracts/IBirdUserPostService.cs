﻿using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Contracts
{
    public interface IBirdUserPostService
    {
        Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId);
    }
}