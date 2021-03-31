using FL.Web.API.Core.Bird.Pending.Configuration.Models;

namespace FL.Web.API.Core.Bird.Pending.Configuration.Contracts
{
    public interface IBirdsConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }

        string BirdPhotoContainer { get; }

        ServiceBusConfig ServiceBusConfig { get; }
    }
}
