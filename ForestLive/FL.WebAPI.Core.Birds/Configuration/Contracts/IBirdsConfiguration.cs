using FL.WebAPI.Core.Birds.Configuration.Models;
using RestSharp;

namespace FL.WebAPI.Core.Birds.Configuration.Contracts
{
    public interface IBirdsConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }

        string UserApiDomain { get; }

        string UserUrlService { get; }

        string SpecieApiDomain { get; }

        string SpecieUrlService { get; }

        string SpecieLanguageUrlService { get; }
    }
}
