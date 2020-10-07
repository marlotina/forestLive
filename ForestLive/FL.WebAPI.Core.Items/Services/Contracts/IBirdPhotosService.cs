using FL.WebAPI.Core.Items.Domain.Entities.Items;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface IBirdPhotosService
    {
        Task<bool> AddBirdPhoto(Item birdPhoto);

        Task<bool> UpdateBirdPhoto(Item birdPhoto);
    }
}
