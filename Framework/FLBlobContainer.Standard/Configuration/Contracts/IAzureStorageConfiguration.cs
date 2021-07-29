using FL.BlobContainer.Standard.Configuration.Dto;

namespace FL.BlobContainer.Standard.Configuration.Contracts
{
    public interface IAzureStorageConfiguration
    {
        StorageConfig StorageConfiguration { get; }
    }
}
