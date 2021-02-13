using FL.WebAPI.Core.Items.Configuration.Models;

namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IItemConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        string PrimaryKey { get; }

        string Database { get; }

        CosmosConfiguration CosmosConfiguration { get; }
    }
}
