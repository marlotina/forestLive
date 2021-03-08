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

        public async Task<IEnumerable<BirdPost>> GetPostsByUserId(string userId, string webUserId)
        {
            try 
            {
                var posts = await this.userRepository.GetPostsForUserId(userId);

                if (webUserId != null) {
                    var list = posts.Select(x => x.PostId).ToList();
                    var userVotes = await this.userVotesRepository.GetUserVoteByPosts(list, webUserId);
                }

                return posts;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
            }

            return null;
        }
    }
}
