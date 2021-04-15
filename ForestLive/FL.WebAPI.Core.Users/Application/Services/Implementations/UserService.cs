using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.LogTrace.Contracts.Standard;
using FL.Cache.Standard.Contracts;
using System.Collections.Generic;
using FL.WebAPI.Core.Users.Domain.Dto;
using FL.Pereza.Helpers.Standard.Extensions;
using System.Linq;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        private readonly ICustomMemoryCache<IEnumerable<User>> customMemoryCache;
        private readonly ILogger<UserService> logger;

        public UserService(
            IUserRepository iUserRepository,
            ICustomMemoryCache<IEnumerable<User>> customMemoryCache,
            ILogger<UserService> logger)
        {
            this.customMemoryCache = customMemoryCache;
            this.iUserRepository = iUserRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<AutocompleteResponse>> AutocompleteByUserName(string keys)
        {
            var itemsCache = this.customMemoryCache.Get("users");

            if (itemsCache == null || !itemsCache.Any())
            {
                itemsCache = await this.iUserRepository.GetUsersAsync();
                
                this.customMemoryCache.Add("users", itemsCache);
            }
            var request = keys.ToUpper().NormalizeName();
            var filter = itemsCache.Where(x => x.NormalizedUserName.Contains(request));

            if (filter != null && filter.Any())
            {
                return filter.Select(x => new AutocompleteResponse {
                    UserName = x.UserName,
                    UserPhoto = x.Photo
                });
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetUsersByKey(string keys)
        {
            var itemsCache = this.customMemoryCache.Get("users");

            if (itemsCache == null || !itemsCache.Any())
            {
                itemsCache = await this.iUserRepository.GetUsersAsync();

                this.customMemoryCache.Add("users", itemsCache);
            }
            var request = keys.ToUpper().NormalizeName();
            var filter = itemsCache.Where(x => x.NormalizedUserName.Contains(request));

            return filter;
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

        public async  Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                return await this.iUserRepository.GetUsersAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return null;
        }
    }
}
