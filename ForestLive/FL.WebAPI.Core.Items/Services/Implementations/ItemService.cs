using FL.Infrastructure.Implementations.Domain.Repository;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Services.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IItemRepository postRepository;

        //private readonly Logger<BirdPostService> logger;
        public ItemService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IItemRepository postRepository)
            //Logger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.postRepository = postRepository;
            //this.logger = logger;
        }
        
        public async Task<BirdItem> AddBirdItem(BirdItem birdItem, Stream imageStream)
        {
            try
            {
                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, "", this.itemConfiguration.BirdPhotoContainer, birdItem.UserId.ToString());

                if (result) {
                    birdItem.PostId = Guid.NewGuid();
                    birdItem.Id = Guid.NewGuid();
                    birdItem.Type = ItemHelper.POST_TYPE;
                    birdItem.LikesCount = 0;
                    birdItem.CommentsCount = 0;
                    birdItem.CreateDate = DateTime.UtcNow;
                    birdItem.SpecieConfirmed = false;

                    return await this.postRepository.AddBirdItem(birdItem);
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
                return null;
            }

            return null;
        }

        public async Task<bool> DeleteBirdItem(Guid birdItemId, Guid userId)
        {
            try
            {
                var fileName = "";

                var result = await this.blobContainerRepository.DeleteFileToStorage(fileName, this.itemConfiguration.BirdPhotoContainer);

                if (result)
                {
                    return await this.postRepository.DeleteBirdPost(birdItemId);
                }
            }
            catch (Exception ex) 
            { 
            
            }

            return false;
        }
    }
}
