﻿using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface IItemPostService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPhoto, Stream fileStream);

        Task<bool> DeleteBird(Guid BirdItemId);
    }
}
