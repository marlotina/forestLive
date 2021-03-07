﻿using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IPostService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost, Stream fileStream, string imageName);

        Task<bool> DeleteBirdPost(Guid birdPostId, string userId);

        Task<BirdPost> GetBirdPost(Guid birdPostId);

        Task<List<BirdComment>> GetCommentByPost(Guid postId);
    }
}