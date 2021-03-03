using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class BirdCommentService : IBirdCommentService
    {
        private readonly IBirdPostRepository itemsRepository;
        private readonly ILogger<BirdCommentService> logger;
        private readonly IServiceBusCommentTopicSender<BirdComment> serviceBusCommentTopicSender;

        public BirdCommentService(
            IBirdPostRepository itemsRepository,
            IServiceBusCommentTopicSender<BirdComment> serviceBusCommentTopicSender,
            ILogger<BirdCommentService> logger)
        {
            this.itemsRepository = itemsRepository;
            this.serviceBusCommentTopicSender = serviceBusCommentTopicSender;
            this.logger = logger;
        }

        public IBirdPostRepository ItemsRepository => itemsRepository;

        public async Task<BirdComment> AddComment(BirdComment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreateDate = DateTime.UtcNow;
                comment.Type = ItemHelper.COMMENT_TYPE;

                var response = await this.itemsRepository.CreateCommentAsync(comment);
                await this.serviceBusCommentTopicSender.SendMessage(comment, TopicHelper.LABEL_COMMENT_CREATED);

                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddComment");
            }

            return null;
        }

        public async Task<List<BirdComment>> GetCommentByPost(Guid postId)
        {
            try
            {
                return await this.itemsRepository.GetCommentsAsync(postId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetCommentByItem");
            }

            return new List<BirdComment>();
        }

        public async Task<bool> DeleteComment(Guid commentId, Guid postId, string userId)
        {
            try
            {
                var comment = await this.itemsRepository.GetCommentAsync(commentId, postId);
                if (userId == comment.UserId && comment != null)
                {
                    await this.itemsRepository.DeleteCommentAsync(commentId, postId);
                    await this.serviceBusCommentTopicSender.SendMessage(comment, TopicHelper.LABEL_COMMENT_DELETED);
                    return true;
                }
                else
                {
                    throw new UnauthorizedRemove();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteComment");
            }

            return false;
        }
    }
}
