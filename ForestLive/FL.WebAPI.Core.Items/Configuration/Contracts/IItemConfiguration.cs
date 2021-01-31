namespace FL.WebAPI.Core.Items.Configuration.Contracts
{
    public interface IItemConfiguration
    {
        string BirdPhotoContainer { get; }

        string Secret { get; }

        string Host { get; }

        string PrimaryKey { get; }

        string Database { get; }

        string Container { get; }

        string CosmosdbConnectionstring { get; }

        string CosmosDatabaseId { get; }

        string CosmosContainerId { get; }
    }
}
