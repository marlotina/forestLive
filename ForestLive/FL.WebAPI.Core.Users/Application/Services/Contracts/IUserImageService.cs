using FL.WebAPI.Core.Users.Api.Models.v1.Request;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserImageService
    {
        Task<bool> DeleteImageAsync (string userId, string webUserId, string imageName);

        Task<bool> UploadImageAsync (ImageProfileRequest request, string webUserId);
    }
}
