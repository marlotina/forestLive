using FL.CosmosDb.Standard.Configuration.Dto;

namespace FL.CosmosDb.Standard.Configuration.Contracts
{
    public interface ICosmosConfiguration
    {
        CosmosConfig DataConfig { get; }
    }
}
