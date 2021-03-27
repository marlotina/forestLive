using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository iPostRepository;
        private readonly ILogger<PostService> iLogger;
        private readonly IUserVotesRepository iUserVotesRepository;

        public PostService(
            IPostRepository iPostRepository,
            IUserVotesRepository iUserVotesRepository,
            ILogger<PostService> iLogger)
        {
            this.iUserVotesRepository = iUserVotesRepository;
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

        public async Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId)
        {
            try
            {
                if (webUserId != null)
                {
                    return await this.iUserVotesRepository.GetUserVoteByPosts(listPost, webUserId);
                }

                return new List<VotePostResponse>();
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBlogPostsForUserId");
                return null;
            }
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
