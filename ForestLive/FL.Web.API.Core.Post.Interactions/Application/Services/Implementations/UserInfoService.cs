using FL.Cache.Standard.Contracts;
using FL.Web.API.Core.Post.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FL.Web.API.Core.Post.Interactions.Application.Services.Implementations
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

            var filter = itemCache != null ? itemCache.FirstOrDefault(x => x.Id == userId) : null;

            return filter != null ? filter.Photo : "porfile.png"; ;              
        }

    }
}
