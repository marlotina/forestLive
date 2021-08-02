namespace FL.WebAPI.Core.Account.Models.v1.Request
{
    public class ConfirmEmailRequest
    {
        public string UserId { get; set; }

        public string Code { get; set; }
    }
}
