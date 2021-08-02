using FL.WebAPI.Core.Searchs.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Domain.Repository
{
    public interface IUserInfoRestRepository
    {
        Task<IEnumerable<InfoUserResponse>> GetUsers();
    }
}
