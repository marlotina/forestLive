using FL.WebAPI.Core.Users.Models.v1.Response;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IAccountService
    {
        Task ConfirmEmailAsync(Guid userId, string code);

        Task<string> RegisterAsync(Domain.Entities.User user, string password = null);

        Task<string> ForgotPasswordAsync(string email);

        Task ResetPasswordAsync(Guid userId, string code, string newPassword);

        Task<bool> Delete(string email, string password);

        AuthResponse Authenticate(string username, string password);
    }
}
