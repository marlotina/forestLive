using FL.WebAPI.Core.Items.Configuration.Models;

namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        CosmosConfiguration CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
