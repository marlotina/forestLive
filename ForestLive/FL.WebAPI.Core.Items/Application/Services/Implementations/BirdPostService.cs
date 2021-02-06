using FL.Infrastructure.Implementations.Domain.Repository;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class BirdPostService : IBirdPostService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IBIrdPostRepository itemsRepository;
        private readonly IBirdUserRepository userRepository;
        //private readonly Logger<BirdPostService> logger;
        public BirdPostService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IBIrdPostRepository itemsRepository,
            IBirdUserRepository userRepository)
            //Logger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.itemsRepository = itemsRepository;
            this.userRepository = userRepository;
            //this.logger = logger;
        }
        
        public async Task<BirdPost> AddBirdItem(BirdPost birdItem, Stream imageStream, string imageName)
        {
            try
            {
                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, imageName, this.itemConfiguration.BirdPhotoContainer, birdItem.UserId);
                
                if (result) {
                    birdItem.ItemId = Guid.NewGuid();
                    birdItem.Id = Guid.NewGuid();
                    birdItem.Type = ItemHelper.POST_TYPE;
                    birdItem.LikesCount = 0;
                    birdItem.CommentsCount = 0;
                    birdItem.CreateDate = DateTime.UtcNow;
                    birdItem.SpecieConfirmed = false;
                    birdItem.ImageUrl = birdItem.UserId + "/" + imageName;

                    await this.itemsRepository.CreateItemAsync(birdItem);
                    await this.userRepository.CreateItemAsync(birdItem);
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
                return null;
            }

            return birdItem;
        }

        public async Task<bool> DeleteBirdItem(Guid itemId, Guid userId)
        {
            try
            {
                var item = await this.itemsRepository.GetItemAsync(itemId);
                var image = item.ImageUrl;
                var partitionKey = item.ItemId.ToString();
                var id = item.Id;
                var userPartitionKey = item.UserId;
                var result = await this.blobContainerRepository.DeleteFileToStorage(image, this.itemConfiguration.BirdPhotoContainer);

                if (result)
                {
                    await this.itemsRepository.DeleteItemAsync(id, partitionKey);
                    await this.userRepository.DeleteItemAsync(id, userPartitionKey);
                }

                return true;
            }
            catch (Exception ex) 
            { 
            
            }

            return false;
        }

        public async Task<BirdPost> GetBirdItem(Guid itemId)
        {
            try
            {
                return await this.itemsRepository.GetItemAsync(itemId);
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
