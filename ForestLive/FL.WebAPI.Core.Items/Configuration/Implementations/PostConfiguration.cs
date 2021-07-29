using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.Items.Configuration.Implementations
{
    public class PostConfiguration : IPostConfiguration
    {
        private readonly IConfiguration configuration;

        public PostConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string BirdPhotoContainer => this.configuration.GetSection("BirdPhotoContainer").Get<string>();
    }
}
