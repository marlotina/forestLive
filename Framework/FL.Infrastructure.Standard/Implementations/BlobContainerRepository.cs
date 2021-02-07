using FL.Infrastructure.Standard.Configuration.Contracts;
using FL.Infrastructure.Implementations.Domain.Repository;
using FL.LogTrace.Contracts.Standard;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.Infrastructure.Implementations.Implementations
{
    public class BlobContainerRepository : IBlobContainerRepository
    {
        private readonly IAzureStorageConfiguration azureStorageConfiguration;

        public BlobContainerRepository(
            IAzureStorageConfiguration azureStorageConfiguration)
        {
            this.azureStorageConfiguration = azureStorageConfiguration;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, string containerName, string folder = null)
        {
            try
            {
                // Create storagecredentials object by reading the values from the configuration (appsettings.json)
                StorageCredentials storageCredentials = new StorageCredentials(this.azureStorageConfiguration.AccountName, this.azureStorageConfiguration.AccountKey);

                // Create cloudstorage account by passing the storagecredentials
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                string containerRoute = string.IsNullOrEmpty(folder) ? containerName : containerName + "/" + folder;
                CloudBlobContainer container = blobClient.GetContainerReference(containerRoute);

                // Get the reference to the block blob from the container
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                // Upload the file
                await blockBlob.UploadFromStreamAsync(fileStream);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteFileToStorage(string fileName, string containerName)
        {
            try
            {
                // Create storagecredentials object by reading the values from the configuration (appsettings.json)
                StorageCredentials storageCredentials = new StorageCredentials(this.azureStorageConfiguration.AccountName, this.azureStorageConfiguration.AccountKey);

                // Create cloudstorage account by passing the storagecredentials
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Get the reference to the block blob from the container
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);

                await blockBlob.DeleteAsync();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
