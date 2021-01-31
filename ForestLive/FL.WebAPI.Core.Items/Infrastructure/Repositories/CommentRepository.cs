using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.Services.Contracts;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IItemConfiguration itemConfiguration;

        public CommentRepository(IItemConfiguration itemConfiguration)
        {
            this.itemConfiguration = itemConfiguration;
        }

        public async Task<ItemComment> AddComment(ItemComment comment)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return comment;
        }

        public async Task<bool> DeleteComment(Guid idComment)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
            }

            return false;
        }
    }
}
