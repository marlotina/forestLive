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
        private readonly IUserConfiguration userConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;

        public UserImageRepository(
            IUserConfiguration userConfiguration,
            IBlobContainerRepository blobContainerRepository)
        {
            this.userConfiguration = userConfiguration;
            this.blobContainerRepository = blobContainerRepository;
        }

        public async Task<bool> UploadFileToStorage(Stream fileStream, string fileName)
        {
            await this.blobContainerRepository.UploadFileToStorage(fileStream, fileName, this.userConfiguration.ImageProfileContainer);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteFileToStorage(string fileName)
        {
            
            await this.blobContainerRepository.DeleteFileToStorage(fileName, this.userConfiguration.ImageProfileContainer);
            return await Task.FromResult(true);
        }
    }
}
