using FL.WebAPI.Core.Searchs.Domain.Dto;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Contracts
{
    public interface IUserInfoService
    {
        Task<string> GetUserImageById(string userId);
    }
}
