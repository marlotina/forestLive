using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IEmailAccountService iEmailAccountService;

        public AccountService(
            UserManager<Domain.Entities.User> userManager,
            IEmailAccountService iEmailAccountService)
        {
            this.userManager = userManager;
            this.iEmailAccountService = iEmailAccountService;
        }

        public async Task ConfirmEmailAsync(Guid userId, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new Exception("NO_CODE");

            var user = await this.userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var identityResult = await this.userManager.ConfirmEmailAsync(user, code);
            if (!identityResult.Succeeded)
            {
                throw new Exception("INVALID_CONFIRMATION");
            }
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("NO_EMAIL");

            var user = await this.userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (await this.userManager.IsEmailConfirmedAsync(user))
            {
                var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
                await this.iEmailAccountService.SendForgotPasswordEmail(user.Email, token);
                return token;
            }
            else
            {
                throw new Exception("INVALID_FORGOT_PASSWORD");
            }
        }

        public async Task<string> RegisterAsync(Domain.Entities.User user, string password = null)
        {
            var entityUser = await this.userManager.FindByEmailAsync(user.Email);
            if (entityUser == null)
            {
                var identityResult = string.IsNullOrWhiteSpace(password)
                     ? await this.userManager.CreateAsync(user)
                     : await this.userManager.CreateAsync(user, password);

                string token;
                if (identityResult.Succeeded)
                {
                    token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                    await this.iEmailAccountService.SendConfirmEmail(user.Id, user.Email, token);
                }
                else
                {
                    throw new Exception("INVALID_REGISTRATION");
                }

                return token;
            }
            else
            {
                throw new UserDuplicatedException();
            }
        }

        public async Task ResetPasswordAsync(Guid userId, string code, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(newPassword))
                throw new Exception("NO_CODE");

            var user = await this.userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var identityResult = await this.userManager.ResetPasswordAsync(user, code, newPassword);
            if (!identityResult.Succeeded)
            {
                throw new Exception("INVALID_RESET_PASSWORD");
            }
        }

        public async Task<bool> Delete(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var result = userManager.PasswordHasher.VerifyHashedPassword(user,
                user.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                //TODO algo?
                return true;
            }

            return false;
        }

        
    }
}
