using System;

namespace FL.WebAPI.Core.Users.Api.Models.v1.Response
{
    public class UserLabelResponse
    {
        public string UserId { get; set; }

        public string Label { get; set; }

        public int PostCount { get; set; }

        public string CreationDate { get; set; }
    }
}
