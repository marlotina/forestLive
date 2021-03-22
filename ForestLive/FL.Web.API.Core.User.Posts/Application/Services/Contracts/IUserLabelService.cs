using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserLabelService
    {
        Task<List<string>> GetLabelsByUser(string userId);
    }
}
