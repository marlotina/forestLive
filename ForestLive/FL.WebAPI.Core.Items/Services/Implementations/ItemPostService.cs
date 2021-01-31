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
    public class ItemPostService : IItemPostService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IItemRepository postRepository;

        //private readonly Logger<BirdPostService> logger;
        public ItemPostService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IItemRepository postRepository)
            //Logger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.postRepository = postRepository;
            //this.logger = logger;
        }
        
        public async Task<bool> DeleteBird(Guid BirdItemId)
        {
            return await this.blobContainerRepository.DeleteFileToStorage("", "");
        }

        public async Task<BirdPost> AddBirdPost(BirdPost birdPost, Stream imageStream)
        {
            try
            {
                birdPost.PostId = Guid.NewGuid();
                birdPost.Id = Guid.NewGuid();
                birdPost.Type = ItemHelper.POST_TYPE;
                birdPost.LikesCount = 0;
                birdPost.CommentsCount = 0;
                birdPost.CreateDate = DateTime.UtcNow;

                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, "", this.itemConfiguration.BirdPhotoContainer, birdPost.UserId.ToString());
                if (result) {
                    return await this.postRepository.AddBirdPost(birdPost);
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
                return null;
            }

            return null;
        }
    }
}
