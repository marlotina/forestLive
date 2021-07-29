using System;

namespace FL.WebAPI.Core.Posts.Api.Models.v1.Request
{
    public class UpdateSpecieRequest
    {
        public Guid PostId { get; set; }

        public Guid SpecieId { get; set; }

        public Guid OldSpecieId { get; set; }

        public string SpecieName { get; set; }
    }
}
