namespace FL.Sendgrid.Implementation.Standard.Configuration.Contracts
{ 
    public interface IMailConfiguration 
    {
        string SendgridApiKey { get; }

        string SupportName { get; }

        string SupportEmail { get; }

        string ForgotPasswordTemplate { get; }

        string ConfirmAccountTemplate { get; }
    }
}
