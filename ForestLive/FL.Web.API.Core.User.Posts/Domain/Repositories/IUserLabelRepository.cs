using FL.Web.API.Core.User.Posts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserLabelRepository
    {
        Task<List<LabelDto>> GetUserLabels(string userId);
    }
}
