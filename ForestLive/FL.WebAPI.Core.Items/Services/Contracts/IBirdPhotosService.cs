using FL.WebAPI.Core.Items.Domain.Entities.Items;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface IBirdPhotosService
    {
        Task<bool> AddBirdInfo(Item birdPhoto);

        Task<bool> UpdateBirdPhoto(Stream fileStream, string fileName);
    }
}
