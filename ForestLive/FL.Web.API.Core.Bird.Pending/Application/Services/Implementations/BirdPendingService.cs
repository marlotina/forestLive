using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Implementations
{
    public class BirdPendingService : IBirdPendingService
    {
        private readonly IBirdPendingRepository iBirdPendingRepository;
        private readonly IUserVotesRestRepository iUserVotesRepository;

        public BirdPendingService(
            IUserVotesRestRepository iUserVotesRepository,
            IBirdPendingRepository iBirdPendingRepository)
        {
            this.iBirdPendingRepository = iBirdPendingRepository;
            this.iUserVotesRepository = iUserVotesRepository;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(int orderBy)
        {
            var orderCondition = this.GerOrderCondition(orderBy);
            return await this.iBirdPendingRepository.GetAllSpecieAsync(orderCondition);
        }

        public async Task<BirdPost> GetPost(Guid postId)
        {
            return await this.iBirdPendingRepository.GetPostsAsync(postId);
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
                //this.logger.LogError(ex, "GetBlogPostsForUserId");
                return null;
            }
        }

        public async Task<List<PostDto>> GetBirds(int orderBy)
        {
            return await this.iBirdPendingRepository.GetAllSpecieAsync(this.GerOrderCondition(orderBy));
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
