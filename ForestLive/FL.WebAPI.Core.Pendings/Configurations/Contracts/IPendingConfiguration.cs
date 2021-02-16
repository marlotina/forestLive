namespace FL.WebAPI.Core.Pendings.Configurations.Contracts
{
    public interface IPendingConfiguration
    {
        string CosmosDatabaseId { get; }

        string CosmosPendingContainer { get; }
    }
}
