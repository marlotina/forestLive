using System;

namespace FL.WebAPI.Core.Users.Api.Models.v1.Request
{
    public class ImageProfileRequest
    {
        public string ImageBase64 { get; set; }

        public string ImageName { get; set; }

        public Guid UserId { get; set; }
    }
}
