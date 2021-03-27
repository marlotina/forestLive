using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Post.Interactions.Application.Exceptions;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Domain.Enum;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using FL.Web.API.Core.Post.Interactions.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository iCommentRepository;
        private readonly ILogger<CommentService> iLogger;
        private readonly IServiceBusCommentTopicSender<BirdCommentDto> iServiceBusCommentTopicSender;

        public CommentService(
            ICommentRepository iCommentRepository,
            IServiceBusCommentTopicSender<BirdCommentDto> iServiceBusCommentTopicSender,
            ILogger<CommentService> iLogger)
        {
            this.iCommentRepository = iCommentRepository;
            this.iServiceBusCommentTopicSender = iServiceBusCommentTopicSender;
            this.iLogger = iLogger;
        }

        public async Task<BirdComment> AddComment(BirdComment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreationDate = DateTime.UtcNow;
                comment.Type = ItemHelper.COMMENT_TYPE;

                var response = await this.iCommentRepository.CreateCommentAsync(comment);

                var message = this.Convert(comment);
                await this.iServiceBusCommentTopicSender.SendMessage(message, TopicHelper.LABEL_COMMENT_CREATED);

                return response;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "AddComment");
            }

            return null;
        }


        public async Task<bool> DeleteComment(Guid commentId, Guid postId, string userId)
        {
            try
            {
                var comment = await this.iCommentRepository.GetCommentAsync(commentId, postId);
                if (userId == comment.UserId && comment != null)
                {
                    var result = await this.iCommentRepository.DeleteCommentAsync(commentId, postId);

                    if (result)
                    {
                        var message = this.Convert(comment);
                        await this.iServiceBusCommentTopicSender.SendMessage(message, TopicHelper.LABEL_COMMENT_DELETED);
                        return true;
                    }
                }
                else
                {
                    throw new UnauthorizedRemove();
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "DeleteComment");
            }

            return false;
        }

        public async Task<List<BirdComment>> GetCommentByPost(Guid postId)
        {
            return await this.iCommentRepository.GetCommentsByPostIdAsync(postId);
        }

        private BirdCommentDto Convert(BirdComment source)
        {
            var result = default(BirdCommentDto);
            if (source != null)
            {
                result = new BirdCommentDto()
                {
                    PostId = source.PostId,
                    SpecieId = source.SpecieId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreationDate = source.CreationDate,
                    Type = source.Type,
                    Text = source.Text
                };
            }
            return result;
        }
    }
}
