using FL.Sendgrid.Implementation.Standard.Configuration.Models;
using System.Collections.Generic;

namespace FL.Sendgrid.Implementation.Standard.Configuration.Contracts
{ 
    public interface IMailConfiguration 
    {
        string SendgridApiKey { get; }

        List<EmailItemConfiguration> EmailTemaplateList { get; }
    }
}
