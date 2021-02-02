using System;

namespace FL.WebAPI.Core.Items.Api.Models.v1.Request
{
    public class UserBirdRequest
    {
        public Guid UserId { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }
    }
}
