using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class UserVoteService : IUserVoteService
    {
        private readonly IUserVotesRestRepository userVotesRepository;

        public UserVoteService(
            IUserVotesRestRepository userVotesRepository)
        {
            this.userVotesRepository = userVotesRepository;
        }

        public async Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId)
        {
            return await this.userVotesRepository.GetUserVoteByPosts(listPost, webUserId);
              
        }
    }
}
