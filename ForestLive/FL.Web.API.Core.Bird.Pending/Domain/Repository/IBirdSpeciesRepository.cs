using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Domain.Repository
{
    public interface IBirdSpeciesRepository
    {
        Task<List<PostDto>> GetPostsBySpecieAsync(Guid specieId, string orderCondition);

        Task<BirdPost> GetPostsAsync(Guid postId, Guid specieId);

        Task<BirdPost> CreatePostAsync(BirdPost post);

        Task DeletePostAsync(Guid postId, Guid specieId);

        Task<List<PostDto>> GetAllSpecieAsync(string orderCondition);
    }
}
