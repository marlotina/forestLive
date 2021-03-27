using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<IEnumerable<PostDto>> GetUserBirds(string userId, Guid? specieId);

        Task<IEnumerable<PostDto>> GetUserPost(string label, string userId);

        Task<BirdPost> GetPostByPostId(Guid postId, string userId);

        Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId);

    }
}     