using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Infrastructure.AzureStorage
{
    public class UserImageRepository : IUserImageRepository
    {
        private readonly IUserConfiguration userConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly ILogger<UserImageRepository> iLogger;
        public UserImageRepository(
            IUserConfiguration userConfiguration,
            IBlobContainerRepository blobContainerRepositor,
            ILogger<UserImageRepository> iLogger)
        {
            this.userConfiguration = userConfiguration;
            this.blobContainerRepository = blobContainerRepositor;
            this.iLogger = iLogger;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            try
            {
                await this.blobContainerRepository.UploadFileToStorage(fileStream, fileName, this.userConfiguration.ImageProfileContainer);
                return true;
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteFileToStorage(string fileName)
        {
            try
            {
                await this.blobContainerRepository.DeleteFileToStorage(fileName, this.userConfiguration.ImageProfileContainer);
                return true;
            }
            catch (Exception ex) 
            {
                this.iLogger.LogError(ex.Message);
                return false;
            }
            
        }
    }
}
