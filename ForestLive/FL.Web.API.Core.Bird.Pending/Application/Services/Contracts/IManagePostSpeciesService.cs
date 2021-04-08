using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Request;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Contracts
{
    public interface IManagePostSpeciesService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost, byte[] imageBytes, string imageName, bool isPost);

        Task<bool> DeleteBirdPost(Guid postId, string userId);

        Task<BirdPost> AssingSpecieToPost(AssignSpecieRequest request, string userId);
    }
}
