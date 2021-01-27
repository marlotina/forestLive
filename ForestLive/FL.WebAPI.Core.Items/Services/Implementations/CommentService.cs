using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public Task<Comment> AddComment(Comment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreateDate = DateTime.UtcNow;
                comment.LikesCount = 0;
                comment.Type = ItemHelper.COMMENT_TYPE;

                var result = this.commentRepository.AddComment(comment);

                return result;

            }
            catch (Exception ex) 
            { 
            
            }

            return null;
        }

        public Task<bool> DeleteComment(Guid commnetId)
        {
            throw new NotImplementedException();
        }
    }
}
