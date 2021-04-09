using System;

namespace FL.Web.API.Core.Bird.Pending.Api.Models.v1.Request
{
    public class AssignSpecieRequest
    {
        public Guid PostId { get; set; }

        public Guid SpecieId { get; set; }

        public string SpecieName { get; set; }

        public string UserHelpedIdentification { get; set; }
    }
}
