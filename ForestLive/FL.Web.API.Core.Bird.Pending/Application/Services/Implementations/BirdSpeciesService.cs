using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Implementations
{
    public class BirdSpeciesService : IBirdSpeciesService
    {
        private readonly IBirdSpeciesRepository iBirdSpeciesRepository;
        private readonly IUserVotesRestRepository iUserVotesRepository;

        public BirdSpeciesService(
            IUserVotesRestRepository iUserVotesRepository,
            IBirdSpeciesRepository iBirdSpeciesRepository)
        {
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
            this.iUserVotesRepository = iUserVotesRepository;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy)
        {
            var orderCondition = this.GerOrderCondition(orderBy);
            return await this.iBirdSpeciesRepository.GetPostsBySpecieAsync(birdSpecieId, orderCondition);
        }

        public async Task<BirdPost> GetPost(Guid postId, Guid specieId)
        {
            return await this.iBirdSpeciesRepository.GetPostsAsync(postId, specieId);
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
            return await this.iBirdSpeciesRepository.GetAllSpecieAsync(this.GerOrderCondition(orderBy));
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
