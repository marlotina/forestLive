using FL.WebAPI.Core.Birds.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface IUserInfoRestRepository
    {
        Task<IEnumerable<InfoUserResponse>> GetUsers();
    }
}
