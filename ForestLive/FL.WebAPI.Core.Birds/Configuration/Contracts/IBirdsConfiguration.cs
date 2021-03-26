
using FL.WebAPI.Core.Birds.Configuration.Models;

namespace FL.WebAPI.Core.Birds.Configuration.Contracts
{
    public interface IBirdsConfiguration
    {
        string ConnectionString { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }
    }
}
