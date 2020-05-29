using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;
using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Mailing.Contracts.Standard;

namespace FL.Sendgrid.Implementation.Standard.Implementations
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IMailConfiguration mailConfiguration;

        public EmailAccountService(IMailConfiguration mailConfiguration)
        {
            this.mailConfiguration = mailConfiguration;
        }
        public async Task SendConfirmEmail(Guid userId, string email, string userName, string token)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(this.mailConfiguration.SupportEmail, this.mailConfiguration.SupportName);
            msg.AddTo(email);
            msg.SetTemplateId(this.mailConfiguration.ConfirmAccountTemplate);
            msg.SetTemplateData(new
            {
                UserName = userName,
                Token = WebUtility.UrlEncode(token),
                UserId = userId
            });

            await this.SendTransactionalMail(msg);
        }

        public async Task SendForgotPasswordEmail(string email, Guid userId, string userName, string code)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(this.mailConfiguration.SupportEmail, this.mailConfiguration.SupportName);
            msg.AddTo(email);
            msg.SetTemplateId(this.mailConfiguration.ForgotPasswordTemplate);
            msg.SetTemplateData(new
            {
                UserName = userName,
                Token = WebUtility.UrlEncode(code),
                UserId = userId
            });

            await this.SendTransactionalMail(msg);
        }

        public async Task SendTransactionalMail(SendGridMessage sendGridMessage) 
        {
            var sendGridClient = new SendGridClient(this.mailConfiguration.SendgridApiKey);
            await sendGridClient.SendEmailAsync(sendGridMessage);
            
        }
    }
}