﻿using FL.Infrastructure.Implementations.Domain.Repository;
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
    public class ItemService : IItemService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IItemsRepository itemsRepository;
        private readonly IUserRepository userRepository;
        //private readonly Logger<BirdPostService> logger;
        public ItemService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IItemsRepository itemsRepository,
            IUserRepository userRepository)
            //Logger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.itemsRepository = itemsRepository;
            this.userRepository = userRepository;
            //this.logger = logger;
        }
        
        public async Task<Item> AddBirdItem(Item birdItem, Stream imageStream, string imageName)
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

        public async Task<bool> DeleteBirdItem(Guid birdItemId, Guid userId)
        {
            try
            {
                var fileName = "";

                var result = await this.blobContainerRepository.DeleteFileToStorage(fileName, this.itemConfiguration.BirdPhotoContainer);

                if (result)
                {
                    await this.itemsRepository.DeleteItemAsync(birdItemId);
                }

                return true;
            }
            catch (Exception ex) 
            { 
            
            }

            return false;
        }

        public async Task<Item> GetBirdItem(Guid itemId)
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