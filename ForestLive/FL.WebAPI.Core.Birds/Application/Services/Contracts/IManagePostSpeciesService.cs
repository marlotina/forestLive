using FL.WebAPI.Core.Birds.Api.Models.v1.Request;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IManagePostSpeciesService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost, byte[] imageBytes, string imageName, bool isPost);

        Task<bool> DeleteBirdPost(Guid postId, Guid specieId, string userId);

        Task<bool> UpdateSpecieToPost(UpdateSpecieRequest request, string userId);
    }
}
