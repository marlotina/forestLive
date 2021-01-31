using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class BirdSpecie
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string SciencieName { get; set; }

        public string ParentBirdSpecieId { get; set; }
    }
}
