using FL.Mailing.Contracts.Standard.Models;
using System;
using System.Threading.Tasks;

namespace FL.Mailing.Contracts.Standard
{
    public interface IEmailAccountService
    {
        Task<bool> SendConfirmEmail(AccountEmailModel accountEmailMode);

        Task<bool> SendForgotPasswordEmail(AccountEmailModel accountEmailMode);
    }
}
