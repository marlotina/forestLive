using FL.WebAPI.Core.UserLabels.Configuration.Contracts;
using FL.WebAPI.Core.UserLabels.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.WebAPI.Core.UserLabels.Configuration.Implementations
{
    public class UserLabelConfiguration : IUserLabelConfiguration
    {
        private readonly IConfiguration configuration;

        public UserLabelConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();
    }
}
