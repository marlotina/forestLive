using FL.Web.API.Core.User.Interactions.Domain.Dto;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Contracts
{
    public interface IVoteCommentService
    {
        Task<List<VoteCommentPost>> GetCommentVotesByUserId(string userId);
    }
}
