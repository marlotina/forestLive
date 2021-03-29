using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.UserLabels.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.UserLabels.Application.Services.Contracts
{
    public interface IUserLabelService
    {
        Task<List<UserLabelDto>> GetLabelsByUser(string userId);

        Task<UserLabel> AddLabel(UserLabel userLabel);

        Task<bool> DeleteLabel(string label, string userId, string userWebSite);

        Task<List<UserLabel>> GetUserLabelsDetails(string userId);
    }
}
