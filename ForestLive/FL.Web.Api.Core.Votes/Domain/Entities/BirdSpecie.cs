using System;

namespace FL.Web.Api.Core.Votes.Domain.Entities
{
    public class BirdSpecie
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SciencieName { get; set; }

        public string ParentBirdSpecieId { get; set; }
    }
}
