﻿using System;
using System.IO;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserImageService: IUserImageService
    {
        private readonly IUserImageRepository uploadImageRepository;
        private readonly ILogger<UserImageService> logger;
        private readonly IUserManagedService userManagedService;

        public UserImageService(
            IUserImageRepository uploadImageRepository,
            IUserManagedService userManagedService,
            ILogger<UserImageService> logger)
        {
            this.logger = logger;
            this.uploadImageRepository = uploadImageRepository;
            this.userManagedService = userManagedService;
        }

        public async Task<bool> DeleteImageAsync(Guid userId)
        {
            try
            {
                return await this.DeleteImageAsync(userId, string.Empty);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "UploadFileToStorage");
            }

            return false;
        }
        
        public async Task<bool> UploadImageAsync(Stream fileStream, string fileName, Guid userId)
        {
            try
            {
                if (await this.DeleteImageAsync(userId, fileName)) {

                    await this.uploadImageRepository.UploadFileToStorage(fileStream, fileName);       
                    return await this.UpdateImageAsync(userId, fileName);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "UploadFileToStorage");
            }

            return false;
        }

        private async Task<bool> UpdateImageAsync(Guid userId, string photo)
        {
            try
            {
                if (await this.userManagedService.UpdatePhotoAsync(userId, photo))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddImageAsync");
            }

            return false;
        }

        private async Task<bool> DeleteImageAsync(Guid userId, string fileName)
        {
            try
            {
                var user = await this.userManagedService.GetByIdAsync(userId);

                if (user != null && !string.IsNullOrEmpty(user.Photo))
                {
                    if (string.IsNullOrEmpty(fileName) || fileName != user.Photo)
                    {
                        await this.uploadImageRepository.DeleteFileToStorage(user.Photo);
                        return await this.UpdateImageAsync(userId, string.Empty);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteImageAsync");
            }

            return false;
        }
    }
}
