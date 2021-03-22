using FL.Web.API.Core.User.Posts.Application.Services.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Posts.Application.Services.Implementations
{
    public class UserLabelService : IUserLabelService
    {
        private readonly IUserLabelRepository userLabelRepository;

        public UserLabelService(IUserLabelRepository userLabelRepository)
        {
            this.userLabelRepository = userLabelRepository;
        }

        public async Task<List<string>> GetLabelsByUser(string userId)
        {
            return await this.userLabelRepository.GetUserLabels(userId);
        }
    }
}
