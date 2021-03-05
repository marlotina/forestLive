﻿using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class BirdPostService : IBirdPostService
    {
        private readonly IPostConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IBirdPostRepository itemsRepository;
        private readonly ILogger<BirdPostService> logger;
        private readonly IServiceBusPostTopicSender<BirdPost> serviceBusCreatedPostTopic;

        public BirdPostService(IPostConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IBirdPostRepository itemsRepository,
            IServiceBusPostTopicSender<BirdPost> serviceBusCreatedPostTopic,
            ILogger<BirdPostService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.itemsRepository = itemsRepository;
            this.serviceBusCreatedPostTopic = serviceBusCreatedPostTopic;
            this.logger = logger;
        }

        public async Task<BirdPost> AddBirdPost(BirdPost birdPost, Stream imageStream, string imageName)
        {
            try
            {
                var folder = birdPost.UserId + "/" + DateTime.Now.ToString("ddMMyyyhhmm");
                var result = await this.blobContainerRepository.UploadFileToStorage(imageStream, imageName, this.itemConfiguration.BirdPhotoContainer, folder);

                if (result)
                {
                    var postId = Guid.NewGuid();
                    birdPost.PostId = postId;
                    birdPost.Id = postId;
                    birdPost.Type = ItemHelper.POST_TYPE;
                    birdPost.VoteCount = 0;
                    birdPost.CommentCount = 0;
                    birdPost.CreateDate = DateTime.UtcNow;
                    birdPost.ImageUrl = folder + "/"+ imageName;
                    birdPost.VoteCount = 0;

                    if (birdPost.SpecieId == null || birdPost.SpecieId == Guid.Empty) {
                        birdPost.SpecieId = Guid.Parse(StatusSpecie.NoSpecieId);
                        birdPost.SpecieName = string.Empty;
                    }

                    var post = await this.itemsRepository.CreatePostAsync(birdPost);
                    await this.serviceBusCreatedPostTopic.SendMessage(birdPost, TopicHelper.LABEL_POST_CREATED);

                    return post;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeleteBirdPost(Guid birdPostId, string userId)
        {
            try
            {
                var post = await this.itemsRepository.GetPostAsync(birdPostId);
                if (userId == post.UserId)
                {
                    var image = post.ImageUrl;
                    var partitionKey = post.PostId.ToString();
                    var id = post.Id;
                    var userPartitionKey = post.UserId;
                    var result = await this.blobContainerRepository.DeleteFileToStorage(image, this.itemConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        await this.itemsRepository.DeletePostAsync(id, partitionKey);
                        await this.serviceBusCreatedPostTopic.SendMessage(post, TopicHelper.LABEL_POST_DELETED);
                    }

                    return true;
                }
                else 
                {
                    throw new UnauthorizedRemove();
                }

                return false;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteBirdItem");
            }

            return false;
        }

        public async Task<BirdPost> GetBirdPost(Guid birdPostId)
        {
            try
            {
                return await this.itemsRepository.GetPostAsync(birdPostId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBirdItem");
            }

            return null;
        }
    }
}
