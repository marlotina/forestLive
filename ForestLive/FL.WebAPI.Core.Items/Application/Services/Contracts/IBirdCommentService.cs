﻿using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IBirdCommentService
    {
        Task<BirdComment> AddComment(BirdComment commnet);

        Task<List<BirdComment>> GetCommentByItem(Guid itemId);

        Task<bool> DeleteComment(Guid commentId, Guid postId, string userId);
    }
}
