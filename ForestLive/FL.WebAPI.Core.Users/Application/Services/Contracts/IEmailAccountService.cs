using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Users.Application.Services.Contracts
{
    public interface IEmailAccountService
    {
        Task SendConfirmEmail(Guid userId, string email, string token);

        Task SendForgotPasswordEmail(string email, string code);
    }
}
