using FL.Cache.Standard.Contracts;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Implementations
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IUserInfoRestRepository iUserInfoRestRepository;
        private readonly ICustomMemoryCache<IEnumerable<InfoUserResponse>> iCustomMemoryCache;
        private const string CACHE_USERID = "CACHE_USERID";
        public UserInfoService(
            IUserInfoRestRepository iUserInfoRestRepository,
            ICustomMemoryCache<IEnumerable<InfoUserResponse>> iCustomMemoryCache)
        {
            this.iUserInfoRestRepository = iUserInfoRestRepository;
            this.iCustomMemoryCache = iCustomMemoryCache;
        }

        public async Task<string> GetUserImageById(string userId)
        {
            var itemCache = this.iCustomMemoryCache.Get(CACHE_USERID);

            if (itemCache == null || !itemCache.Any())
            {
                itemCache = await this.iUserInfoRestRepository.GetUsers();
                this.iCustomMemoryCache.Add(CACHE_USERID, itemCache);
            }

            var filter = itemCache.FirstOrDefault(x => x.Id == userId);

            return filter != null ? filter.Photo : "porfile.png";             
        }

    }
}
