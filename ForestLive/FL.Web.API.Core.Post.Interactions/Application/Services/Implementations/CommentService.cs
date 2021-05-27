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
        private readonly IServiceBusCommentTopicSender<CommentBaseDto> iServiceBusCommentTopicSender;

        public CommentService(
            ICommentRepository iCommentRepository,
            IServiceBusCommentTopicSender<CommentBaseDto> iServiceBusCommentTopicSender)
        {
            this.iCommentRepository = iCommentRepository;
            this.iServiceBusCommentTopicSender = iServiceBusCommentTopicSender;
        }

        public async Task<BirdComment> AddComment(CommentDto commentItem)
        {
            commentItem.Id = Guid.NewGuid();
            commentItem.CreationDate = DateTime.UtcNow;
            commentItem.Type = ItemHelper.COMMENT_TYPE;

            var comment = this.Convert(commentItem);
            var response = await this.iCommentRepository.CreateCommentAsync(comment);

            await this.iServiceBusCommentTopicSender.SendMessage(commentItem, TopicHelper.LABEL_COMMENT_CREATED);

            return response;
        }


        public async Task<bool> DeleteComment(Guid commentId, Guid postId, string userId)
        {
            var comment = await this.iCommentRepository.GetCommentAsync(commentId, postId);
            if (userId == comment.UserId && comment != null)
            {
                comment.UserId = null;
                comment.Text = string.Empty;
                comment.VoteCount = 0;
                var result = await this.iCommentRepository.DeleteCommentAsync(comment);

                if (result)
                {
                    var messageComment = this.Convert(comment);
                    await this.iServiceBusCommentTopicSender.SendMessage(messageComment, TopicHelper.LABEL_COMMENT_DELETED);
                    return true;
                }
            }
            else
            {
                throw new UnauthorizedRemove();
            }
            
            return false;
        }

        public async Task<List<BirdComment>> GetCommentByPost(Guid postId)
        {
            return await this.iCommentRepository.GetCommentsByPostIdAsync(postId);
        }

        public BirdComment Convert(CommentDto source)
        {
            var result = default(BirdComment);
            if (source != null)
            {
                result = new BirdComment()
                {
                    Id = source.Id,
                    PostId = source.PostId,
                    Text = source.Text,
                    Type = source.Type,
                    UserId = source.UserId,
                    ParentId = source.ParentId,
                    SpecieId = source.SpecieId,
                    AuthorPostId = source.AuthorPostId,
                    CreationDate = source.CreationDate,
                    VoteCount = 0
                };
            }
            return result;
        }

        public CommentDto Convert(BirdComment source)
        {
            var result = default(CommentDto);
            if (source != null)
            {
                result = new CommentDto()
                {
                    Id = source.Id,
                    PostId = source.PostId,
                    Text = source.Text,
                    Type = source.Type,
                    UserId = source.UserId,
                    ParentId = source.ParentId,
                    SpecieId = source.SpecieId,
                    AuthorPostId = source.AuthorPostId,
                    CreationDate = source.CreationDate
                };
            }
            return result;
        }
    }
}
