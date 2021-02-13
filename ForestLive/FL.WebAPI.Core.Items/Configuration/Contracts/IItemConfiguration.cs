using FL.WebAPI.Core.Items.Configuration.Models;

namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IItemConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        CosmosConfiguration CosmosConfiguration { get; }
    }
}
