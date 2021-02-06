using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
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
                comment.Type = ItemHelper.COMMENT_TYPE;

                await this.itemsRepository.CreateItemCommentAsync(comment);

            }
            catch (Exception ex) 
            { 
            
            }

            return comment;
        }

        public async Task<bool> DeleteComment(Guid commnetId)
        {
            try
            {
                await this.itemsRepository.DeleteCommentAsync(commnetId, new Guid());
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public async Task<bool> DeleteComment(Guid commnetId, Guid itemId)
        {
            try
            {
                await this.itemsRepository.DeleteCommentAsync(commnetId, itemId);
                return true;
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public async Task<List<ItemComment>> GetCommentByItem(Guid itemId)
        {
            try
            {
                var result = await this.itemsRepository.GetItemCommentsAsync(itemId);
                return result;
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
