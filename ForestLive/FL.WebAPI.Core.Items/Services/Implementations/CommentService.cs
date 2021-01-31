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
        private readonly IItemsRepository itemsRepository;

        public CommentService(IItemsRepository itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        public IItemsRepository ItemsRepository => itemsRepository;

        public async Task<ItemComment> AddComment(ItemComment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreateDate = DateTime.UtcNow;
                comment.LikesCount = 0;
                comment.Type = ItemHelper.COMMENT_TYPE;

                await this.itemsRepository.CreateItemCommentAsync(comment);

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
