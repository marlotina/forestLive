using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Infrastructure.AzureStorage
{
    public class UserImageRepository : IUserImageRepository
    {
        private readonly IUserConfiguration userConfiguration;

        public UserImageRepository(
            IUserConfiguration userConfiguration)
        {
            this.userConfiguration = userConfiguration;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            try
            {
                // Create storagecredentials object by reading the values from the configuration (appsettings.json)
                StorageCredentials storageCredentials = new StorageCredentials(this.userConfiguration.AccountName, this.userConfiguration.AccountKey);

                // Create cloudstorage account by passing the storagecredentials
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = blobClient.GetContainerReference(this.userConfiguration.ImageContainer);

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

        public async Task<bool> DeleteFileToStorage(string fileName)
        {
            try
            {
                // Create storagecredentials object by reading the values from the configuration (appsettings.json)
                StorageCredentials storageCredentials = new StorageCredentials(this.userConfiguration.AccountName, this.userConfiguration.AccountKey);

                // Create cloudstorage account by passing the storagecredentials
                CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Get reference to the blob container by passing the name by reading the value from the configuration (appsettings.json)
                CloudBlobContainer container = blobClient.GetContainerReference(this.userConfiguration.ImageContainer);
                
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
