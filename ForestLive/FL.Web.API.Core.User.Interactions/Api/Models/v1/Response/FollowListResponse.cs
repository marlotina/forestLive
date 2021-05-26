using System;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Response
{
    public class FollowListResponse
    {
        public string Id { get; set; }

        public DateTime CreationDate { get; set; }

        public string UserId { get; set; }

        public string FollowUserId { get; set; }
    }
}
