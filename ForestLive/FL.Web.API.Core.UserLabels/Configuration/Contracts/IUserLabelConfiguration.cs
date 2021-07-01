using FL.WebAPI.Core.UserLabels.Configuration.Models;

namespace FL.WebAPI.Core.UserLabels.Configuration.Contracts
{
    public interface IUserLabelConfiguration
    {
        CosmosConfiguration CosmosConfiguration { get; }
    }
}
