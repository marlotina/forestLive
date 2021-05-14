using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository iPostRepository;
        private readonly ILogger<PostService> iLogger;

        public PostService(
            IPostRepository iPostRepository,
            ILogger<PostService> iLogger)
        {
            this.iPostRepository = iPostRepository;
            this.iLogger = iLogger;
        }

        public async Task<BirdPost> GetBirdPost(Guid birdPostId)
        {
            try
            {
                return await this.iPostRepository.GetPostAsync(birdPostId);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBirdItem");
            }

            return null;
        }

        public async Task<List<PostDto>> GetPosts(int orderBy)
        {
            try
            {
                var order = this.GerOrderCondition(orderBy);
                return await this.iPostRepository.GetPostsAsync(order);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBirdItem");
            }

            return null;
        }

        private string GerOrderCondition(int orderBy)
        {
            switch (orderBy)
            {
                case 1: return "creationDate DESC";
                case 2: return "voteCount DESC";
                case 3: return "commentCount DESC";
                default: return "creationDate DESC";
            }
        }
    }
}
