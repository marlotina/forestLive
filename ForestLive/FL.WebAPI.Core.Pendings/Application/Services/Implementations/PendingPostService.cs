using FL.WebAPI.Core.Pendings.Application.Services.Contracts;
using FL.WebAPI.Core.Pendings.Domain.Entities;
using FL.WebAPI.Core.Pendings.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Application.Services.Implementations
{
    public class PendingPostService : IPendingPostService
    {
        private readonly IPendingPostRepository pendingPostRepository;

        public PendingPostService(
            IPendingPostRepository pendingPostRepository)
        {
            this.pendingPostRepository = pendingPostRepository;
        }

        public async Task<IEnumerable<BirdPost>> GetPostByStatus(string status)
        {
            return await this.pendingPostRepository.GetPendingPostsByStatus(status);
        }
    }
}
