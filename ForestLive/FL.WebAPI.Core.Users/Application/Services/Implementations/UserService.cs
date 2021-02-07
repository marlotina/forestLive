using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Application.Exceptions;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;

        public UserService(
            IUserRepository iUserRepository)
        {
            this.iUserRepository = iUserRepository;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var result = false;
            var entityUser = await this.GetByIdAsync(userId);
            if (entityUser != null)
            {
                result = await this.iUserRepository.DeleteAsync(entityUser.Id);
            }

            return result;
        }
        
        public async Task<IEnumerable<User>> FindByEmailAsync(string email)
        {
            var result = await this.iUserRepository.FindByEmailAsync(email);
            return result;
        }

        public async Task<User> GetByIdAsync(Guid userId)
        {
            var result = await this.iUserRepository.GetByIdAsync(userId);
            return result;
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            var result = await this.iUserRepository.GetByUserNameAsync(userName);
            return result;
        }

        public async Task<bool> UpdatePhotoAsync(Guid userId, string photo)
        {
            var result = default(bool);
            var user = await this.GetUser(userId);
            
            if (user != null)
            {
                user.Photo = photo;
                result = await this.UpdateUser(user);
            }

            return result;
        }

        public async Task<bool> UpdateAsync(User newUserData)
        {
            var result = false;

            var newNormalizeUserName = newUserData.UserName.ToUpperInvariant();
            var user = await this.GetUser(newUserData.Id);

            var isValidUserName = await IsValidUserName(newNormalizeUserName, user.NormalizedUserName);

            if(!isValidUserName)
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
            var result = await this.iUserRepository.UpdateAsync(user);
            return result;
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
