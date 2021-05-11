using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IManagePostService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost, string imageBytes, string imageName, bool isPost);

        Task<bool> DeleteBirdPost(Guid birdPostId, string userId);

        Task<bool> UpdateSpecieToPost(UpdateSpecieRequest request, string userId);
    }
}
