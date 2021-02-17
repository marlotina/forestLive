using FL.Infrastructure.Standard.Configuration.Dto;

namespace FL.Infrastructure.Standard.Configuration.Contracts
{
    public interface IAzureStorageConfiguration
    {
        StorageConfig StorageConfiguration { get; }
    }
}
