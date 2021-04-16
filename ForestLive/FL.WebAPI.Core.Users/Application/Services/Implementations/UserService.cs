﻿using FL.WebAPI.Core.Users.Application.Services.Contracts;
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
using FL.WebAPI.Core.Items.Domain.Repositories;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserCosmosRepository iUserCosmosRepository;
        private readonly ICustomMemoryCache<IEnumerable<UserInfo>> customMemoryCache;
        private readonly ILogger<UserService> logger;

        public UserService(
            IUserCosmosRepository iUserCosmosRepository,
            ICustomMemoryCache<IEnumerable<UserInfo>> customMemoryCache,
            ILogger<UserService> logger)
        {
            this.customMemoryCache = customMemoryCache;
            this.iUserCosmosRepository = iUserCosmosRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<AutocompleteResponse>> AutocompleteByUserName(string keys)
        {
            var itemsCache = this.customMemoryCache.Get("users");

            if (itemsCache == null || !itemsCache.Any())
            {
                itemsCache = await this.iUserCosmosRepository.GetUsersAsync();
                
                this.customMemoryCache.Add("users", itemsCache);
            }
            var request = keys.ToUpper().NormalizeName();
            var filter = itemsCache.Where(x => x.UserId.ToLower().Contains(request));

            if (filter != null && filter.Any())
            {
                return filter.Select(x => new AutocompleteResponse {
                    UserName = x.UserId,
                    UserPhoto = x.Photo
                });
            }

            return null;
        }

        public async Task<IEnumerable<UserInfo>> GetUsersByKey(string keys)
        {
            var itemsCache = this.customMemoryCache.Get("users");

            if (itemsCache == null || !itemsCache.Any())
            {
                itemsCache = await this.iUserCosmosRepository.GetUsersAsync();

                this.customMemoryCache.Add("users", itemsCache);
            }
            var request = keys.ToUpper().NormalizeName();
            var filter = itemsCache.Where(x => x.UserId.ToLower().Contains(request));

            return filter;
        }

        public async Task<UserInfo> GetByUserNameAsync(string userName)
        {
            var response = new UserInfo();
            try
            {
                response = await this.iUserCosmosRepository.GetUserByName(userName);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return response;
        }

        public async  Task<IEnumerable<UserInfo>> GetUsersAsync()
        {
            try
            {
                return await this.iUserCosmosRepository.GetUsersAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteAsync");
            }

            return null;
        }
    }
}
