using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface IBirdPhotosService
    {
        Task<bool> AddBirdInfo(BirdPost birdPhoto);

        Task<bool> UpdateBirdPhoto(Stream fileStream, string fileName);

        Task<bool> DeleteBird(Guid BirdItemId);
    }
}
