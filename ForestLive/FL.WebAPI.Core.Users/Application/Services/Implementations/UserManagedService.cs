using FL.WebAPI.Core.Users.Application.Services.Contracts;
using System;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Domain.Repositories;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserManagedService : IUserManagedService
    {
        private readonly ILogger<UserManagedService> logger;
        private readonly IUserCosmosRepository iUserCosmosRepository;

        public UserManagedService(
            IUserCosmosRepository iUserCosmosRepository,
            ILogger<UserManagedService> logger)
        {
            this.iUserCosmosRepository = iUserCosmosRepository;
            this.logger = logger;
        }

        public async Task<bool> DeleteAsync(string userId, string userWebId)
        {
            var result = false;
            try
            {
                var entityUser = await this.iUserCosmosRepository.GetUser(userId, userWebId);

                if (entityUser != null && entityUser.Id == userWebId)
                {
                    return await this.iUserCosmosRepository.DeleteUserInfoAsync(entityUser.Id, entityUser.Id);
                }
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return result;
        }

        public async Task<bool> UpdateAsync(UserInfo newUserData, string userIdRequest)
        {
            try
            {
                var user = await this.iUserCosmosRepository.GetUser(newUserData.Id, userIdRequest);

                if (user != null && user.Id == userIdRequest)
                {
                    user.Name = newUserData.Name;
                    user.Surname = newUserData.Surname;
                    user.Description = newUserData.Description;
                    user.IsCompany = newUserData.IsCompany;
                    user.LanguageId = newUserData.LanguageId;
                    user.Photo = newUserData.Photo;
                    user.UrlWebSite = newUserData.UrlWebSite;
                    user.Location = newUserData.Location;
                    user.TwitterUrl = newUserData.TwitterUrl;
                    user.FacebookUrl = newUserData.FacebookUrl;
                    user.InstagramUrl = newUserData.InstagramUrl;
                    user.LinkedlinUrl = newUserData.LinkedlinUrl;
                    user.LastModification = DateTime.UtcNow;
                    user.Latitude = newUserData.Latitude;
                    user.Longitude = newUserData.Longitude;

                    return await this.iUserCosmosRepository.UpdateUserInfoAsync(user);
                }
                else {
                    throw new UserDuplicatedException();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return false;
        }

        public async Task<UserInfo> GetUserAsync(string userId)
        {
            return await this.iUserCosmosRepository.GetUserByName(userId);
        }

        public async Task<UserInfo> GetUserAsync(string userId, string userName)
        {
            return await this.iUserCosmosRepository.GetUser(userId, userName);
        }
    }
}
