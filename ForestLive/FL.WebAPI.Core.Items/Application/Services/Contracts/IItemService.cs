using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IItemService
    {
        Task<Item> AddBirdItem(Item birdItem, Stream fileStream);

        Task<bool> DeleteBirdItem(Guid BirdItemId, Guid userId);

        Task<Item> GetBirdItem(Guid itemId);
    }
}
