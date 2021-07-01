using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
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
            userLabel.Type = "label";
            return await this.userLabelRepository.AddLabel(userLabel);
        }

        public async Task<bool> DeleteLabel(string label, string userId, string userWebSite)
        {
            var userLabel = await this.userLabelRepository.GetUserLabel(label, userId);

            if (userLabel != null && userLabel.UserId != userWebSite)
                throw new UnauthorizedRemove();
                        
            if (userLabel.PostCount > 0)
                throw new UnauthorizedHasPostRemove();

            return await this.userLabelRepository.DeleteLabel(userLabel);
        }

        public async Task<List<UserLabel>> GetUserLabelsDetails(string userId)
        {
            return await this.userLabelRepository.GetUserLabelsDetails(userId);
        }
    }
}
