using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<bool> CreatePostAsync(BirdPost post);

        Task<bool> DeletePostAsync(Guid id, string partitionKey);

        Task<bool> UpdatePostAsync(BirdPost post);

        Task<BirdPost> GetPostAsync(Guid postId);
    }
}
