using FL.Web.API.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserLabelRepository
    {
        Task<List<LabelDto>> GetUserLabelsByUserId(string userId);

        Task<UserLabel> GetUserLabel(string label, string userId);

        Task<UserLabel> AddLabel(UserLabel userLabel);

        Task<bool> DeleteLabel(UserLabel userLabel);

        Task<List<UserLabel>> GetUserLabelsDetails(string userId);
    }
}
