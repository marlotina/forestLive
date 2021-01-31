using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlogCosmosDbService blogCosmosDbService;
        public ItemRepository(IItemConfiguration itemConfiguration,
            IBlogCosmosDbService blogCosmosDbService)
        {
            this.blogCosmosDbService = blogCosmosDbService;
            this.itemConfiguration = itemConfiguration;
        }

        public async Task<Item> AddBirdItem(Item birdItem)
        {
            try
            {
                await this.blogCosmosDbService.CreateItemAsync(birdItem);
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return birdItem;
        }

        public async Task<bool> DeleteBirdPost(Guid idItem)
        {
            try
            {
                
                return true;
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return false;
        }

        public Task<Item> GetBirdItem(Guid idItem)
        {
            throw new NotImplementedException();
        }
    }
}
