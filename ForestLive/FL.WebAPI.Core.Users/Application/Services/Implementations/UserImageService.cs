using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FL.BlobContainer.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Images;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Api.Models.v1.Request;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Configuration.Contracts;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserImageService: IUserImageService
    {
        private readonly IUserManagedService userManagedService;
        private readonly IUserCosmosRepository iUserCosmosRepository;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IUserConfiguration userConfiguration;

        public UserImageService(
            IUserManagedService userManagedService,
            IBlobContainerRepository blobContainerRepository,
            IUserConfiguration userConfiguration,
            IUserCosmosRepository iUserCosmosRepository)
        {
            this.iUserCosmosRepository = iUserCosmosRepository;
            this.userManagedService = userManagedService;
            this.userConfiguration = userConfiguration;
            this.blobContainerRepository = blobContainerRepository;
        }

        public async Task<bool> DeleteImageAsync(string userId, string webUserId, string imageName)
        {
             var user = await this.iUserCosmosRepository.GetUser(userId, webUserId);

            if (user != null && user.Id == webUserId && imageName != "profile.png")
            {
                if (await this.blobContainerRepository.DeleteFileToStorage(imageName, this.userConfiguration.ImageProfileContainer))
                {
                    user.Photo = "profile.png";
                    return await this.userManagedService.UpdateAsync(user, webUserId);
                }
            }
            //ojo no rights to update

            return false;
        }

        public async Task<bool> UploadImageAsync(ImageProfileRequest request, string webUserId)
        {
            var user = await this.iUserCosmosRepository.GetUser(request.UserId, webUserId);

            if (user != null && user.Id == webUserId)
            {
                if (request.Hasmage)
                {
                    if (!await this.blobContainerRepository.DeleteFileToStorage(user.Photo, this.userConfiguration.ImageProfileContainer))
                        return false;
                }

                //var fileStream = await this.SavePhoto(fileData);
                var fileName = $"{request.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}";

                if (await this.SavePhoto(request.ImageBase64, fileName)) {
                    if (user.Photo != fileName)
                    {
                        user.Photo = fileName;
                        return await this.userManagedService.UpdateAsync(user, webUserId);
                    }

                    return true;
                }
            }

            return false;
        }

        private async Task<bool> SavePhoto(string imageData, string fileName)
        {

            var imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);

            var contents = new StreamContent(new MemoryStream(imageBytes));
            var imageStream = await contents.ReadAsStreamAsync();

            var stream = new MemoryStream();
            Image image = Image.FromStream(imageStream);
            Image thumb = image.GetThumbnailImage(image.Width, image.Height, () => false, IntPtr.Zero);
            thumb.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            return await this.blobContainerRepository.UploadFileToStorage(stream, fileName, this.userConfiguration.ImageProfileContainer);
        }
    }
}
