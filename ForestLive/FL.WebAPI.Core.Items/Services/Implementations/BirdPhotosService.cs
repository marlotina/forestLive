using FL.Infrastructure.Implementations.Domain.Repository;
using FL.Logging.Implementation.Standard;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Services.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Services.Implementations
{
    public class BirdPhotosService : IBirdPhotosService
    {
        private readonly IItemConfiguration itemConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly Logger<BirdPhotosService> logger;
        public BirdPhotosService(IItemConfiguration itemConfiguration,
            IBlobContainerRepository blobContainerRepository,
            Logger<BirdPhotosService> logger)
        {
            this.blobContainerRepository = blobContainerRepository;
            this.itemConfiguration = itemConfiguration;
            this.logger = logger;
        }
        public async Task<bool> AddBirdInfo(BirdPost birdPhoto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateBirdPhoto(Stream fileStream, string fileName)
        {
            try
            {
                var result = await this.blobContainerRepository.UploadFileToStorage(fileStream, fileName, this.itemConfiguration.BirthPhotoContainer);
                return result;
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex);
                return false;
            }
        }

        public Task<bool> DeleteBird(Guid BirdItemId)
        {
            throw new NotImplementedException();
        }
    }
}
