using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IBirdPostService
    {
        Task<BirdPost> AddBirdItem(BirdPost birdItem, Stream fileStream, string imageName);

        Task<bool> DeleteBirdItem(Guid BirdItemId, Guid userId);

        Task<BirdPost> GetBirdItem(Guid itemId);
    }
}
