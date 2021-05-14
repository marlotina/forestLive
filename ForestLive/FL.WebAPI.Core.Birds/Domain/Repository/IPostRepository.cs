using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<BirdPost> GetPostAsync(Guid postId);

        Task<List<PostDto>> GetPostsAsync(string orderBy);
    }
}
