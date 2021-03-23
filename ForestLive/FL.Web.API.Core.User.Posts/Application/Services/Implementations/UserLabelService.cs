using FL.Web.API.Core.User.Posts.Application.Exceptions;
using FL.Web.API.Core.User.Posts.Application.Services.Contracts;
using FL.Web.API.Core.User.Posts.Domain.Entities;
using FL.Web.API.Core.User.Posts.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<UserLabel> AddLabel(UserLabel userLabel)
        {
            userLabel.CreationDate = DateTime.UtcNow;
            return await this.userLabelRepository.AddLabel(userLabel);
        }

        public async Task<bool> DeleteLabel(string label, string userId, string userWebSite)
        {
            var userLabel = await this.userLabelRepository.GetUserLabel(label, userId);

            if (userLabel != null && userLabel.UserId == userWebSite)
            {
                return await this.userLabelRepository.DeleteLabel(userLabel);
            }
            else
            {
                throw new UnauthorizedRemove();
            }
        }

        public async Task<List<string>> GetLabelsByUser(string userId)
        {
            var list = await this.userLabelRepository.GetUserLabelsByUserId(userId);
            return list.Select(x => x.Id).ToList();
        }

        public async Task<List<UserLabel>> GetUserLabelsDetails(string userId)
        {
            return await this.userLabelRepository.GetUserLabelsDetails(userId);
        }
    }
}
