using FL.Web.API.Core.User.Interactions.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Domain.Repositories
{
    public interface IFollowerRepository
    {
        Task<List<FollowerUser>> GetFollowersByUserId(string userId);
    }
}
