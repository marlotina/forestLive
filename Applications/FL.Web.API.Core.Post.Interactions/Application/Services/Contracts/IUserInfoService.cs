using System.Threading.Tasks;

namespace FL.Web.API.Core.Post.Interactions.Application.Services.Contracts
{
    public interface IUserInfoService
    {
        Task<string> GetUserImageById(string userId);
    }
}
