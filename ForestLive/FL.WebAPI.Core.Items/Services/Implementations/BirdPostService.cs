using FL.Infrastructure.Implementations.Domain.Repository;
using FL.Logging.Implementation.Standard;
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
    public class BirdPostService : IBirdPostService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IPostRepository postRepository;

        //private readonly Logger<BirdPostService> logger;
        public BirdPostService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IPostRepository postRepository)
            //Logger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.postRepository = postRepository;
            //this.logger = logger;
        }
        
        public Task<bool> DeleteBird(Guid BirdItemId)
        {
            throw new NotImplementedException();
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

                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, "", this.itemConfiguration.BirthPhotoContainer, birdPost.UserId.ToString());
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
