using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Comments.Application.Exceptions;
using FL.Web.API.Core.Comments.Application.Services.Contracts;
using FL.Web.API.Core.Comments.Domain.Dto;
using FL.Web.API.Core.Comments.Domain.Entities;
using FL.Web.API.Core.Comments.Domain.Enum;
using FL.Web.API.Core.Comments.Domain.Repositories;
using FL.Web.API.Core.Comments.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Comments.Application.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly ILogger<CommentService> logger;
        private readonly IServiceBusCommentTopicSender<BirdCommentDto> serviceBusCommentTopicSender;

        public CommentService(
            ICommentRepository commentRepository,
            IServiceBusCommentTopicSender<BirdCommentDto> serviceBusCommentTopicSender,
            ILogger<CommentService> logger)
        {
            this.commentRepository = commentRepository;
            this.serviceBusCommentTopicSender = serviceBusCommentTopicSender;
            this.logger = logger;
        }

        public async Task<BirdComment> AddComment(BirdComment comment, Guid specieId)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreateDate = DateTime.UtcNow;
                comment.Type = ItemHelper.COMMENT_TYPE;

                var response = await this.commentRepository.CreateCommentAsync(comment);

                var message = this.Convert(comment, specieId);
                await this.serviceBusCommentTopicSender.SendMessage(message, TopicHelper.LABEL_COMMENT_CREATED);

                return response;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddComment");
            }

            return null;
        }

        public async Task<List<BirdComment>> GetCommentByUser(string userId)
        {
            try
            {
                return await this.commentRepository.GetCommentsAsync(userId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetCommentByItem");
            }

            return new List<BirdComment>();
        }

        public async Task<bool> DeleteComment(Guid commentId, string userId, Guid specieId)
        {
            try
            {
                var comment = await this.commentRepository.GetCommentAsync(commentId, userId);
                if (userId == comment.UserId && comment != null)
                {
                    var result = await this.commentRepository.DeleteCommentAsync(commentId, userId);

                    if (result)
                    {
                        var message = this.Convert(comment, specieId);
                        await this.serviceBusCommentTopicSender.SendMessage(message, TopicHelper.LABEL_COMMENT_DELETED);
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
                this.logger.LogError(ex, "DeleteComment");
            }

            return false;
        }

        public Task<List<BirdComment>> GetCommentByPost(string userId)
        {
            throw new NotImplementedException();
        }

        private BirdCommentDto Convert(BirdComment source, Guid specieId)
        {
            var result = default(BirdCommentDto);
            if (source != null)
            {
                result = new BirdCommentDto()
                {
                    PostId = source.PostId,
                    SpecieId = specieId,
                    UserId = source.UserId,
                    Id = source.Id,
                    CreateDate = source.CreateDate,
                    Type = source.Type,
                    Text = source.Text
                };
            }
            return result;

        }
    }
}
