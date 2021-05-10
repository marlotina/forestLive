using System;
using System.IO;
using System.Threading.Tasks;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserImageService: IUserImageService
    {
        private readonly IUserImageRepository uploadImageRepository;
        private readonly IUserManagedService userManagedService;
        private readonly IUserCosmosRepository iUserCosmosRepository;

        public UserImageService(
            IUserImageRepository uploadImageRepository,
            IUserManagedService userManagedService,
            IUserCosmosRepository iUserCosmosRepository)
        {
            this.iUserCosmosRepository = iUserCosmosRepository;
            this.uploadImageRepository = uploadImageRepository;
            this.userManagedService = userManagedService;
        }

        public async Task<bool> DeleteImageAsync(Guid userId, string webUserId)
        {
             var user = await this.iUserCosmosRepository.GetUser(userId, webUserId);

            if (user != null && user.UserId == webUserId && user.Photo != "profile.png")
            {
                if (await this.uploadImageRepository.DeleteFileToStorage(user.Photo))
                {
                    user.Photo = "profile.png";
                    return await this.userManagedService.UpdateAsync(user, webUserId);
                }
            }
            //ojo no rights to update

            return false;
        }

        public async Task<bool> UploadImageAsync(Stream fileStream, string fileName, Guid userId, string webUserId)
        {
            var user = await this.iUserCosmosRepository.GetUser(userId, webUserId);

            if (user != null && user.UserId == webUserId)
            {
                if (user.Photo != "profile.png")
                {
                    if (!await this.uploadImageRepository.DeleteFileToStorage(user.Photo))
                        return false;
                
                }

                if (await this.uploadImageRepository.UploadFileToStorage(fileStream, fileName)) {
                    user.Photo = fileName;
                    return await this.userManagedService.UpdateAsync(user, webUserId);
                }
            }

            return false;
        }
    }
}
