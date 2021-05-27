﻿using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repository
{
    public interface IUserPostRepository
    {
        Task<bool> CreatePostAsync(BirdPost post);

        Task<bool> DeletePostAsync(Guid postId, Guid specieId);
    }
}