using FL.Web.API.Core.Comments.Configuration.Models;

namespace FL.Web.API.Core.Comments.Configuration.Contracts
{
    public interface IPostConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        CosmosConfig CosmosConfiguration { get; }

        ServiceBusConfig ServiceBusConfig { get;  }
    }
}
