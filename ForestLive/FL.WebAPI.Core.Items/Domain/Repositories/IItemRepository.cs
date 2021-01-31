using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<BirdItem> AddBirdItem(BirdItem birdItem);

        Task<bool> DeleteBirdPost(Guid idItem);

        Task<BirdItem> GetBirdItem(Guid idItem);
    }
}
