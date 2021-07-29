using FL.Web.API.Core.User.Interactions.Configuration.Models;

namespace FL.Web.API.Core.User.Interactions.Configuration.Contracts
{
    public interface IUserInteractionsConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }
    }
}
