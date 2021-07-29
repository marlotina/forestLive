using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IManagePostService
    {
        Task<BirdPost> AddPost(BirdPost birdPost, string imageBytes, string imageName, bool isPost);

        Task<bool> DeletePost(Guid birdPostId, string userId);

        Task<bool> UpdateSpeciePost(UpdateSpecieRequest request, string userId);
    }
}
