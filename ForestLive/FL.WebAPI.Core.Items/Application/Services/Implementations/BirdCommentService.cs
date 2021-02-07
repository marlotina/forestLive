﻿using FL.Logging.Implementation.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class BirdCommentService : IBirdCommentService
    {
        private readonly IBIrdPostRepository itemsRepository;
        private readonly Logger<BirdCommentService> logger;

        public BirdCommentService(
            IBIrdPostRepository itemsRepository,
            Logger<BirdCommentService> logger)
        {
            this.itemsRepository = itemsRepository;
            this.logger = logger;
        }

        public IBIrdPostRepository ItemsRepository => itemsRepository;

        public async Task<BirdComment> AddComment(BirdComment comment)
        {
            try
            {
                comment.Id = Guid.NewGuid();
                comment.CreateDate = DateTime.UtcNow;
                comment.Type = ItemHelper.COMMENT_TYPE;

                await this.itemsRepository.CreateCommentAsync(comment);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddComment");
            }

            return comment;
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
                this.logger.LogError(ex, "DeleteComment");
            }

            return false;
        }

        public async Task<List<BirdComment>> GetCommentByItem(Guid itemId)
        {
            try
            {
                var result = await this.itemsRepository.GetCommentsAsync(itemId);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetCommentByItem");
            }

            return new List<BirdComment>(); ;
        }
    }
}
