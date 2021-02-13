﻿using FL.WebAPI.Core.Items.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IBirdUserPostService
    {
        Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId);
    }
}
