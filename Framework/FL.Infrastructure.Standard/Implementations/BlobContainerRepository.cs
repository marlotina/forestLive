using FL.Infrastructure.Standard.Configuration.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FL.Infrastructure.Standard.Contracts;

namespace FL.Infrastructure.Standard.Implementations
{
    public class BlobContainerRepository : IBlobContainerRepository
    {
        private readonly IAzureStorageConfiguration azureStorageConfiguration;
        private const string BLOB_AZURE_URL = ".blob.core.windows.net/";

        public BlobContainerRepository(
            IAzureStorageConfiguration azureStorageConfiguration)
        {
            this.azureStorageConfiguration = azureStorageConfiguration;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName, string containerName, string folder = null)
        {
            try
            {
                string containerRoute = string.IsNullOrEmpty(folder) ? containerName : containerName + "/" + folder;
                Uri blobUri = new Uri("https://" +
                          this.azureStorageConfiguration.AccountName +
                          BLOB_AZURE_URL +
                          containerRoute +
                          "/" + fileName);

                // Create StorageSharedKeyCredentials object by reading
                // the values from the configuration (appsettings.json)
                StorageSharedKeyCredential storageCredentials =
                    new StorageSharedKeyCredential(this.azureStorageConfiguration.AccountName, this.azureStorageConfiguration.AccountKey);

                // Create the blob client.
                BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

                // Upload the file
                await blobClient.UploadAsync(fileStream);


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
                // Create a URI to the blob
                Uri blobUri = new Uri("https://" +
                          this.azureStorageConfiguration.AccountName +
                          ".blob.core.windows.net/" +
                          containerName +
                          "/" + fileName);

                // Create StorageSharedKeyCredentials object by reading
                // the values from the configuration (appsettings.json)
                StorageSharedKeyCredential storageCredentials =
                    new StorageSharedKeyCredential(this.azureStorageConfiguration.AccountName, this.azureStorageConfiguration.AccountKey);

                // Create the blob client.
                BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

                await blobClient.DeleteAsync(DeleteSnapshotsOption.IncludeSnapshots);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }
    }
}
