using FL.WebAPI.Core.Users.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Repositories
{
    public interface IUserLabelRepository
    {
        Task<UserLabel> GetUserLabel(string label, string userId);

        Task<UserLabel> AddLabel(UserLabel userLabel);

        Task<bool> DeleteLabel(UserLabel userLabel);

        Task<List<UserLabel>> GetUserLabelsDetails(string userId);
    }
}
