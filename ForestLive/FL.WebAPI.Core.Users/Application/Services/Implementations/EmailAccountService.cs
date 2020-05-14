using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FL.WebAPI.Core.Users.Application.Services.Contracts;
using FL.WebAPI.Core.Users.Configuration.Contracts;

namespace FL.WebAPI.Core.Users.Application.Services.Implementations
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IUserConfiguration iUserConfiguration;

        public EmailAccountService(IUserConfiguration iUserConfiguration)
        {
            this.iUserConfiguration = iUserConfiguration;
        }
        public async Task SendConfirmEmail(Guid userId, string email, string token)
        {
            var htmlContent = $"{this.iUserConfiguration.UrlConfirmEmail}?code={WebUtility.UrlEncode(token)}&userId={userId}";

            var apiKey = this.iUserConfiguration.SendgridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.iUserConfiguration.SupportEmail, this.iUserConfiguration.SupportName);
            var to = new EmailAddress(email);
            var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, "LABEL_Confirm email", plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendForgotPasswordEmail(string email, string code)
        {
            var htmlContent = $"{this.iUserConfiguration.UrlForgotPasswordEmail}<br/> {WebUtility.UrlEncode(code)}";

            var apiKey = this.iUserConfiguration.SendgridApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(this.iUserConfiguration.SupportEmail, this.iUserConfiguration.SupportName);
            var to = new EmailAddress(email);
            var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, "LABEL_Recover password", plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
