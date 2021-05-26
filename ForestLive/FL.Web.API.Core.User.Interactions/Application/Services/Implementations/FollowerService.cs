using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class FollowerService : IFollowerService
    { 
        private readonly IFollowerRepository iFollowerRepository;

        public FollowerService(
            IFollowerRepository iFollowerRepository)
        {
            this.iFollowerRepository = iFollowerRepository;
        }

        public async Task<List<FollowUser>> GetFollowerByUserId(string userId)
        {
            return await this.iFollowerRepository.GetFollowersByUserId(userId);
        }
    }
}
