using System;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Request
{
    public class FollowUserRequest
    {
        public string UserId { get; set; }

        public string FollowUserId { get; set; }

        public Guid userSystemId { get; set; }
    }
}
