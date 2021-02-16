using FL.WebAPI.Core.Pendings.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Domain.Repository
{
    public interface IPendingPostRepository
    {
        Task<List<BirdPost>> GetPendingPostsByStatus(string status);
    }
}
