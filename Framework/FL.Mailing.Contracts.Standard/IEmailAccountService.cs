﻿using System;
using System.Threading.Tasks;

namespace FL.Mailing.Contracts.Standard
{
    public interface IEmailAccountService
    {
        Task<bool> SendConfirmEmail(Guid userId, string email, string userName, string token);

        Task<bool> SendForgotPasswordEmail(string email, Guid userId, string userName, string code);
    }
}
