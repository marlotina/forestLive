using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class VoteCommentService : IVoteCommentService
    {
        private readonly IVoteCommentRepository voteCommentRepository;

        public VoteCommentService(
            IVoteCommentRepository voteCommentRepository)
        {
            this.voteCommentRepository = voteCommentRepository;
        }

        public async Task<List<VoteCommentPost>> GetCommentVotesByUserId(string userId)
        {
            return await this.voteCommentRepository.GetVotesByUserId(userId);
        }
    }
}
