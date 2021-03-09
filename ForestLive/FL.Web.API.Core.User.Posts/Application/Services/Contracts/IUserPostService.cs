using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId);

        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);
    }
}
