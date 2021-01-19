using System;

namespace FL.WebAPI.Core.Items.Models.v1.Request
{
    public class DeletePostRequest
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Type { get; set; }
    }
}
