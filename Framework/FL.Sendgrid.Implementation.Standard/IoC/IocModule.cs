using FL.DependencyInjection.Standard.Contracts;
using FL.Mailing.Contracts.Standard;
using FL.Sendgrid.Implementation.Standard.Configuration.Contracts;
using FL.Sendgrid.Implementation.Standard.Configuration.Implementations;
using FL.Sendgrid.Implementation.Standard.Implementations;

namespace FL.Sendgrid.Implementation.Standard.IoC
{
    public class IocModule : IModule
    {
        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IMailConfiguration, MailConfiguration>();
            services.AddTransient<IEmailAccountService, EmailAccountService>();
        }
    }
}
