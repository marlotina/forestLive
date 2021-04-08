using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface IBirdSpeciesRepository
    {
        Task<List<PostDto>> GetPostsBySpecieAsync(Guid specieId, string orderCondition);

        Task<BirdPost> GetPostsAsync(Guid postId, Guid specieId);

        Task<BirdPost> CreatePostAsync(BirdPost post);

        Task<bool> DeletePostAsync(Guid postId, Guid specieId);

        Task<List<PostDto>> GetAllSpecieAsync(string orderCondition);

        Task<bool> UpdatePostAsync(BirdPost post);
    }
}
