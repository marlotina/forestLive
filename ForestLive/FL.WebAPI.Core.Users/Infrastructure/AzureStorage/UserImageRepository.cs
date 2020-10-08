using FL.Infrastructure.Implementations.Domain.Repository;
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
        readonly ILogger<UserImageRepository> logger;
        private readonly IUserConfiguration userConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;

        public UserImageRepository(
            IUserConfiguration userConfiguration,
            IBlobContainerRepository blobContainerRepository,
            ILogger<UserImageRepository> logger)
        {
            this.userConfiguration = userConfiguration;
            this.blobContainerRepository = blobContainerRepository;
            this.logger = logger;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            try
            {
                var result = await this.blobContainerRepository.UploadFileToStorage(fileStream, fileName, this.userConfiguration.ImageProfileContainer);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteFileToStorage(string fileName)
        {
            try
            {
                var result = await this.blobContainerRepository.DeleteFileToStorage(fileName, this.userConfiguration.ImageProfileContainer);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return await Task.FromResult(false);
            }
        }
    }
}
