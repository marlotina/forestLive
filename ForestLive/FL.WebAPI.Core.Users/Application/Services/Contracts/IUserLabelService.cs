using FL.WebAPI.Core.Users.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserLabelService
    {
        Task<UserLabel> AddLabel(UserLabel userLabel);

        Task<bool> DeleteLabel(string label, string userId, string userWebSite);

        Task<List<UserLabel>> GetUserLabelsDetails(string userId);
    }
}
