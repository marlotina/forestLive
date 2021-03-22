using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Domain.Repositories
{
    public interface IUserLabelRepository
    {
        Task<List<string>> GetUserLabels(string userId);
    }
}
