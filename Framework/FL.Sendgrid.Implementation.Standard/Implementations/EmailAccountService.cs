using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;
using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Mailing.Contracts.Standard;
using FL.LogTrace.Contracts.Standard;

namespace FL.Sendgrid.Implementation.Standard.Implementations
{
    public class EmailAccountService : IEmailAccountService
    {
        private readonly IMailConfiguration mailConfiguration;
        private readonly ILogger<EmailAccountService> logger; 

        public EmailAccountService(
            IMailConfiguration mailConfiguration,
            ILogger<EmailAccountService> logger)
        {
            this.mailConfiguration = mailConfiguration;
            this.logger = logger;
        }
        public async Task<bool> SendConfirmEmail(Guid userId, string email, string userName, string token)
        {
            try 
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
                return true;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex);
            }

            return false;
        }

        public async Task<bool> SendForgotPasswordEmail(string email, Guid userId, string userName, string code)
        {
            try
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
                return true;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
            }

            return false;

        }

        public async Task SendTransactionalMail(SendGridMessage sendGridMessage) 
        {
            var sendGridClient = new SendGridClient(this.mailConfiguration.SendgridApiKey);
            await sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }
}