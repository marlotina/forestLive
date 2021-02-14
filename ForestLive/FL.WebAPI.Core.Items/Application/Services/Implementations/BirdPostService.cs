﻿using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
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
        private readonly IBIrdPostRepository itemsRepository;
        private readonly IBirdUserRepository userRepository;
        private readonly ILogger<BirdPostService> logger;
        private readonly IServiceBusTopicSender<BirdPost> serviceBusTopicSender;

        public BirdPostService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IBIrdPostRepository itemsRepository,
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
                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, imageName, this.itemConfiguration.BirdPhotoContainer, birdItem.UserId);

                if (result)
                {
                    birdItem.PostId = Guid.NewGuid();
                    birdItem.Id = Guid.NewGuid();
                    birdItem.Type = ItemHelper.POST_TYPE;
                    birdItem.LikesCount = 0;
                    birdItem.CommentsCount = 0;
                    birdItem.CreateDate = DateTime.UtcNow;
                    birdItem.SpecieConfirmed = false;
                    birdItem.ImageUrl = birdItem.UserId + "/" + imageName;

                    await this.itemsRepository.CreatePostAsync(birdItem);
                    await this.serviceBusTopicSender.SendMessage(birdItem);
                }

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddBirdItem");
                return null;
            }

            return birdItem;
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
