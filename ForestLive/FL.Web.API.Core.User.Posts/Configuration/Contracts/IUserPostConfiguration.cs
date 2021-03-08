using FL.WebAPI.Core.User.Posts.Configuration.Models;

namespace FL.WebAPI.Core.User.Posts.Configuration.Contracts
{
    public interface IUserPostConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }

        string VoteApiDomain { get; }

        string VoteUrlService { get; }
    }
}
