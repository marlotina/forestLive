using FL.Infrastructure.Standard.Contracts;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
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

        public async Task UploadFileToStorage(Stream fileStream, string fileName)
        {
            await this.blobContainerRepository.UploadFileToStorage(fileStream, fileName, this.userConfiguration.ImageProfileContainer);
        }

        public async Task DeleteFileToStorage(string fileName)
        {
            
            await this.blobContainerRepository.DeleteFileToStorage(fileName, this.userConfiguration.ImageProfileContainer);
        }
    }
}
