using FL.WebAPI.Core.Users.Domain.Dto;
using FL.WebAPI.Core.Users.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetByUserNameAsync(string userName);

        Task<IEnumerable<AutocompleteResponse>> AutocompleteByUserName(string keys); 

        Task<IEnumerable<User>> GetUsersAsync();

        Task<IEnumerable<User>> GetUsersByKey(string keys);
    }
}
