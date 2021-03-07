using FL.Web.Api.Core.Votes.Configuration.Models;

namespace FL.Web.Api.Core.Votes.Configuration.Contracts
{
    public interface IVoteConfiguration
    {
        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
