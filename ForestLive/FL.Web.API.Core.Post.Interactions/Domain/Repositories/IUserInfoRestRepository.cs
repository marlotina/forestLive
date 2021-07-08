using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Domain.Repositories
{
    public interface IUserInfoRestRepository
    {
        Task<IEnumerable<InfoUserResponse>> GetUsers();
    }
}
