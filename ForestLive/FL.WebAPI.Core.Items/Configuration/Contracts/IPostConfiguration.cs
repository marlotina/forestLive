using FL.WebAPI.Core.Items.Configuration.Models;

namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }

        string VoteUrlService { get; }

        string VoteApiDomain { get; }
    }
}
