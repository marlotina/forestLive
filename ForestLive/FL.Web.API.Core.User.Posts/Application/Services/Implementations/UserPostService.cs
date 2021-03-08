using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using FL.WebAPI.Core.User.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using FL.WebAPI.Core.User.Posts.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.User.Posts.Application.Services.Implementations
{
    public class UserPostService : IUserPostService
    {
        private readonly IBirdUserRepository userRepository;
        private readonly ILogger<UserPostService> logger;
        private readonly IUserVotesRepository userVotesRepository;

        public UserPostService(
            IBirdUserRepository userRepository,
            IUserVotesRepository userVotesRepository,
            ILogger<UserPostService> logger)
        {
            this.userVotesRepository = userVotesRepository;
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId)
        {
            try 
            {
                var posts = await this.userRepository.GetPostsForUserId(userId);

                return posts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }

        public async Task<IEnumerable<Guid>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId)
        {
            try
            {
                if (webUserId != null)
                {
                    return await this.userVotesRepository.GetUserVoteByPosts(listPost, webUserId);
                }

                return new List<Guid>();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
                return null;
            }
        }
    }
}
