using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class BirdSpeciesService : IBirdSpeciesService
    {
        private readonly IBirdSpeciesRepository birdSpeciesRepository;
        private readonly IUserVotesRepository userVotesRepository;

        public BirdSpeciesService(
            IUserVotesRepository userVotesRepository,
            IBirdSpeciesRepository birdSpeciesRepository)
        {
            this.birdSpeciesRepository = birdSpeciesRepository;
            this.userVotesRepository = userVotesRepository;
        }

        public async Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy)
        {
            var orderCondition = this.GerOrderCondition(orderBy);
            return await this.birdSpeciesRepository.GetBirdsPostsBySpecieId(birdSpecieId.ToString(), orderCondition);
        }

        public async Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId)
        {
            try
            {
                if (webUserId != null)
                {
                    return await this.userVotesRepository.GetUserVoteByPosts(listPost, webUserId);
                }

                return new List<VotePostResponse>();
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "GetBlogPostsForUserId");
                return null;
            }
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
