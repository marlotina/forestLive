using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<Item> AddBirdItem(Item birdItem);

        Task<bool> DeleteBirdPost(Guid idItem);

        Task<Item> GetBirdItem(Guid idItem);
    }
}
