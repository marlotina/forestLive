using FL.WebAPI.Core.Birds.Domain.Dto;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IUserInfoService
    {
        Task<string> GetUserImageById(string userId);
    }
}
