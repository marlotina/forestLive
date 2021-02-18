using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;
using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Mailing.Contracts.Standard;
using FL.LogTrace.Contracts.Standard;
using FL.Mailing.Contracts.Standard.Models;
using System.Linq;
using FL.Sendgrid.Implementation.Standard.Configuration.Models;

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
        public async Task<bool> SendConfirmEmail(AccountEmailModel accountEmailModel)
        {
            try
            {
                var template = this.GetEmailTemplate(accountEmailModel.LanguageId, EmailTypes.ConfirmEmail);

                var msg = new SendGridMessage();
                msg.SetFrom(template.SupportEmail, template.SupportName);
                msg.AddTo(accountEmailModel.Email);
                msg.SetTemplateId(template.TemplateId);
                msg.SetTemplateData(new AccountTemplateModel
                {
                    UserName = accountEmailModel.UserName,
                    Token = WebUtility.UrlEncode(accountEmailModel.Code),
                    UserId = accountEmailModel.UserId.ToString()
                });

                var sendGridClient = new SendGridClient(this.mailConfiguration.SendgridApiKey);
                var response = await sendGridClient.SendEmailAsync(msg);
                return response.StatusCode == HttpStatusCode.Accepted;
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex);
            }

            return false;
        }

        public async Task<bool> SendForgotPasswordEmail(AccountEmailModel accountEmailModel)
        {
            try
            {
                var template = this.GetEmailTemplate(accountEmailModel.LanguageId, EmailTypes.ForgotPassword);
                var client = new SendGridClient(this.mailConfiguration.SendgridApiKey);
                var msg = new SendGridMessage();
                msg.SetFrom(new EmailAddress(template.SupportEmail, template.SupportEmail));
                msg.AddTo(new EmailAddress(accountEmailModel.Email));
                msg.SetTemplateId(template.TemplateId);

                var dynamicTemplateData = new AccountTemplateModel
                {
                    UserName = accountEmailModel.UserName,
                    Token = WebUtility.UrlEncode(accountEmailModel.Code),
                    UserId = accountEmailModel.UserId.ToString()
                };

                msg.SetTemplateData(dynamicTemplateData);
                var response = await client.SendEmailAsync(msg);
                return response.StatusCode == HttpStatusCode.Accepted;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
            }

            return false;
        }

        private EmailItemConfiguration GetEmailTemplate(Guid languageId, string type)
        {
            var language = languageId;
            if (language == null || language == Guid.Empty)
                language = Guid.Parse("1d134297-b070-4149-a2e0-c2643de48f95");

            var template = this.mailConfiguration.EmailTemaplateList.FirstOrDefault(x => x.LangaugeId == language && x.TypeEmail == type);
            return template;
        }
    }
}