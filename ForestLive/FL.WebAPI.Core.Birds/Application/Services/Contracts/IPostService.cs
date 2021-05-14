using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IPostService
    {
        Task<BirdPost> GetBirdPost(Guid birdPostId);

        Task<List<PostDto>> GetPosts(int orderBy);
    }
}
