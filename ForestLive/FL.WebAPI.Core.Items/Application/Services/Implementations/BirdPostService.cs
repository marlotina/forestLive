using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.ServiceBus.Standard.Contracts;
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
        private readonly IBirdPostRepository itemsRepository;
        private readonly IBirdUserRepository userRepository;
        private readonly ILogger<BirdPostService> logger;
        private readonly IServiceBusTopicSender<BirdPost> serviceBusTopicSender;

        public BirdPostService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IBirdPostRepository itemsRepository,
            IBirdUserRepository userRepository,
            IServiceBusTopicSender<BirdPost> serviceBusTopicSender,
            ILogger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.itemsRepository = itemsRepository;
            this.userRepository = userRepository;
            this.serviceBusTopicSender = serviceBusTopicSender;
            this.logger = logger;
        }
        
        public async Task<BirdPost> AddBirdItem(BirdPost birdItem, Stream imageStream, string imageName)
        {
            try
            {
                var folder = birdItem.UserId + "/" + DateTime.Now.ToString("ddMMyyyhhmm");
                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, imageName, this.itemConfiguration.BirdPhotoContainer, folder);

                if (result)
                {
                    birdItem.PostId = Guid.NewGuid();
                    birdItem.Id = Guid.NewGuid();
                    birdItem.Type = ItemHelper.POST_TYPE;
                    birdItem.LikesCount = 0;
                    birdItem.CommentsCount = 0;
                    birdItem.CreateDate = DateTime.UtcNow;
                    birdItem.SpecieStatus = birdItem.SpecieId == null || birdItem.SpecieId == Guid.Empty ? StatusSpecie.NoSpecie : StatusSpecie.Pending;
                    birdItem.ImageUrl = folder + "/"+ imageName;
                    birdItem.VoteCount = 0;

                    var post = await this.itemsRepository.CreatePostAsync(birdItem);
                    await this.serviceBusTopicSender.SendMessage(birdItem);

                    return post;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeleteBirdItem(Guid itemId, string userId)
        {
            try
            {
                var item = await this.itemsRepository.GetPostAsync(itemId);
                if (userId == item.UserId) {
                    var image = item.ImageUrl;
                    var partitionKey = item.PostId.ToString();
                    var id = item.Id;
                    var userPartitionKey = item.UserId;
                    var result = await this.blobContainerRepository.DeleteFileToStorage(image, this.itemConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        await this.itemsRepository.DeletePostAsync(id, partitionKey);
                        await this.userRepository.DeleteItemAsync(id, userPartitionKey);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteBirdItem");
            }

            return false;
        }

        public async Task<BirdPost> GetBirdItem(Guid itemId)
        {
            try
            {
                return await this.itemsRepository.GetPostAsync(itemId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBirdItem");
            }

            return null;
        }
    }
}
