using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Contracts
{
    public interface IUserPostService
    {
        Task<BirdPost> GetPost(Guid birdPostId, string userId);
        
        Task<IEnumerable<PostDto>> GetUserPosts(string userId, string label, string type, Guid langugeId);

        Task<IEnumerable<PointPostDto>> GetMapPointsByUserId(string userId);
    }
}     