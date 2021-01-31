using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Contracts
{
    public interface IItemService
    {
        Task<BirdItem> AddBirdItem(BirdItem birdItem, Stream fileStream);

        Task<bool> DeleteBirdItem(Guid BirdItemId, Guid userId);
    }
}
