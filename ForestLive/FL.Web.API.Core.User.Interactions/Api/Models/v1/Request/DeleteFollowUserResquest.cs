namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Request
{
    public class DeleteFollowUserResquest
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FollowUserId { get; set; }
    }
}
