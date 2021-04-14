using System;

namespace FL.Web.API.Core.User.Interactions.Api.Models.v1.Response
{
    public class VoteResponse
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public string AuthorPostId { get; set; }

        public string UserId { get; set; }

        public string CreationDate { get; set; }

        public string TitlePost { get; set; }

        public Guid? SpecieId { get; set; }
    }
}
