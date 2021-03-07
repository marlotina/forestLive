using FL.WebAPI.Core.User.Posts.Configuration.Contracts;
using FL.WebAPI.Core.User.Posts.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.User.Posts.Configuration.Implementations
{
    public class UserPostConfiguration : IUserPostConfiguration
    {
        private readonly IConfiguration configuration;

        public UserPostConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();
    }
}
