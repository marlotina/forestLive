using FL.WebAPI.Core.Searchs.Configuration.Contracts;
using FL.WebAPI.Core.Searchs.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.Searchs.Configuration.Implementations
{
    public class BirdsConfiguration : IBirdsConfiguration
    {
        private readonly IConfiguration configuration;

        public BirdsConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

        public string VoteApiDomain => this.configuration.GetSection("VoteApiDomain").Get<string>();

        public string VoteUrlService => this.configuration.GetSection("VoteUrlService").Get<string>();

        public string UserApiDomain => this.configuration.GetSection("UserApiDomain").Get<string>();

        public string UserUrlService => this.configuration.GetSection("UserUrlService").Get<string>();

        public string SpecieApiDomain => this.configuration.GetSection("SpecieApiDomain").Get<string>();

        public string SpecieUrlService => this.configuration.GetSection("SpecieUrlService").Get<string>();

        public string SpecieLanguageUrlService => this.configuration.GetSection("SpecieLanguageUrlService").Get<string>();
    }
}
