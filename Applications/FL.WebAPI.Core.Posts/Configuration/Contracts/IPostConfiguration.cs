using FL.WebAPI.Core.Posts.Configuration.Models;

namespace FL.WebAPI.Core.Posts.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        CosmosConfiguration CosmosConfiguration { get; }
    }
}
