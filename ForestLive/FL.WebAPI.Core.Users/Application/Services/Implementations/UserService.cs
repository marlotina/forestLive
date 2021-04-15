using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Repositories;
using System;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.LogTrace.Contracts.Standard;
using FL.Cache.Standard.Contracts;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository iUserRepository;
        //private readonly ICustomMemoryCache<IEnumerable<AutocompleteResponse>> customMemoryCache;
        private readonly ILogger<UserService> logger;

        public UserService(
            IUserRepository iUserRepository,
            //ICustomMemoryCache<IEnumerable<AutocompleteResponse>> customMemoryCache,
            ILogger<UserService> logger)
        {
            this.iUserRepository = iUserRepository;
            this.logger = logger;
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
