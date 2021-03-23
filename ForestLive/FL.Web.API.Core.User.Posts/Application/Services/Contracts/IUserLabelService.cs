using FL.Web.API.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Application.Services.Contracts
{
    public interface IUserLabelService
    {
        Task<List<string>> GetLabelsByUser(string userId);

        Task<UserLabel> AddLabel(UserLabel userLabel);

        Task<bool> DeleteLabel(string label, string userWebSite);

        Task<List<UserLabel>> GetUserLabelsDetails(string userId);
    }
}
