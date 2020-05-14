using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserImageService: IUserImageService
    {
        private readonly IUserImageRepository uploadImageRepository;
        private readonly ILogger<UserImageService> logger;
        private readonly IUserService userService;

        public UserImageService(
            IUserImageRepository uploadImageRepository,
            IUserService userService,
            ILogger<UserImageService> logger)
        {
            this.logger = logger;
            this.uploadImageRepository = uploadImageRepository;
            this.userService = userService;
        }

        public async Task<bool> DeleteImageAsync(Guid userId)
        {
            try
            {
                return await this.DeleteImageAsync(userId, string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.LogError("DeleteImageAsync", ex);
            }

            return false;
        }
        
        public async Task<bool> UploadImageAsync(Stream fileStream, string fileName, Guid userId)
        {
            var result = false;
            try
            {
                if (await this.DeleteImageAsync(userId, fileName)) {
                    result = await this.uploadImageRepository.UploadFileToStorage(fileStream, fileName);
                    if (result)
                    {
                        result = await this.UpdateImageAsync(userId, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("UploadFileToStorage", ex);
            }

            return result;
        }

        private async Task<bool> UpdateImageAsync(Guid userId, string photo)
        {
            try
            {
                if (await this.userService.UpdatePhotoAsync(userId, photo))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("AddImageAsync", ex);
            }
            return false;
        }

        private async Task<bool> DeleteImageAsync(Guid userId, string fileName)
        {
            var result = false;
            try
            {
                var user = await this.userService.GetByIdAsync(userId);

                if (user != null && !string.IsNullOrEmpty(user.Photo))
                {
                    if (string.IsNullOrEmpty(fileName) || fileName != user.Photo)
                    {
                        result = await this.uploadImageRepository.DeleteFileToStorage(user.Photo);
                        if (result)
                        {
                            return await this.UpdateImageAsync(userId, string.Empty);
                        }
                    }

                    return true;
                    
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("DeleteImageAsync", ex);
            }

            return false;
        }
    }
}
