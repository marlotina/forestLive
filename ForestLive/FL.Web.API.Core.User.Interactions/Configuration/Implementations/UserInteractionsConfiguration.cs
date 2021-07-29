using FL.Web.API.Core.User.Interactions.Configuration.Contracts;
using FL.Web.API.Core.User.Interactions.Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace FL.Web.API.Core.User.Interactions.Configuration.Implementations
{
    public class UserInteractionsConfiguration : IUserInteractionsConfiguration
    {
        private readonly IConfiguration configuration;

        public UserInteractionsConfiguration(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public CosmosConfiguration CosmosConfiguration => this.configuration.GetSection("CosmosConfiguration").Get<CosmosConfiguration>();

    }
}
