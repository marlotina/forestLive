
using FL.WebAPI.Core.Birds.Configuration.Models;

namespace FL.WebAPI.Core.Birds.Configuration.Contracts
{
    public interface IBirdsConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }
    }
}
