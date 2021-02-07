using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.LogTrace.Contracts.Standard;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        private readonly ILogger<UserService> logger;

        public UserService(
            IUserRepository iUserRepository,
            ILogger<UserService> logger)
        {
            this.iUserRepository = iUserRepository;
            this.logger = logger;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var result = false;
            try
            {
                var entityUser = await this.GetByIdAsync(userId);
                if (entityUser != null)
                {
                    result = await this.iUserRepository.DeleteAsync(entityUser.Id);
                }
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return result;
        }
        
        public async Task<IEnumerable<User>> FindByEmailAsync(string email)
        {
            try
            {
                return await this.iUserRepository.FindByEmailAsync(email);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return null;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            var response = new User();
            try
            {
                response = await this.iUserRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return response;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var response = new User();
            try
            {
                response = await this.iUserRepository.GetByUserNameAsync(userName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return response;
        }

        public async Task<bool> UpdatePhotoAsync(Guid userId, string photo)
        {
            var result = default(bool);
            try
            {
                var user = await this.GetUser(userId);

                if (user != null)
                {
                    user.Photo = photo;
                    result = await this.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return result;
        }

        public async Task<bool> UpdateAsync(User newUserData)
        {
            var result = false;

            try
            {
                var newNormalizeUserName = newUserData.UserName.ToUpperInvariant();
                var user = await this.GetUser(newUserData.Id);

                var isValidUserName = await IsValidUserName(newNormalizeUserName, user.NormalizedUserName);

                if (!isValidUserName)
                    throw new UserDuplicatedException();

                if (user != null)
                {
                    user.UserName = newUserData.UserName;
                    user.NormalizedUserName = newNormalizeUserName;
                    user.Name = newUserData.Name;
                    user.Surname = newUserData.Surname;
                    user.Description = newUserData.Description;
                    user.IsCompany = newUserData.IsCompany;
                    user.LanguageId = newUserData.LanguageId;
                    user.Photo = user.Photo;
                    user.UrlWebSite = newUserData.UrlWebSite;
                    user.Location = newUserData.Location;
                    user.AcceptedConditions = newUserData.AcceptedConditions;
                    user.TwitterUrl = newUserData.TwitterUrl;
                    user.FacebookUrl = newUserData.FacebookUrl;
                    user.InstagramUrl = newUserData.InstagramUrl;
                    user.LinkedlinUrl = newUserData.LinkedlinUrl;

                    result = await this.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return result;
        }

        private async Task<bool> IsValidUserName(string newNormalizeUserName, string userName)
        {
            if (newNormalizeUserName != userName)
            {
                var userExits = await this.GetUserByUserName(newNormalizeUserName);

                if (userExits != null)
                {
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> UpdateUser(User user)
        {
            return await this.iUserRepository.UpdateAsync(user);
        }

        private async Task<User> GetUserByUserName(string userName)
        {
            return await this.iUserRepository.GetByUserNameAsync(userName);
        }

        private async Task<User> GetUser(Guid id)
        {
            return await this.iUserRepository.GetByIdAsync(id);
        }
    }
}
