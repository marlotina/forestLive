using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Application.Exceptions;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using System.Linq;
using FL.WebAPI.Core.Users.Models.v1.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FL.WebAPI.Core.Users.Configuration.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Domain.Entities.User> userManager;
        private readonly IEmailAccountService iEmailAccountService;
        private readonly IUserConfiguration iUserConfiguration;

        public AccountService(
            UserManager<Domain.Entities.User> userManager,
            IEmailAccountService iEmailAccountService,
            IUserConfiguration iUserConfiguration)
        {
            this.userManager = userManager;
            this.iEmailAccountService = iEmailAccountService;
            this.iUserConfiguration = iUserConfiguration;
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
                await this.iEmailAccountService.SendForgotPasswordEmail(user.Email, user.Id, token);
                return token;
            }
            else
            {
                throw new Exception("INVALID_FORGOT_PASSWORD");
            }
        }

        public async Task<string> RegisterAsync(Domain.Entities.User user, string password = null)
        {
            var identityResult = string.IsNullOrWhiteSpace(password)
                    ? await this.userManager.CreateAsync(user)
                    : await this.userManager.CreateAsync(user, password);

            string token = string.Empty;
            if (identityResult.Succeeded)
            {
                token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                await this.iEmailAccountService.SendConfirmEmail(user.Id, user.Email, token);
            }
            else
            {
                if (identityResult.Errors.Any(x => x.Code == nameof(IdentityErrorDescriber.DuplicateEmail)))
                {
                    throw new EmailDuplicatedException();
                }

                if (identityResult.Errors.Any(x => x.Code == nameof(IdentityErrorDescriber.DuplicateUserName)))
                {
                    throw new UserDuplicatedException();
                }
            }

            return token;
        }

        public AuthResponse Authenticate(string username, string password)
        {
            var user = userManager.FindByEmailAsync(username).Result;

            if (user == null)
                return null;

            if (!user.EmailConfirmed)
                throw new UserNotEmailConfirm();

            var result = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(this.iUserConfiguration.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new AuthResponse
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FirstName = user.Name,
                    LastName = user.Surname,
                    Token = tokenHandler.WriteToken(token),
                    Photo = user.Photo
                };
            }

            return null;
        }

        public async Task ResetPasswordAsync(Guid userId, string code, string newPassword)
        {
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
