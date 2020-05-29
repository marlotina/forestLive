using FL.Mailing.Contracts.Standard.Models;
using System;
using System.Threading.Tasks;

namespace FL.Mailing.Contracts.Standard
{
    public interface IEmailAccountService
    {
        Task<bool> SendConfirmEmail(AccountEmailModel accountEmailModel);

        Task<bool> SendForgotPasswordEmail(AccountEmailModel accountEmailModel);
    }
}
