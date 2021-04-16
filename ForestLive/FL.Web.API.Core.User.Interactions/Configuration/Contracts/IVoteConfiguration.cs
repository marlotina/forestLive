using FL.Web.API.Core.User.Interactions.Configuration.Models;

namespace FL.Web.API.Core.User.Interactions.Configuration.Contracts
{
    public interface IVoteConfiguration
    {
        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get; }
    }
}
