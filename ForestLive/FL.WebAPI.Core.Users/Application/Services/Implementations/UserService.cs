using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using FL.WebAPI.Core.Users.Domain.Entities;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        private readonly IUserConfiguration userConfiguration;

        public UserService(
            IUserRepository iUserRepository,
            IUserConfiguration userConfiguration)
        {
            this.iUserRepository = iUserRepository;
            this.userConfiguration = userConfiguration;
        }

        public async Task<bool> DeleteAsync(Guid userId)
        {
            var entityUser = await this.GetByIdAsync(userId);
            if (entityUser != null)
            {
                var result = await this.iUserRepository.DeleteAsync(entityUser.Id);
                return result;
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
        
        public async Task<IEnumerable<Domain.Entities.User>> FindByEmailAsync(string email)
        {
            var result = await this.iUserRepository.FindByEmailAsync(email);
            return result;
        }

        public async Task<Domain.Entities.User> GetByIdAsync(Guid userId)
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

        public async Task<bool> UpdateAsync(Domain.Entities.User newUserData)
        {
            var result = default(bool);
            var user = await this.GetUser(newUserData.Id);
            if (user != null)
            {
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

        private async Task<bool> UpdateUser(Domain.Entities.User user)
        {
            var result = await this.iUserRepository.UpdateAsync(user);
            return result;
        }

        private async Task<Domain.Entities.User> GetUser(Guid id)
        {
            return await this.iUserRepository.GetByIdAsync(id);
        }
    }
}
