using FL.WebAPI.Core.Users.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<Domain.Entities.User> GetByUserNameAsync(string userName);

        Task<IEnumerable<AutocompleteResponse>> AutocompleteByUserName(string keys); 

        Task<IEnumerable<Domain.Entities.User>> GetUsersAsync();
    }
}
