﻿using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IBirdUserPostService
    {
        Task CreateUserAsync(BirdUser user);

        Task<List<BirdPost>> GetBlogPostsForUserId(string userId);
    }
}