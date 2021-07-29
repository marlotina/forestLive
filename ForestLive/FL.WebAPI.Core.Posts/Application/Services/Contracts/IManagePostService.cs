using FL.WebAPI.Core.Posts.Api.Models.v1.Request;
using FL.WebAPI.Core.Posts.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Posts.Application.Services.Contracts
{
    public interface IManagePostService
    {
        Task<BirdPost> AddPost(BirdPost birdPost, string imageBytes, string imageName, bool isPost);

        Task<bool> DeletePost(Guid postId, string userId);

        Task<bool> UpdateSpeciePost(UpdateSpecieRequest request, string userId);
    }
}
