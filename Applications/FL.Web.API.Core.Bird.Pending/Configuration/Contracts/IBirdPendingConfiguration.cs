using FL.Web.API.Core.Bird.Pending.Configuration.Models;

namespace FL.Web.API.Core.Bird.Pending.Configuration.Contracts
{
    public interface IBirdPendingConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }
    }
}
