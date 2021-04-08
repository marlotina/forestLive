using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Domain.Repository
{
    public interface IBirdPendingRepository
    {
        Task<BirdPost> GetPostsAsync(Guid postId);

        Task<BirdPost> CreatePostAsync(BirdPost post);

        Task DeletePostAsync(Guid postId);

        Task<BirdPost> UpdatePostAsync(BirdPost post);

        Task<List<PostDto>> GetAllSpecieAsync(string orderCondition);
    }
}
