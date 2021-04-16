﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserImageService
    {
        Task<bool> DeleteImageAsync (Guid userId, string webUserId);

        Task<bool> UploadImageAsync (Stream fileStream, string fileName, Guid userId, string webUserId);
    }
}
